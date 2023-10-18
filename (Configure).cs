//=============================================================================

public class Configure : cmk.NMS.Script.ModClass
{
	
	protected override void Execute()
	{
		Presets();

		//Set to false if debugging.
		if (true) {
			Execute<assets_ArghWater>();
			Execute<assets_ayymang_PrereleaseAtmospheres>();
			Execute<assets_BackToFoundations_Terrain>();
			Execute<assets_WhoTnT_MetallicMaterialsFix>();
			
			Execute<cmk_Lang_Norm_Names>();
			Execute<cmk_Lod>();
			Execute<cmk_Technology>();
			
			Execute<depreciated_HudHealth>();
			Execute<depreciated_HudWeapon>();
			Execute<tractorbeam_E3Hud>();
			Execute<tractorbeam_PlayerTweaks>();
			Execute<tractorbeam_MultitoolTweaks>();
			Execute<tractorbeam_SentinelTweaks>();
			Execute<tractorbeam_EnvironmentTweaks>();
			Execute<tractorbeam_GalaxyTweaks>();
			Execute<tractorbeam_ShipTweaks>();
			Execute<tractorbeam_RetroShipTrails>();
			Execute<tractorbeam_silent_suit_refiner>();
		} else {
		//	Execute<assets_vitalised_shadertest>();
			Execute<depreciated_HudHealth>();
			Execute<depreciated_HudWeapon>();
			Execute<tractorbeam_E3Hud>();
		}
		
		Postsets();
	}

	//...........................................................

	protected void Presets() {
		foreach( var script in ModFiles.ModCollection ) {
			script.IsExecutable = false;
		}
	}

	//...........................................................

	protected void Postsets() {}
}

//=============================================================================
