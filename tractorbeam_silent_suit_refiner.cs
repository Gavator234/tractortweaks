//=============================================================================

public class tractorbeam_silent_suit_refiner : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<TkAttachmentData>(
			"MODELS/COMMON/PLAYER/PLAYERCHARACTER/PLAYERCHARACTER/ENTITIES/PLAYERCHARACTER.ENTITY.MBIN"
		);
		
		var component = mbin.Components[17] as GcTriggerActionComponentData;
		var state = component.States[1] as GcActionTriggerState;
		var trigger = state.Triggers[0] as GcActionTrigger;
		trigger.Action.RemoveAt(6);
	}

	//...........................................................
}

//=============================================================================
