using static libMBIN.NMS.GameComponents.GcStatsTypes;

//=============================================================================

public class tractorbeam_MultitoolTweaks : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		NoMoreLaserFlare();
		PlayerGlobals();
		GameplayGlobals();
		TechnologyTables();
		ProjectileTable();
		HudInteractionMarker();
	}
	
	protected void NoMoreLaserFlare() {
		var mbin = ExtractMbin<TkSceneNodeData>(
			"MODELS/EFFECTS/MUZZLE/LASERMUZZLE.SCENE.MBIN"
		);
		
		var index = mbin.Children.FindIndex(INFO => INFO.Name == "Flare");
		
		mbin.Children.RemoveAt(index);
	}
	
	protected void PlayerGlobals() {
		var mbin = ExtractMbin<GcPlayerGlobals>(
			"GCPLAYERGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.AutoAim = false;
		mbin.WeaponLowerDelay		= 10;	//3
		mbin.WeaponHolsterDelay		= 11;	//9999999 broken in echoes, does nothing smh
		mbin.WeaponBobFactorRun		= 0.4f;	//0.2 (used to be the same as walking)
		mbin.BulletClipMultiplier	= 1;	//2
		mbin.FullClipReloadSpeedMultiplier = 1;	//2
	}
	
	protected void GameplayGlobals() {
		var mbin = ExtractMbin<GcGameplayGlobals>(
			"GCGAMEPLAYGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.GunOffsetAggressiveY		= 0.02f;	//0.05
		mbin.GunOffsetY					= 0.02f;	//0
		mbin.GunDownAngle				= 5;		//20
		mbin.OverheatGenerosity			= 1;		//1.05
		mbin.HeatDamageBoost			= 15;		//30
		mbin.OverheatDecay				= 1.5f;		//8
	//	mbin.OverheatCurve.Curve		= TkCurveType.CurveEnum.SmootherStep;	//SmoothInOut
	//	mbin.OverheatColourCurve.Curve	= TkCurveType.CurveEnum.SmootherStep; 	//EaseInExpo
	}
	
	protected GcStatsBonus NewStat(StatsTypeEnum StatInput, float BonusInput = 1, int LevelInput = 1) {
		return new() {
			Stat = new GcStatsTypes {
				StatsType = StatInput
			},
			Bonus = BonusInput,
			Level = LevelInput
		};
	}
	
	protected void TechnologyTables() {
		var mbin = ExtractMbin<GcTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN"
		);
		
		foreach (GcTechnology tech in mbin.Table) {
			//Generally reverting the laser
			if (tech.BaseStat.StatsType == StatsTypeEnum.Weapon_Laser
			    && tech.Chargeable == true) {
				
				tech.Name = "LAZEUS";
				tech.NameLower = "Lazeus";
				
				tech.StatBonuses.Clear();
				tech.StatBonuses = new() {
					NewStat(
						StatsTypeEnum.Weapon_Laser_Mining_Speed, 3),
					NewStat(
						StatsTypeEnum.Weapon_Laser_HeatTime, 5),
					NewStat(
						StatsTypeEnum.Weapon_Laser_Damage, 25),
					NewStat(
						StatsTypeEnum.Weapon_Laser_ReloadTime, 1),
					NewStat(
						StatsTypeEnum.Weapon_Laser_Drain, 1.2f),
					NewStat(
						StatsTypeEnum.Weapon_Laser_MiningBonus, 2)
				};
			}
			
			//Bolt no longer bursts, steady stream of boollet
			if (tech.ID.Value == "BOLT") {
				
				tech.Name = "REZOSU";
				tech.NameLower = "Rezosu";
				
				tech.StatBonuses.Clear();
				tech.StatBonuses = new() {
					NewStat(
						StatsTypeEnum.Weapon_Projectile),
				    NewStat(
						StatsTypeEnum.Weapon_Projectile_Damage, 180),
				    NewStat(
				    	StatsTypeEnum.Weapon_Projectile_Range, 1200),
				    NewStat(
				    	StatsTypeEnum.Weapon_Projectile_Rate, 4.5f),
				    NewStat(
				    	StatsTypeEnum.Weapon_Projectile_ClipSize, 32),
				    NewStat(
				    	StatsTypeEnum.Weapon_Projectile_Recoil, 200),
				    NewStat(
				    	StatsTypeEnum.Weapon_Projectile_ReloadTime, 1),
				    NewStat(
				    	StatsTypeEnum.Weapon_Projectile_Dispersion, 2),
				    NewStat(
				    	StatsTypeEnum.Weapon_Projectile_MaximumCharge, 1)
				};
			}
			
			//Do the same for upgrades or it will inherit burst from them
			if (tech.ID.Value.Contains("BOLT") && !tech.Chargeable) {
				tech.StatBonuses.ForEach(INFO => {
					if (INFO.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_BurstCap)
						INFO.Stat.StatsType = StatsTypeEnum.Unspecified;
					if (INFO.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_BurstCooldown)
						INFO.Stat.StatsType = StatsTypeEnum.Unspecified;
				});
			}
			
			//This bursts like it was originally supposed to
			if (tech.ID.Value == "SMG") {
				tech.StatBonuses[2].Bonus = 6;		//ProjectileRate
				tech.StatBonuses[3].Bonus = 3;		//ProjectileDispersion
				tech.StatBonuses[6].Bonus = 2;		//BulletsPerShot
				tech.StatBonuses[7].Bonus = 8;		//ProjectileRecoil
				//It even already has burst stats, they're just set to zero by default smh
				tech.StatBonuses[8].Bonus = 3;		//BurstCap
				tech.StatBonuses[9].Bonus = 0.66f;	//BurstCooldown
			}
		}
	
		var proctable = ExtractMbin<GcProceduralTechnologyTable> (
			"METADATA/REALITY/TABLES/NMS_REALITY_GCPROCEDURALTECHNOLOGYTABLE.MBIN"
		);
		
		foreach (GcProceduralTechnologyData tech in proctable.Table)
		{
			if (tech.ID.Value.Contains("_LASER")) {
				tech.StatLevels.ForEach(INFO => {
					if (INFO.Stat.StatsType == StatsTypeEnum.Weapon_Laser_ReloadTime)
						INFO.Stat.StatsType = StatsTypeEnum.Unspecified;
				});
			}
			
			if (tech.ID.Value.Contains("_BOLT")) {
				tech.StatLevels.ForEach(INFO => {
					if (INFO.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_BurstCap)
						INFO.Stat.StatsType = StatsTypeEnum.Unspecified;
					if (INFO.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_BurstCooldown)
						INFO.Stat.StatsType = StatsTypeEnum.Unspecified;
				});
			}
		}
	}
	
	protected void ProjectileTable() {
		var mbin = ExtractMbin<GcProjectileDataTable> (
			"METADATA/PROJECTILES/PROJECTILETABLE.MBIN"
		);
		
		var MiningLaser = mbin.Lasers.Find(INFO => INFO.Id.Value == "PLAYER");
		
		MiningLaser.CanMine = true;
		MiningLaser.MiningHitRate = 1;
	}
	
	//	This only exists so the beam.CanMine immediately above isn't invalidated with the hud saying you need
	//	a terrain manipulator to mine deposits. If it's like one of those atlas pass things, it'll tell
	//	you when you try to interact anyways.
	protected void HudInteractionMarker() {
		var mbin = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/HUDINTERACTIONMARKER.MBIN"
		);
		
		var MainBar = mbin.Children[0] as GcNGuiLayerData;
		var Subheading = MainBar.Children[1] as GcNGuiLayerData;
		var Cost = Subheading.Children[2] as GcNGuiTextData;
		
		Cost.ElementData.IsHidden = true;
	}

	//...........................................................
}

//=============================================================================
