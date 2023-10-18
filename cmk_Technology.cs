//=============================================================================
// Allow everything to be dismantled e.g. ship photon cannon.
//=============================================================================

public class cmk_Technology : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => GcTechnologyTable());
	}

	//...........................................................

	protected void GcTechnologyTable()
	{
		var mbin = ExtractMbin<GcTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN"
		);	

		foreach( var laser in mbin.Table.FindAll(TECH => TECH.BaseStat.StatsType == StatsTypeEnum.Weapon_Laser) ) {
			if (laser.Core == true) continue;
			laser.UpgradeColour = new(0.286f, 0.82f, 0.165f, 1);
		}

		// allow everything to be dismantled, except damaged slots
		// e.g. SHIPGUN1 - Photon Cannon
		foreach( var tech in mbin.Table ) {
			if( tech.Core && !tech.ID.Value.Contains("DMG"))
				tech.Core = false;
		}
	}
}

//=============================================================================
