using Cake.Common;
using Cake.Common.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Clean;
using Cake.Common.Tools.DotNet.Publish;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;
using Vintagestory.API.Common;

namespace CakeBuild
{
	public static class Program
	{
		public static int Main(string[] args)
		{
			return new CakeHost()
					.UseContext<BuildContext>()
					.Run(args);
		}
	}

	public class BuildContext : FrostingContext
	{
		public const string ProjectName = "MorePilesWithSticks";
		public string BuildConfiguration { get; }
		public string Version { get; }
		public string Name { get; }
		public bool SkipJsonValidation { get; }
		public string ResourcesLocation { get; set; }
		public string ModInfoLocation { get; set; }

		public BuildContext(ICakeContext context)
				: base(context)
		{
			BuildConfiguration = context.Argument("configuration", "Release");
			SkipJsonValidation = context.Argument("skipJsonValidation", false);
			ModInfoLocation = $"../modinfo.json";
			ModInfo modInfo = context.DeserializeJsonFromFile<ModInfo>(ModInfoLocation);
			Version = modInfo.Version;
			Name = modInfo.ModID;
			ResourcesLocation = $"../resources/assets";
		}
	}

	[TaskName("ValidateJson")]
	public sealed class ValidateJsonTask : FrostingTask<BuildContext>
	{
		public override void Run(BuildContext context)
		{
			if (context.SkipJsonValidation)
			{
				return;
			}
			FilePathCollection jsonFiles = context.GetFiles($"{context.ResourcesLocation}/**/*.json");
			foreach (FilePath file in jsonFiles)
			{
				try
				{
					string json = File.ReadAllText(file.FullPath);
					JToken.Parse(json);
				}
				catch (JsonException ex)
				{
					throw new Exception($"Validation failed for JSON file: {file.FullPath}{Environment.NewLine}{ex.Message}", ex);
				}
			}
		}
	}

	[TaskName("Build")]
	[IsDependentOn(typeof(ValidateJsonTask))]
	public sealed class BuildTask : FrostingTask<BuildContext>
	{
		public override void Run(BuildContext context)
		{
			context.DotNetClean($"../{BuildContext.ProjectName}.csproj",
					new DotNetCleanSettings
					{
						Configuration = context.BuildConfiguration
					});


			context.DotNetPublish($"../{BuildContext.ProjectName}.csproj",
					new DotNetPublishSettings
					{
						Configuration = context.BuildConfiguration
					});
		}
	}

	[TaskName("Package")]
	[IsDependentOn(typeof(BuildTask))]
	public sealed class PackageTask : FrostingTask<BuildContext>
	{
		public override void Run(BuildContext context)
		{
			context.EnsureDirectoryExists("../Releases");
			context.CleanDirectory("../Releases");
			context.EnsureDirectoryExists($"../Releases/{context.Name}");
			context.CopyFiles($"../bin/{context.BuildConfiguration}/Mods/mod/publish/*", $"../Releases/{context.Name}");

			if (context.DirectoryExists(context.ResourcesLocation))
			{
				context.CopyDirectory(context.ResourcesLocation, $"../Releases/{context.Name}/assets");
			}
			context.CopyFile($"../modinfo.json", $"../Releases/{context.Name}/modinfo.json");
			if (context.FileExists($"../{BuildContext.ProjectName}/modicon.png"))
			{
				context.CopyFile($"../{BuildContext.ProjectName}/modicon.png", $"../Releases/{context.Name}/modicon.png");
			}
			context.Zip($"../Releases/{context.Name}", $"../Releases/{context.Name}_{context.Version}.zip");
		}
	}

	[TaskName("Default")]
	[IsDependentOn(typeof(PackageTask))]
	public class DefaultTask : FrostingTask
	{
	}
}