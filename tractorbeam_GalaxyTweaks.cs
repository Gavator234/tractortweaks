//=============================================================================

public class tractorbeam_GalaxyTweaks : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcGalaxyGlobals>(
			"GCGALAXYGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.IntroFadeInRate			= 0.2f;		//0.5
		mbin.IntroTitleFadeTrigger		= 1;		//3.5
		mbin.IntroTitleHoldTime			= 4;		//3
		
		mbin.StarBlurMaxDistanceFromCamera = 0;		//50
	}

	//...........................................................
}

//=============================================================================
