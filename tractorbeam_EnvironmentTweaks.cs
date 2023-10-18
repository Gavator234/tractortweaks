//=============================================================================

public class tractorbeam_EnvironmentTweaks : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		EnvironmentGlobals();
	}
	
	protected void EnvironmentGlobals() {
		var mbin = ExtractMbin<GcEnvironmentGlobals> (
			"GCENVIRONMENTGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.AtmosphereSpaceRadius *= 2;	//10800
		
		foreach (var Setting in mbin.LODSettings)
			Setting.MaxAsteroidGenerationPerFramePulseJump = Setting.MaxAsteroidGenerationPerFrame;
	}

	//...........................................................
}

//=============================================================================
