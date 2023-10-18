//=============================================================================

public class tractorbeam_RetroShipTrails : cmk.NMS.Script.ModClass
{
	protected override void Execute() {
	//	ChangeTrail("HOTTRAIL", "GLOWSIDE.DDS");
	//	ChangeTrail("HOTCYANTRAIL", "GLOWSIDE.DDS");
	//	ChangeTrail("HOTREDTRAIL", "GLOWSIDE_RED.DDS");
	//	ChangeTrail("HOTGREENTRAIL", "GLOWSIDE.DDS");
	//	ChangeTrail("HOTGOLDTRAIL", "GLOWSIDE.DDS");
	//	ChangeTrail("HOTORANGETRAIL", "GLOWSIDE_POLICE.DDS");
		ChangeTrail("HOTTRAIL", "GLOWSIDEADD.DDS");
		ChangeTrail("HOTCYANTRAIL", "GLOWSIDEADD.DDS");
		ChangeTrail("HOTREDTRAIL", "GLOWSIDEADD.DDS");
		ChangeTrail("HOTGREENTRAIL", "GLOWSIDEADD.DDS");
		ChangeTrail("HOTGOLDTRAIL", "GLOWSIDEADD.DDS");
		ChangeTrail("HOTORANGETRAIL", "GLOWSIDEADD.DDS");
		
		TrailWidth();
	}
	
	//...........................................................
	
	protected void ChangeTrail(string path, string texture) {
		var Material = ExtractMbin<TkMaterialData>("MODELS/EFFECTS/TRAILS/SPACECRAFT/HOT/" + path + ".MATERIAL.MBIN");
		string TexturePath = "TEXTURES/EFFECTS/TRAILS/" + texture;
		
		Log.AddInformation($"Trail changed to {TexturePath}");
		TkMaterialSampler Sampler = Material.Samplers.Find(info => info.Name.Value == "gDiffuseMap");
		Sampler.Map.Value = TexturePath;
	}
	
	//...........................................................
	
	protected void TrailWidth() {
		var Trail = ExtractMbin<TkTrailData>("MODELS/EFFECTS/TRAILS/SPACECRAFT/HOT/HOTTRAIL.TRAIL.MBIN");
		Trail.Width = 2;
	}
}

//=============================================================================
