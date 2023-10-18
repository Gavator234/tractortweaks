//=============================================================================

public class tractorbeam_BetterSentinelCockpit : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<TkSceneNodeData>(
			"MODELS/COMMON/SPACECRAFT/SENTINELSHIP/SENTINELCOCKPIT.SCENE.MBIN"
		);
		
		List<int> SortTable = new() {
			mbin.Children.FindIndex(INFO => INFO.Name == "CableSpinnerL"),
			mbin.Children.FindIndex(INFO => INFO.Name == "CableSpinnerR"),
			mbin.Children.FindIndex(INFO => INFO.Name == "SentinelCableL"),
			mbin.Children.FindIndex(INFO => INFO.Name == "SentinelCableR")
		};
		
		SortTable.Sort();
		SortTable.Reverse();
		
		foreach (int i in SortTable) {
			Log.AddInformation($"{i}");
			mbin.Children.RemoveAt(i);
		}
	}

	//...........................................................
}

//=============================================================================
