using Newtonsoft.Json.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;
using Vintagestory.GameContent;
using System.Linq;
using static HarmonyLib.Code;
using System.Diagnostics;
using System;
using System.Runtime.InteropServices;

namespace MorePiles
{
	public static class Patches
	{
		public static void AppendBehaviors(this ICoreAPI api)
		{
			StringComparer ordinalComparer = StringComparer.Ordinal;
			Stopwatch timer = Stopwatch.StartNew();

			Span<CollectibleObject> collectibles = CollectionsMarshal.AsSpan(api.World.Collectibles.Where(item => item.Code != null || item.Id != 0).ToList());
			CollectibleObject obj;
			for (int i = collectibles.Length - 1; i != 0; --i)
			{
				obj = collectibles[i];
				if (obj.HasBehavior<CollectibleBehaviorGroundStorable>())
				{
					switch (obj.Code.Path)
					{
						case "stick":
						case "bone":
							break;
						default:
							continue;
					}
				}

				#region arrow
				if (obj.WildCardMatch("arrow-*"))
				{
					const string key = "arrow";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)16 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.0625f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/stickplace");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion arrow
				#region bamboostakes
				if (ordinalComparer.Compare(obj.Code.Path, "bamboostakes") == 0)
				{
					const string key = "bamboostakes";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.125f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/stickplace");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion bamboostakes
				#region beeswax
				if (ordinalComparer.Compare(obj.Code.Path, "beeswax") == 0)
				{
					const string key = "beeswax";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.2941f, 0, 0.2941f, 0.75f, 0.125f, 0.75f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/effect/squish1");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion beeswax
				#region bone
				if (ordinalComparer.Compare(obj.Code.Path, "bone") == 0)
				{
					const string key = "bone";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)16 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.0625f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/ceramicplace");
					obj.CollectibleBehaviors = new CollectibleBehavior[1];
					JsonObject properties = new JsonObject(JToken.FromObject(gsprops));
					CollectibleBehaviorGroundStorable cbgs = new(obj);
					cbgs.Initialize(properties);
					obj.CollectibleBehaviors[0] = cbgs;
					AppendCreativeInventoryTabs(obj);
				}
				#endregion bone
				#region chutesection
				if (obj.WildCardMatch("chutesection-*"))
				{
					const string key = "chutesection";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)20 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.05f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/chute");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion chutesection
				#region cloth
				if (obj.WildCardMatch("cloth-*"))
				{
					const string key = "cloth";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.125f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/cloth");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion cloth
				#region flaxfibers
				if (ordinalComparer.Compare(obj.Code.Path, "flaxfibers") == 0)
				{
					const string key = "flaxfibers";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)16 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.0625f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/cloth");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion flaxfibers
				#region flaxtwine
				if (ordinalComparer.Compare(obj.Code.Path, "flaxtwine") == 0)
				{
					const string key = "flaxtwine";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.1875f, 0, 0.1875f, 0.8125f, 0.125f, 0.8125f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/cloth");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion flaxtwine
				#region metalchain
				if (obj.WildCardMatch("metalchain-*"))
				{
					const string key = "metalchain";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.0315f, 0, 0.0315f, 0.9685f, 0.125f, 0.9685f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/wearable/chain1");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion metalchain
				#region metallamellae
				if (obj.WildCardMatch("metallamellae-*"))
				{
					const string key = "metallamellae";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.0315f, 0, 0.0315f, 0.9685f, 0.125f, 0.9685f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/wearable/chain1");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion metallamellae
				#region metalscale
				if (obj.WildCardMatch("metalscale-*"))
				{
					const string key = "metalscale";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.1875f, 0, 0.1875f, 0.8125f, 0.125f, 0.8125f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/wearable/chain1");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion metalscale
				#region sail
				if (ordinalComparer.Compare(obj.Code.Path, "sail") == 0)
				{
					const string key = "sail";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.125f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/cloth");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion sail
				#region stick
				if (ordinalComparer.Compare(obj.Code.Path, "stick") == 0)
				{
					const string key = "stick";
					PileExtended pileEx = new(key, obj, api);
					if (!pileEx.Enabled) continue;
					GroundStorageProperties gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)16 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.0625f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/stickplace");
					obj.CollectibleBehaviors = new CollectibleBehavior[1];
					JsonObject properties = new JsonObject(JToken.FromObject(gsprops));
					CollectibleBehaviorGroundStorable cbgs = new(obj);
					cbgs.Initialize(properties);
					obj.CollectibleBehaviors[0] = cbgs;
					AppendCreativeInventoryTabs(obj);
				}
				#endregion stick

				#region angledgears
				if (obj.WildCardMatch("angledgears-s"))
				{
					const string key = "angledgears";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.1875f, 0, 0.1875f, 0.8215f, 0.125f, 0.8215f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/stickplace");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion angledgears
				#region axle
				if (obj.WildCardMatch("woodenaxle-ud"))
				{
					const string key = "axle";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)4 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.25f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/stickplace");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion axle
				#region ironfence
				if (obj.WildCardMatch(new string[] { "ironfence-base-ew", "ironfence-top-ew" }))
				{
					const string key = "ironfence";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)16 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.0625f, 0, 0.0625f, 0.9375f, 0.0625f, 0.9375f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/ingot");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion ironfence
				#region torchholder
				if (obj.WildCardMatch("torchholder-*-empty-north"))
				{
					const string key = "torchholder";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)4 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.188f, 0, 0.188f, 0.812f, 0.25f, 0.812f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/ingot");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion torchholder
				#region drystonefence
				if (obj.WildCardMatch("drystonefence-*-ew-free"))
				{
					const string key = "drystonefence";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)4 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.25f, 0, 0.25f, 0.75f, 0.25f, 0.75f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/loosestone1");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion drystonefence
				#region henbox
				if (obj.WildCardMatch("henbox-empty") || obj.WildCardMatch(new AssetLocation("vanvar", "henbox-*-empty")))
				{
					const string key = "henbox";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)4 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.0625f, 0, 0.0625f, 0.9375f, 0.25f, 0.9375f);
					gsprops.ModelItemsToStackSizeRatio = 320 / gsprops.StackingCapacity;
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/planks");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion henbox
				#region ladder
				if (obj.WildCardMatch(new string[] { "ladder-*-north" }))
				{
					const string key = "ladder";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.125f, 0, 0.125f, 0.875f, 0.125f, 0.875f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/planks");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion ladder
				#region sign
				if (ordinalComparer.Compare(obj.Code.Path, "sign-ground-north") == 0 || obj.WildCardMatch(new AssetLocation("vanvar", "sign-*-ground-north")))
				{
					const string key = "sign";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.125f, 0, 0.125f, 0.875f, 0.125f, 0.875f);
					gsprops.ModelItemsToStackSizeRatio = 320 / gsprops.StackingCapacity;
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/planks");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion sign
				#region toolrack
				if (ordinalComparer.Compare(obj.Code.Path, "toolrack-north") == 0 || obj.WildCardMatch(new AssetLocation("vanvar", "toolrack-*-north")))
				{
					const string key = "toolrack";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.1565f, 0, 0.1565f, 0.8435f, 0.125f, 0.8435f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/planks");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion toolrack
				#region trapdoor
				if (ordinalComparer.Compare(obj.Code.Path, "trapdoor-closed-up-north") == 0 || obj.WildCardMatch(new AssetLocation("vanvar", "trapdoor-*-closed-up-north")))
				{
					const string key = "trapdoor";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.125f, 0, 0.125f, 0.875f, 0.125f, 0.875f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/planks");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion trapdoor
				#region fence
				if (obj.WildCardMatch("woodenfence-*-ew-free"))
				{
					const string key = "fence";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;
					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.125f, 0, 0.125f, 0.875f, 0.125f, 0.875f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/planks");
					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion fence
				#region fencegate
				if (obj.WildCardMatch("woodenfencegate-*-n-closed-left-free"))
				{
					const string key = "fencegate";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;

					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0.125f, 0, 0.125f, 0.875f, 0.125f, 0.875f);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/planks");

					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion fencegate
				#region stone
				if (obj.WildCardMatch("stone-*") && obj is ItemStone)
				{
					const string key = "stone";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;

					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.125f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/loosestone");

					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion stone
				#region rope
				if (obj.Code.Equals(new AssetLocation("rope")))
				{
					const string key = "rope";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;

					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.125f, 1);
					gsprops.PlaceRemoveSound = new AssetLocation("sounds/block/cloth");

					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion rope
				#region mostCubicBlocks
				if (obj is Block block && block.Shape.Base == new AssetLocation("block/basic/cube"))
				{
					const string key = "mostCubicBlocks";
					var pileEx = new PileExtended(key, obj, api);
					if (!pileEx.Enabled) continue;
					var gsprops = pileEx.GroundStorageProps;

					gsprops.CbScaleYByLayer = (float)8 / gsprops.StackingCapacity;
					gsprops.CollisionBox = new Cuboidf(0, 0, 0, 1, 0.125f, 1);
					gsprops.PlaceRemoveSound = block.Sounds.Place ?? new AssetLocation("sounds/player/build");

					AppendBehavior(obj, gsprops, new CollectibleBehaviorGroundStorable(obj));
					AppendCreativeInventoryTabs(obj);
				}
				#endregion mostCubicBlocks
			}
			timer.Stop();
			api.Logger.Notification($"MorePilesWithSticks: Insert behavior time - {timer.ElapsedTicks / TimeSpan.TicksPerMicrosecond} μs");
		}

		private static void AppendBehavior(CollectibleObject collobj, object props, CollectibleBehavior instance)
		{
			JsonObject properties = new JsonObject(JToken.FromObject(props));
			instance.Initialize(properties);
			collobj.CollectibleBehaviors = collobj.CollectibleBehaviors.Append(instance);
		}

		private static void AppendCreativeInventoryTabs(CollectibleObject obj)
		{
			if (obj.CreativeInventoryTabs != null && obj.CreativeInventoryTabs.Length != 0 && !string.IsNullOrEmpty(obj?.CreativeInventoryTabs?[0]))
			{
				obj.CreativeInventoryTabs = obj.CreativeInventoryTabs.Append("groundstorable");
			}
		}
	}
}