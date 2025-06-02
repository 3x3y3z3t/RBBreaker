/*  GameClone/Enums.cs
 *  Version 1.0 (2025.05.29)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSaveEditor.GameClone
{
    public enum eMetagameFlags
    {

    }

    public enum eMetagameNPEStep
    {
        OpenedProfileDialogWithBreakAvailable,
        BrokeProfile,
        OpenedRealityStream,
        BoughtTalent,
        SawTierLevelHelp,
        SawChangeDifficultyHelp,
        SawSpendRealityPointsHelp,
        SawDialogueRewritingHelp,
        SawEntityRewritingHelp,
        PressedBreakButton
    }

    public enum eNPEStep
    {
        None,
        SwitchedToCraftingTab,
        CraftedQuantumFrameRegulator,
        Deprecated,
        SawInputHelpShipMovement,
        SawInputHelpFiringWeapons,
        SawInputHelpShipRegeneration,
        SawInputHelpShipPropulsion,
        SawInputHelpShipOpenAreaMap,
        SawInputHelpOpenShipDialog,
        SawInputHelpRewriteItems,
        SawHelpAccumulateFocus,
        SawInputHelpUseFocusAbility,
        SawInputHelpEquipItem,
        SawInputHelpFiringSecondaryWeapons,
        LootedFatePulse,
        SawHelpSpendFatePulses,
        SawHelpPilotLeveling,
        SawHelpBuyingSkills,
        SawInputHelpOpenCharacter,
        SawInputHelpOpenCodexDialog,
        SawInputHelpUseActiveSkill,
        SawExperienceBarHelp,
        MadeDiscovery,
        SawHelpDiscoverySystem,
        SwappedLoadouts,
        SwitchedRewritePath,
        SawHelpMultipleRewritePaths,
        BuyActiveSkill,
        SawHelpSkillsCanBeRewritten,
        SawHelpLockOn,
        SawDifferentPropulsionAbilityHelp,
        RewroteFateCoreToCommon,
        SawRepairHelp,
        SawStatDetailsHelp,
        SawSlipdriveHelp,
        SawCodexDescription,
        SawInputHelpChoosingPassiveSkills,
        SawItemCompareHelp,
        LootedCredits,
        LootedHealthSpark,
        SawHoldForPrimaryWeaponFireHelp,
        SawTutorialArchiveHint,
        SawRewriteItemsExtraHint,
        SawHelpFocusFull,
        SawHelpMissionChallengeLevel,
        SawHelpFractureMission,
        LootedEnergySpark,
        LootedSupercharge,
        SawHelpNewCycle,
        SawPilotDialogManualBreakHelp,
        SawBreakButtonHelp,
        SawAptitudeHelp,
        SawLootFilterHelp,
        SawCraftingHelp,
        WasPromptedToBuildChassis,
        WasPromptedToExtractAffix,
        SawHelpMultiLoot,
        SawHelpRetrieveAnalyzableWhileAnalyzing,
        SawHelpFatestorms,
        SawHelpJolgenAssault,
        TotalSteps
    }

    public enum eProfileType
    {
        Unknown,
        Demo,
        FullVersion
    }

    public enum eUnlockableCriterion
    {
        None,
        UnlockedShipDialog,
        UnlockedPilotDialog,
        UnlockedNavMapDialog,
        UnlockedCodexDialog,
        UnlockedRewriting,
        RepairedShip,
        CanSeeAnomalies,
        AllowAlternateSpecializationSubtypeDrops,
        UnlockedSlipdrive,
        UnlockedDiscoveries,
        UnlockedKillStreaks,
        UnlockedBounties,
        UnlockedCrafting,
        UnlockedRandomMissions,
        LootedFateCore,
        EquippedFateCore,
        ReadyToRewriteFateCore,
        RewroteFateCoreToCommon,
        ReadyToEquipFateCore,
        RewroteFateCoreToUncommon,
        UnlockDerelictMaps,
        UnlockPlanetsideMaps,
        RescuedScientist,
        StationZeroActivated,
        RewroteTheUniverse,
        RewroteScientist,
        BeginJolgenAssault,
        UnlockSandwormMaps,
        UnlockedFatestorms,
        UnlockMissionRewriting,
        UnlockFacilityMaps,
        UnlockVoinerMaps
    }

    public enum eExpertGoalTracker
    {
        None,
        NumTimesDamagedByWarshipRailgun,
        NumTimesDamagedByColossusReactor,
        NumRaiderCapacitorsDestroyed,
        NumMelterDronesDestroyed,
        NumTimesProximityDecloaked,
        NumTorpedoesDestroyed,
        HighestKillStreak,
        NumNexusWarshipsDestroyed,
        NumNexusMothershipsDestroyed,
        NumTimesDamagedByResonantAsteroids,
        NumTimesHitByFateBeam,
        NumSuperlaserAttacksSurvived,
        NumAnnihilatorsDestroyedWhileIlluminated,
        ShipDamageSustainedWhileScanningFacility,
        NumVoinerDreadnaughtsDestroyed,
        NumTimesRescueTargetDisrupted,
        NumEchoesDestroyedSimultaneously,
        NumCommerceHubsDestroyed,
        NumSandwormWarshipKills,
        NumTetherMinesExploded,
        NumOverlordsDestroyed
    }



    public enum eRarity
    {
        Junk,
        Common,
        Uncommon,
        Rare,
        Epic,
        Artifact,
        Ultimate
    }

    public enum eShipClass
    {
        Fighter,
        Warship,
        CapitalShip,
        BaseStar
    }


    #region Items
    public enum eItemType
    {
        Equipment,
        Resource,
        Fragment,
        Chassis,
        Unspecified
    }

    public enum eEquipmentType
    {
        None,
        Weapon,
        Armor,
        Shields,
        Generator,
        Propulsion,
        CPU,
        Sensors,
        Stabilizers,
        Drone,
        FATECore,
        Mission
    }

    public enum eSpecializationSubtype
    {
        Main,
        Alternate
    }

    #endregion

    #region Rewrite
    public enum eItemRewriteType
    {
        AffixType,
        AffixValue,
        Rarity,
        RequiredShipClass,
        RequiredLevel,
        UniqueModifierValue,
        StatCostValue,
        UniqueModifierStatType,
        Drawback,
        ItemUsable,
        EquipmentType,
        AffixOperator,
        Crafting
    }

    public enum eSkillRewriteType
    {
        Cooldown,
        MaxLevel,
        Rarity,
        Duration,
        Cost,
        Effect,
        Drawback,
        Automate
    }



    #endregion

    #region Missions
    public enum eMissionState
    {
        Inactive,
        Active
    }

    public enum eMissionTargetType
    {
        Character,
        Structure,
        Item
    }
    #endregion


    public enum eItemAffixType
    {
        Power,
        CriticalChance,
        CriticalDamage,
        Armor,
        Sockets,
        Structure,
        Barrier,
        HealthMultiplier,
        HealthRegen,
        CooldownRate,
        SiphonHealth,
        LootQuality,
        Hacking,
        MissionTargetDamage,
        MaxEnergy,
        EnergyRegen,
        Acceleration,
        Twinbolt,
        EliteDamage,
        Chain,
        SiphonEnergy,
        PilotSkillBenefit,
        WeaponPowerSourceBenefit,
        WeaponSpecializationBenefit,
        ItemAbilityCooldown,
        FateStabilization,
        Ambush,
        HomeTurf,
        Barrage,
        Skillmaster,
        Extractor,
        Inciter,
        Agitator,
        Magnify,
        Coherence,
        Maestro,
        Sublimation,
        Executioner,
        Awareness,
        Devastator,
        AllSkills,
        BlastRadius,
        DamageReduction
    }

    public enum eAffixOperator
    {
        Add,
        Multiply
    }

    public enum eCurrencyType : int
    {
        Credits,
        Fate
    }

    public enum eDrawbackStatus
    {
        Enabled,
        Disabled,
        Reversed
    }

}
