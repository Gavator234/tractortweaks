//=============================================================================

public class tractorbeam_PlayerTweaks : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		ScanDataTable();
		
		UIGlobals();
		DebugOptions();
		GameplayGlobals();
		PlayerGlobals();
		AudioGlobals();
		GraphicsGlobals();
		EnvironmentGlobals();
		SkyGlobals();
		PulseXML();
		
		HUDTexturePrefetch();
		ShaderPipeline();
	}
	
	protected void PulseXML() {
		var xml = ExtractData<NMS.PAK.XML.Data>(
			"MUSIC/PULSE.XML"
		);
		
		string data = xml.Text;
		
		data = data.Replace("<string>Planet</string>", "");
		data = data.Replace("<string>Space</string>", "");
		
		xml.Text = data;
	}
	
	protected void SkyGlobals() {
		var mbin = ExtractMbin<GcSkyGlobals>(
			"GCSKYGLOBALS.GLOBALS.MBIN"
		);
		
		mbin.DayLength *= 10;		//1800
		mbin.SunClampAngle = 85;	//55	//This will cause problems, Terrain LODs next to the lowest don't make shadows and so it'll clip over hills. Eh whatever.
	}
	
	protected void UIGlobals() {
		var mbin = ExtractMbin<GcUIGlobals>(
			"GCUIGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.ReplaceItemBarWithNumbers = false;
		mbin.MaxSubstanceMaxAmountForAmountFraction = 1001; //1000
	}
	
	protected void ShaderPipeline() {
		var data = ExtractData<NMS.PAK.BIN.Data>(
			"PIPELINES/PIPELINEDEFERRED.BIN"
		);
		var text = data.Text;
		
		while (text.Contains("CLOUDS_HIGH")) {
			
			int indexOfText = text.IndexOf("CLOUDS_HIGH");
			int i = 0;
			
			//find the index of the newline before it
			char iteratorChar = '\0';
			int PreviousNewlineIndex = 0;
			while (iteratorChar != '\n') {
				iteratorChar = text[indexOfText - i];
				PreviousNewlineIndex = indexOfText - i;
				i++;
			}
			
			//find the num of chars before it hits another newline
			iteratorChar = '\0';
			int LineLength = 0;
			i = 1;
			while (iteratorChar != '\n') {
				iteratorChar = text[PreviousNewlineIndex + i];
				LineLength = i;
				i++;
			}
			
			text = text.Remove(PreviousNewlineIndex, LineLength);
		}
		
		//Remove the CloudsHigh stage
		int indexBefore = text.IndexOf("<Stage id=\"CloudsHigh\">");
		
		int length = 0;
		string iteratorString = "";
		while (!iteratorString.Contains("</Stage>")) {
			iteratorString = text.Substring(indexBefore, length);
			length++;
		}
		
		text = text.Remove(indexBefore, length);
		
		data.Text = text;
	}
	
	protected void ScanDataTable() {
		var mbin = ExtractMbin<GcScanDataTable> (
			"METADATA/SIMULATION/SCANNING/SCANDATATABLE.MBIN"
		);
		
		var Tool = mbin.ScanData.Find(INFO => INFO.ID == "TOOL");
		var ToolHard = mbin.ScanData.Find(INFO => INFO.ID == "TOOL_HARD");
		
		var scale = 2;
		
		Tool.ScanData.PulseTime		*= scale;	//1
		ToolHard.ScanData.PulseTime	*= scale;	//1
	}
	
	protected void DebugOptions() {
		var mbin = ExtractMbin<GcDebugOptions>(
			"GCDEBUGOPTIONS.GLOBAL.MBIN"
		);
		
		/*
		mbin.SkipUITimers				= true;
		mbin.FastLoad					= true;
		mbin.BootMode					= BootModeEnum.SolarSystem;
		mbin.BootLoadDelay				= GcDebugOptions.BootLoadDelayEnum.WaitForNothing;
		*/
		
		mbin.ThirdPersonIsDefaultCameraForPlayer = false;
		mbin.DisableProfanityFilter		= true;		//*Grins immaturely*
		mbin.DisableLimits				= true;
		mbin.GenerateFarLodBuildingDist	= 100000;	//1000
	}
	
	protected void GameplayGlobals() {
		var mbin = ExtractMbin<GcGameplayGlobals>(
			"GCGAMEPLAYGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.GoalGapVoxelDist = 1;		//8
		
		mbin.TorchFollowCameraTime = 0.05f;
		
		var fov = 45;
		var stn = 10;
		
		mbin.TorchFoV						= fov;
		mbin.TorchDimFoV					= fov;
		mbin.InteractionTorchFoV			= fov;
		mbin.UndergroundTorchFoV			= fov;
		mbin.UndergroundTorchFoVFar			= fov;
		mbin.TorchStrength					= stn;
		mbin.TorchDimStrength				= stn;
		mbin.InteractionTorchStrength		= stn;
		mbin.UndergroundTorchStrength		= stn;
		mbin.UndergroundTorchStrengthFar	= stn;
	}
	
	protected void PlayerGlobals() {
		var mbin = ExtractMbin<GcPlayerGlobals>(
			"GCPLAYERGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.ShieldRechargeMinTimeSinceDamage = 15;		//30
		mbin.ShieldRechargeRate			= 1;			//10
		mbin.HealthPipRechargeRate		= 1;			//200
		mbin.HitReactNoiseAmount		= 1;			//0.7
		mbin.HitReactBlendOutSpeedMin	= 20;			//5, same as max
		
	//	Depreciated
	//	mbin.HUDHeightPosX			= 600;	//500
	//	mbin.HUDHeightPosY			= 300;	//360
		
		mbin.MaxFallSpeed			= float.MaxValue;	//30
		mbin.HardLandTime			= 1;	//0.5
		mbin.HelmetLag				= 2;	//0.02
		mbin.HelmetMaxLag			= 5;	//0.05
	}
	
	protected void AudioGlobals() {
		var mbin = ExtractMbin<GcAudioGlobals>(
			"GCAUDIOGLOBALS.GLOBAL.MBIN"
		);
	
		mbin.CommsChatterFalloffFreighers	= new(1000, 10000);
		mbin.CommsChatterFalloffShips		= new( 500,  5000);
	}
	
	protected void GraphicsGlobals() {
		var mbin = ExtractMbin<GcGraphicsGlobals>(
			"GCGRAPHICSGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.SunLightIntensity = 4;				//3
		mbin.ShadowMapSize				= 4096;	//1024
		mbin.TargetTextureMemUsageMB	= 4096;	//1280
	}
	
	protected void HUDTexturePrefetch() {
		var mbin = ExtractMbin<GcTexturePrefetchData>(
			"METADATA/UI/HUDTEXTUREPREFETCH.MBIN"
		);
		
		mbin.Textures.Clear();

		IPakItemCollection.ForEachInfo((INFO, LOG, CANCEL) => {
			if(	!INFO.Path.Full.StartsWith("TEXTURES/UI") ||
		    	!INFO.Path.Full.EndsWith(".DDS")
		    )	return;
		    
		    mbin.Textures.AddUnique(INFO.Path.Full);
		});
	}
	
	protected void EnvironmentGlobals()
	{
		var mbin = ExtractMbin<GcEnvironmentGlobals>(
			"GCENVIRONMENTGLOBALS.GLOBAL.MBIN"
		);
		mbin.CloudProperties.AnimationScale = 16;  // 50, slower clouds
	}
	//...........................................................
}

//=============================================================================
