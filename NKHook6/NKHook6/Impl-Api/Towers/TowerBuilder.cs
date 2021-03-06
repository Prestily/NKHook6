﻿
using Assets.Scripts.Models;
using Assets.Scripts.Models.Map;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Mods;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Unity;
using Assets.Scripts.Utils;
using Harmony;
using NKHook6.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;

namespace NKHook6.Api.Towers
{
    public class TowerBuilder
    {
        #region Default Properties
        public string name = "Monkey";
        public string baseId = "Monkey";
        public string towerSet = "Primary";
        public string display = "Towers/Dart";
        public float cost;
        public float radius;
        public float range;
        public bool ignoreBlockers = false;
        public bool isGlobalRange = false;
        public int tier;
        public int[] tiers;
        public string[] appliedUpgrades;
        public UpgradePathModel[] upgrades;
        public List<TowerBehaviorModel> behaviors;
        public AreaType[] areaTypes;
        public SpriteReference icon;
        public SpriteReference portrait;
        public SpriteReference instaIcon;
        public ApplyModModel[] mods;
        public bool ignoreTowerForSelection = false;
        public bool isSubTower = false;
        public bool isBakable = false;
        public FootprintModel footprint;
        public bool dontDisplayUpgrades = false;
        public string powerName;
        public float animationSpeed;
        public SpriteReference emoteSpriteSmall;
        public SpriteReference emoteSpriteLarge;
        public bool doesntRotate = false;
        public bool showPowerTowerBuffs = false;
        public string towerSelectionMenuThemeId = "Default";
        public bool ignoreCoopAreas = false;
        #endregion
        #region Custom Properties
        public bool visibleInShop = false;
        #endregion
        #region Constructor
        public TowerBuilder() : this("DartMonkey") { }
        public TowerBuilder(string baseName) : this(Game.instance.getTowerModel(baseName)) { }
        public TowerBuilder(TowerModel baseModel)
        {
            this.name = baseModel.name;
            this.baseId = baseModel.baseId;
            this.towerSet = baseModel.towerSet;
            this.display = baseModel.display;
            this.cost = baseModel.cost;
            this.radius = baseModel.radius;
            this.range = baseModel.range;
            this.ignoreBlockers = baseModel.ignoreBlockers;
            this.isGlobalRange = baseModel.isGlobalRange;
            this.tier = baseModel.tier;
            this.tiers = baseModel.tiers;
            this.appliedUpgrades = baseModel.appliedUpgrades;
            this.upgrades = baseModel.upgrades;
            List<TowerBehaviorModel> modelList = new List<TowerBehaviorModel>();
            foreach(Model behaviorModel in baseModel.behaviors)
            {
                TowerBehaviorModel newModel = new TowerBehaviorModel(behaviorModel.Pointer);
                modelList.Add(newModel);
            }
            this.behaviors = modelList;
            this.areaTypes = baseModel.areaTypes;
            this.icon = baseModel.icon;
            this.portrait = baseModel.portrait;
            this.instaIcon = baseModel.instaIcon;
            this.mods = baseModel.mods;
            this.ignoreTowerForSelection = baseModel.ignoreTowerForSelection;
            this.isSubTower = baseModel.isSubTower;
            this.isBakable = baseModel.isBakable;
            this.footprint = baseModel.footprint;
            this.dontDisplayUpgrades = baseModel.dontDisplayUpgrades;
            this.powerName = baseModel.powerName;
            this.animationSpeed = baseModel.animationSpeed;
            this.emoteSpriteSmall = baseModel.emoteSpriteSmall;
            this.emoteSpriteLarge = baseModel.emoteSpriteLarge;
            this.doesntRotate = baseModel.doesntRotate;
            this.showPowerTowerBuffs = baseModel.showPowerTowerBuffs;
            this.towerSelectionMenuThemeId = baseModel.towerSelectionMenuThemeId;
            this.ignoreCoopAreas = baseModel.ignoreCoopAreas;
            this.visibleInShop = false;
        }
        #endregion
        #region Functions
        public TowerBuilder SetName(string name)
        {
            this.name = name;
            return this;
        }
        public TowerBuilder SetBaseId(string baseId)
        {
            this.baseId = baseId;
            return this;
        }
        public TowerBuilder SetTowerSet(string towerSet)
        {
            this.towerSet = towerSet;
            return this;
        }
        public TowerBuilder SetDisplay(string display)
        {
            this.display = display;
            return this;
        }
        public TowerBuilder SetCost(float cost)
        {
            this.cost = cost;
            return this;
        }
        public TowerBuilder SetRadius(float radius)
        {
            this.radius = radius;
            return this;
        }
        public TowerBuilder SetRange(float range)
        {
            this.range = range;
            return this;
        }
        public TowerBuilder IgnoreBlockers(bool ignoreBlockers)
        {
            this.ignoreBlockers = ignoreBlockers;
            return this;
        }
        public TowerBuilder SetGlobalRange(bool isGlobalRange)
        {
            this.isGlobalRange = isGlobalRange;
            return this;
        }
        public TowerBuilder SetTier(int tier)
        {
            this.tier = tier;
            return this;
        }
        public TowerBuilder SetTiers(int[] tiers)
        {
            this.tiers = tiers;
            return this;
        }
        public TowerBuilder SetAppliedUpgrades(string[] appliedUpgrades)
        {
            this.appliedUpgrades = appliedUpgrades;
            return this;
        }
        public TowerBuilder SetUpgrades(UpgradePathModel[] upgrades)
        {
            this.upgrades = upgrades;
            return this;
        }
        public TowerBuilder AddBehavior(Model behavior)
        {
            return AddBehavior(behavior);
        }
        public TowerBuilder AddBehavior(TowerBehaviorModel behavior)
        {
            this.behaviors.Add(behavior);
            return this;
        }
        public TowerBuilder RemoveBehavior(string behaviorType)
        {
            foreach(TowerBehaviorModel behavior in behaviors)
            {
                if (behavior.name.StartsWith(behaviorType))
                {
                    behaviors.Remove(behavior);
                    break;
                }
            }
            return this;
        }
        public TowerBuilder RemoveBehavior(Model behavior)
        {
            return RemoveBehavior(behavior);
        }
        public TowerBuilder RemoveBehavior(TowerBehaviorModel behavior)
        {
            behaviors.Remove(behavior);
            return this;
        }
        public TowerBuilder SetBehaviors(List<TowerBehaviorModel> behaviors)
        {
            this.behaviors = behaviors;
            return this;
        }
        public TowerBuilder SetAreaTypes(AreaType[] areaTypes)
        {
            this.areaTypes = areaTypes;
            return this;
        }
        public TowerBuilder SetIcon(SpriteReference icon)
        {
            this.icon = icon;
            return this;
        }
        public TowerBuilder SetPortrait(SpriteReference portrait)
        {
            this.portrait = portrait;
            return this;
        }
        public TowerBuilder SetInstaIcon(SpriteReference instaIcon)
        {
            this.instaIcon = instaIcon;
            return this;
        }
        public TowerBuilder SetMods(ApplyModModel[] mods)
        {
            this.mods = mods;
            return this;
        }
        public TowerBuilder SetIgnoreTowerForSelection(bool ignoreTowerForSelection)
        {
            this.ignoreTowerForSelection = ignoreTowerForSelection;
            return this;
        }
        public TowerBuilder SetIsSubTower(bool isSubTower)
        {
            this.isSubTower = isSubTower;
            return this;
        }
        public TowerBuilder SetIsBakable(bool isBakable)
        {
            this.isBakable = isBakable;
            return this;
        }
        public TowerBuilder SetFootprint(FootprintModel footprint)
        {
            this.footprint = footprint;
            return this;
        }
        public TowerBuilder SetDontDisplayUpgrades(bool dontDisplayUpgrades)
        {
            this.dontDisplayUpgrades = dontDisplayUpgrades;
            return this;
        }
        public TowerBuilder SetIsPowerTower(string powerName)
        {
            this.powerName = powerName;
            return this;
        }
        public TowerBuilder SetAnimationSpeed(float animationSpeed)
        {
            this.animationSpeed = animationSpeed;
            return this;
        }
        public TowerBuilder SetEmoteSpriteSmall(SpriteReference emoteSpriteSmall)
        {
            this.emoteSpriteSmall = emoteSpriteSmall;
            return this;
        }
        public TowerBuilder SetEmoteSpriteLarge(SpriteReference emoteSpriteLarge)
        {
            this.emoteSpriteLarge = emoteSpriteLarge;
            return this;
        }
        public TowerBuilder SetDoesntRotate(bool doesntRotate)
        {
            this.doesntRotate = doesntRotate;
            return this;
        }
        public TowerBuilder SetShowPowerTowerBuffs(bool showPowerTowerBuffs)
        {
            this.showPowerTowerBuffs = showPowerTowerBuffs;
            return this;
        }
        public TowerBuilder SetTowerSelectionMenuThemeId(string towerSelectionMenuThemeId)
        {
            this.towerSelectionMenuThemeId = towerSelectionMenuThemeId;
            return this;
        }
        public TowerBuilder SetIgnoreCoopAreas(bool ignoreCoopAreas)
        {
            this.ignoreCoopAreas = ignoreCoopAreas;
            return this;
        }
        public TowerBuilder SetVisibleInShop(bool visibleInShop)
        {
            this.visibleInShop = visibleInShop;
            return this;
        }
        #endregion
        #region Builder
        public TowerModel build()
        {
            return new TowerModel(
                this.name,
                this.baseId,
                this.towerSet,
                this.display,
                this.cost,
                this.radius,
                this.range,
                this.ignoreBlockers,
                this.isGlobalRange,
                this.tier,
                this.tiers,
                this.appliedUpgrades,
                this.upgrades,
                this.behaviors.ToArray(),
                this.areaTypes,
                this.icon,
                this.portrait,
                this.instaIcon,
                this.mods,
                this.ignoreTowerForSelection,
                this.isSubTower,
                this.isBakable,
                this.footprint,
                this.dontDisplayUpgrades,
                this.powerName,
                this.animationSpeed,
                this.emoteSpriteSmall,
                this.emoteSpriteLarge,
                this.doesntRotate,
                this.showPowerTowerBuffs,
                this.towerSelectionMenuThemeId,
                this.ignoreCoopAreas
            );
        }
        #endregion
    }
}