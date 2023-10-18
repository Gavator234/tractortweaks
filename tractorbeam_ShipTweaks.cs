//=============================================================================

public class tractorbeam_ShipTweaks : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
	//	PlayerGlobals();
		AISpaceshipGlobals();
		SpaceshipGlobals();
		GameplayGlobals();
		ScanDataTable();
		CameraGlobals();
		
		PhotonOverheatNoiseReturns();
		NoFakeRocksMiniJump();
		NoAmbientNebulaEncounters();
		SpaceHeavyAir();
		NoMoreSpeedTunnel();
	}
	
	protected void PlayerGlobals() {
		var mbin = ExtractMbin<GcPlayerGlobals>(
			"GCPLAYERGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.MaxNumShipsAttackingPlayer = 5;	//3
	}
	
	protected void AISpaceshipGlobals() {
		var mbin = ExtractMbin<GcAISpaceshipGlobals>(
			"GCAISPACESHIPGLOBALS.GLOBAL.MBIN"
		);

		mbin.PlayerSquadronConfig.MaxShipsInFormationDuringCombat	= 0;
		
		
		//They ain't despawning, not on my watch.
		mbin.MaxNumActivePolice			= int.MaxValue;
		mbin.MaxNumActiveTraders		= int.MaxValue;
		mbin.MaxNumActivePoliceRadius	= float.MaxValue;
		mbin.MaxNumActiveTraderRadius	= float.MaxValue;
		mbin.VisibleDistance			= float.MaxValue;
		
		mbin.OutpostDockMaxApproachSpeed	= 20;		//400
		mbin.OutpostDockOverspeedBrake		= 0.1f;		//30
		mbin.DockingLandingTime				= 2.25f;	//1.6
		mbin.DockingLandingTimeDirectional	= 2.25f;	//1
		mbin.DockingRotateSpeed				= 1;		//0.5
		
		mbin.AtmosphereEffectEnabled	= true;
		
		mbin.AttackShipsFollowLeader	= true;
		mbin.PirateExtraDamage			= 1;			//1.5
	}
	
	protected void SpaceshipGlobals() {
		var mbin = ExtractMbin<GcSpaceshipGlobals>(
			"GCSPACESHIPGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.AutoEjectOnLanding			= true;
		
		mbin.ShieldRechargeMinHitTime	= 0;		//60
		mbin.ShieldRechargeRate			= 0;		//6
		
		mbin.CockpitExitAnimOffset		= 0;		//0.3
		mbin.MiniWarpMarkerApproachSlowdown = 1f;	//0.5
		mbin.MiniWarpMarkerAlignSlowdown = 1;		//0.8
		mbin.MiniWarpStationRadius		= 1500;		//700
		mbin.MiniWarpPlanetRadius		= 1500;		//500
		mbin.MiniWarpShakeStrength		= 1;		//2
		mbin.MiniWarpSpeed				= 30000;	//30000, default
		mbin.MiniWarpTopSpeedTime		= 0.5f;		//0.1
		mbin.MiniWarpFlashIntensity 	= 0.5f;		//0.9
		mbin.MiniWarpFlashDuration		= 0.65f;	//0.9
		mbin.MiniWarpExitSpeedStation	= 2000;		//500
		mbin.MiniWarpExitSpeed			= 2000;		//700
		mbin.CameraPostWarpFovTime		= 0;		//0.1
		
		mbin.Warp.EntryTunnelCurve.Curve = TkCurveType.CurveEnum.ReallySlowOut;
		mbin.Warp.ExitTime				= 0;
		mbin.Warp.ExitTunnelCurve.Curve	= TkCurveType.CurveEnum.Linear;
		
		mbin.LandingButtonMinTime		= 0.01f;	//0.5
		mbin.LandingCurve.Curve			= TkCurveType.CurveEnum.SmootherStep;
		mbin.LandingCurveHeavy.Curve	= TkCurveType.CurveEnum.SmootherStep;
		mbin.HoverLandManeuvreTimeMin	= 2.5f;		//0.7
		mbin.HoverLandManeuvreTimeMax	= 3;		//1.2
		mbin.HoverLandManeuvreTimeHmdMin = mbin.HoverLandManeuvreTimeMin;
		mbin.HoverLandManeuvreTimeHmdMax = mbin.HoverLandManeuvreTimeMax;
		
		mbin.MuzzleLightIntensity		= 0;		//9
		mbin.LaserFireTime				= float.MaxValue;	//10, now respects upgrades
		mbin.DamageMinHitTime			= 0;		//0.2
		mbin.DamageMaxHitTime			= 0;		//2.5
		mbin.DamageMinWoundTime			= 0;		//2.5
		
		mbin.GroundHeightSmoothTime = float.MaxValue;	//0, can now fly underwater

		//Taken from cmk's example "Ship" script
		mbin.NearGroundPitchCorrectMinHeight = 1;  // 23
		mbin.NearGroundPitchCorrectRange     = 1;  // 15
		mbin.NearGroundPitchCorrectMinHeightRemote = 3;  // 30
		mbin.NearGroundPitchCorrectRangeRemote     = 3;  // 30
		
		mbin.PitchCorrectSoftDownAngle = 360;  // 25
		mbin.PitchCorrectMaxDownAngle  = 360;  // 50
		mbin.PitchCorrectMaxDownAnglePostCollision  = 360;  //  10
		mbin.PitchCorrectSoftDownAnglePostCollision = 360;  // -10
		mbin.PitchCorrectMaxDownAngleWater          = 360;  //  20
	
		mbin.GroundHeightSoft      =  7;  // 20
		mbin.GroundHeightSoftForce = 14;  // 35

		mbin.LandHeightThreshold     = 60;  // 100
		mbin.LandingPushNoseUpFactor =  0;  // 0.15
		//end of cmk's code
		
		//Just adjusting the thrust speed to make it closer to the trailers
		mbin.BoostChargeRate		= 1;	//2.5
		
		ChangeSpeed(mbin.Control);
		ChangeSpeed(mbin.ControlLight);
		ChangeSpeed(mbin.ControlHeavy);
		ChangeSpeed(mbin.ControlHover);
	}
	
	void ChangeSpeed(GcPlayerSpaceshipControlData ControlData) {
			
		GcPlayerSpaceshipEngineData[] EngineData = new GcPlayerSpaceshipEngineData[4];
		EngineData[0] = ControlData.SpaceEngine;
		EngineData[1] = ControlData.PlanetEngine;
		EngineData[2] = ControlData.CombatEngine;
		EngineData[3] = ControlData.AtmosCombatEngine;
		
		foreach (GcPlayerSpaceshipEngineData data in EngineData) {
			data.BoostThrustForce	/= 2;
			data.ThrustForce		/= 3;
			data.MinSpeed			= 0.01f;
		}
	}
	
	protected void GameplayGlobals() {
		var mbin = ExtractMbin<GcGameplayGlobals>(
			"GCGAMEPLAYGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.SpaceSpeedReadoutMultiplier	= 1;	//2
		mbin.CombatSpeedReadoutMultiplier	= 1;	//1.5
	}
	
	protected void ScanDataTable() {
		var mbin = ExtractMbin<GcScanDataTable> (
			"METADATA/SIMULATION/SCANNING/SCANDATATABLE.MBIN"
		);
		
		var Ship = mbin.ScanData.Find(INFO => INFO.ID == "SHIP");
		
		Ship.ScanData.PulseRange	*= 100;		//10000
		Ship.ScanData.PulseTime		 = 4;		//3
	}
	
	protected void CameraGlobals() {
		var mbin = ExtractMbin<GcCameraGlobals>(
			"GCCAMERAGLOBALS.GLOBAL.MBIN"
		);
		
		GcCameraShakeData LandShake = mbin.CameraShakeTable.Find(INFO => INFO.Name =="LAND");
		LandShake.TotalTime = 0;
		LandShake.DecayRate = 0;
		LandShake.StrengthScale = 0;
		LandShake.CapturedData.Active = false;
		
		/*
		mbin.WarpSettings.OffsetZFrequency_1	= 0;
		mbin.WarpSettings.OffsetZFrequency_2	= 0;
		mbin.WarpSettings.OffsetZPhase_1		= 0;
		mbin.WarpSettings.OffsetZPhase_2		= 0;
		mbin.WarpSettings.OffsetZStartBias		= -10;
		mbin.WarpSettings.OffsetZBias			= 0;
		mbin.WarpSettings.OffsetZRange			= 0;
		mbin.WarpSettings.OffsetYFrequency_1	= 0;
		mbin.WarpSettings.OffsetYFrequency_2	= 0;
		mbin.WarpSettings.OffsetYPhase_1		= 0;
		mbin.WarpSettings.OffsetYPhase_2		= 0;
		mbin.WarpSettings.OffsetYStartBias		= 3;
		mbin.WarpSettings.OffsetYBias			= 0;
		mbin.WarpSettings.OffsetYRange			= 0;
		mbin.WarpSettings.OffsetXFrequency		= 0;
		mbin.WarpSettings.OffsetXPhase			= 0;
		mbin.WarpSettings.OffsetXRange			= 0;
		mbin.WarpSettings.RollRange				= 0;
		mbin.WarpSettings.YawnRange				= 0;
		*/
	}
	
	protected void PhotonOverheatNoiseReturns() {
		var mbin = ExtractMbin<GcProjectileDataTable>(
			"METADATA/PROJECTILES/PROJECTILETABLE.MBIN"
		);
		
		var ShipGun = mbin.Table.Find(INFO => INFO.Id == "SHIPGUN") as GcProjectileData;
		ShipGun.OverheatAudioEvent.AkEvent = GcAudioWwiseEvents.AkEventEnum.WPN_SHIP_READY;
	}
	
	protected void NoFakeRocksMiniJump() {
		var mbin = ExtractMbin<TkSceneNodeData>(
			"MODELS/EFFECTS/SPEEDLINES/SPEEDLINE.SCENE.MBIN"
		);
		
		var MiniJump2 = mbin.Children.Find(INFO => INFO.Name == "MiniJump2");
		int MiniJump4Index = MiniJump2.Children.FindIndex(INFO => INFO.Name == "MiniJump4");
		
		MiniJump2.Children.RemoveAt(MiniJump4Index);
	}
	
	protected void NoAmbientNebulaEncounters() {
		var mbin = ExtractMbin<GcExperienceSpawnTable>(
			"METADATA/SIMULATION/SCENE/EXPERIENCESPAWNTABLE.MBIN"
		);
		
		mbin.BackgroundSpaceEncounters.Clear();
	}
	
	protected void SpaceHeavyAir() {
		var mbin = ExtractMbin<TkHeavyAirData>(
			"MODELS/EFFECTS/HEAVYAIR/SPACE/SPACE2.HEAVYAIR.MBIN"
		);
		
		mbin.Material = "MODELS/EFFECTS/COMMON/MATERIALS/FLARE.MATERIAL.MBIN";
		mbin.NumberOfParticles		= 400;
		mbin.Radius					= 250;
		mbin.RadiusY				= 200;
		mbin.MinParticleLifetime	= 10;
		mbin.MaxParticleLifetime	= 10;
		mbin.SpeedFadeOutTime		= 1;
		mbin.MaxVisibleSpeed		= 20;
		mbin.SoftFadeStrength		= 2;	//Not in older mbins, may be fine as it is ((5))
		mbin.SpawnRotationRange		= 0;
		mbin.ScaleRange				= new(1, 0.1f, 0);
		mbin.RotationSpeedRange		= new(0, 0, 0);
		mbin.TwinkleRange			= new(1, 0.3f, 0);
		mbin.AmplitudeMin			= new(-0.02f, -0.02f, -0.02f);
		mbin.AmplitudeMax			= new(0.1f, 0.1f, 0.1f);
		mbin.Colour1				= new(1, 1, 1, 1);
		
		var root = ExtractMbin<TkSceneNodeData>(
			"MODELS/EFFECTS/HEAVYAIR/SPACE/SPACE.SCENE.MBIN"
		);
		
		root.Children.RemoveAt(1);	//removes the plasma effect
	}
	
	protected void NoMoreSpeedTunnel() {
		var mbin = ExtractMbin<TkMaterialData> (
			"MODELS/EFFECTS/ENGINES/SPEEDCOOL/TUNNELMAT.MATERIAL.MBIN"
		);
		
		//	Despite this edit the tunnel does still exist because that's how the cockpits are modelled in the files,
		//	however it's much more faint now and that I can put up with.
		var sampler = mbin.Samplers.Find(INFO => INFO.Map == "TEXTURES/EFFECTS/SHIP/BOOSTSCREENEFFECT.DDS");
		
		sampler.Map = "";
	}
	//...........................................................
}

//=============================================================================
