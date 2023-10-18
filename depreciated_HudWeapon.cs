//=============================================================================

public class depreciated_HudWeapon : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		//	Old script, it's a mess
		Hud();
		HudWeapons();
		HudBoarder();
		PlayerGlobals();
		UIGlobals();
	}
	
	protected void Hud() {
		
		var mbin = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/HUD.MBIN"
		);
		
		//Not needed? Zero in latest ver but not in fractal build, so I will leave it here
		var Weapon = mbin.Children[2] as GcNGuiLayerData;
		Weapon.ElementData.Layout.PositionY = 0;
		
		Weapon.Style.Animate = TkNGuiGraphicStyle.AnimateEnum.SimpleWipeDown;
	}
	
	protected void HudWeapons() {
		var mbin = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/HUDWEAPONS.MBIN"
		);
		
		mbin.ElementData.Layout.PositionX	= 100;	//103, otherwise the graphic goes off the edge
		
		//	Thermal load percentages and charge percentages are no longer required, relying on the
		//	"OVERHEAT WARNING" pop up is more than enough. Also getting rid of the control hints
		//	and the pips showing you what weapon you're using, of no use in the 2014 hud.
		var Root					= mbin.Children[0] as GcNGuiLayerData;
		var		Overheat			= Root.Children[1] as GcNGuiLayerData;
		var			OverheatBar		= Overheat.Children[0] as GcNGuiGraphicData;
		var 	Stats				= Root.Children[2] as GcNGuiLayerData;
		var			WeaponSlash		= Stats.Children[0] as GcNGuiGraphicData;
		var			WeaponSlashLine	= Stats.Children[1] as GcNGuiGraphicData;
		var			AmmoReservoir	= Stats.Children[3] as GcNGuiTextData;
		var			AmmoClip		= Stats.Children[4] as GcNGuiTextData;
		var			Icon			= Stats.Children[5] as GcNGuiLayerData;
		var				Icon2		= Icon.Children[0] as GcNGuiLayerData;
		var					Icon3	= Icon2.Children[1] as GcNGuiGraphicData;
		var 		Charge			= Stats.Children[6] as GcNGuiTextData;
		var			WeaponName		= Stats.Children[7] as GcNGuiTextData;
		var 		ThermalLoad		= Stats.Children[8] as GcNGuiLayerData;
		var		WeaponLabel			= Root.Children[3] as GcNGuiLayerData;
		var			WeaponSlashAlt	= WeaponLabel.Children[0] as GcNGuiGraphicData;
		var			AltWeapon		= WeaponLabel.Children[1] as GcNGuiTextData;
		var 	WeaponPips			= Root.Children[4] as GcNGuiLayerData;
		var 	SwitchWeaponHint	= Root.Children[5] as GcNGuiTextData;
		var 	SwitchAltWeaponHint	= Root.Children[6] as GcNGuiTextData;
		var		Wanted				= Root.Children[7] as GcNGuiLayerData;
		
		WeaponLabel			.ElementData.IsHidden = true;
		
		WeaponSlash			.ElementData.IsHidden = true;
		WeaponSlashLine		.ElementData.IsHidden = true;
		AmmoClip			.ElementData.IsHidden = true;
		Charge				.ElementData.IsHidden = true;
		ThermalLoad			.ElementData.IsHidden = true;
		
		WeaponSlashAlt		.ElementData.IsHidden = true;
		AltWeapon			.ElementData.IsHidden = true;
		Wanted				.ElementData.IsHidden = true;
		
		WeaponPips			.ElementData.IsHidden = true;
		//SwitchWeaponHint	.ElementData.IsHidden = true;		Doesn't work, the exe will un-hide them
		//SwitchAltWeaponHint	.ElementData.IsHidden = true;
		SwitchWeaponHint.ElementData.Layout.PositionX = -900;	//This oughta do it.
		SwitchWeaponHint.ElementData.Layout.PositionY = -900;
		SwitchAltWeaponHint.ElementData.Layout.PositionX = -900;
		SwitchAltWeaponHint.ElementData.Layout.PositionY = -900;
		
		//That's the end of hiding things, now to change the things you *will* see
		
		Overheat.ElementData.Layout.Align.Vertical		= TkNGuiAlignment.VerticalEnum.Top;
		Overheat.ElementData.Layout.Align.Horizontal	= TkNGuiAlignment.HorizontalEnum.Left;
		Overheat.ElementData.Layout.AnchorPercent		= false;
		Overheat.ElementData.Layout.WidthPercentage		= false;
		Overheat.ElementData.Layout.HeightPercentage	= false;
		Overheat.ElementData.Layout.PositionX			= 407;
		Overheat.ElementData.Layout.PositionY			= 132;
		Overheat.ElementData.Layout.Width				= 280;
		Overheat.ElementData.Layout.Height				= 43;
		Overheat.Style.Default.Colour		= new(0f, 0f, 0f, 0f);
		
		OverheatBar.ElementData.Layout.Align.Vertical	= TkNGuiAlignment.VerticalEnum.Top;
		OverheatBar.ElementData.Layout.Align.Horizontal	= TkNGuiAlignment.HorizontalEnum.Left;
		OverheatBar.ElementData.Layout.PositionX		= -2;
		OverheatBar.ElementData.Layout.HeightPercentage	= true;
		OverheatBar.ElementData.Layout.Height			= 100;
		OverheatBar.Style.Default.Colour	= new(0f, 0f, 0f, 0.1f);
		
		Root.Style.Animate					= TkNGuiGraphicStyle.AnimateEnum.SimpleWipeDown;
		Root.ElementData.Layout.PositionX	= 1515;
		Root.ElementData.Layout.PositionY	= -20;
		
		Stats.ElementData.Layout.AnchorPercent = false;
		Stats.ElementData.Layout.PositionX	= 748;
		Stats.ElementData.Layout.PositionY	= 29;
		Stats.ElementData.Layout.Width		= 400;
		Stats.ElementData.Layout.Height		= 125;
		
		AmmoReservoir.Style.Default.Colour = new() {
			R = 0,
			G = 0,
			B = 0,
			A = 1
		};
		AmmoReservoir.Style.Default.FontSpacing		= -5;
		AmmoReservoir.Style.Default.HasDropShadow	= false;
		AmmoReservoir.ElementData.Layout.AnchorPercent = false;
		AmmoReservoir.ElementData.Layout.PositionX	= 210;
		AmmoReservoir.ElementData.Layout.PositionY	= 44;
		AmmoReservoir.ElementData.Layout.Width		= 92;
		AmmoReservoir.ElementData.Layout.Height		= 46;
		
		Icon.ElementData.Layout.AnchorPercent	= false;
		Icon.ElementData.Layout.ConstrainProportions = false;
		Icon.ElementData.Layout.PositionX		= 212;
		Icon.ElementData.Layout.PositionY		= 47;
		Icon.ElementData.Layout.Width			= 141;
		Icon.ElementData.Layout.Height			= 41;
		
		Icon2.ElementData.Layout.AnchorPercent	= false;
		Icon2.ElementData.Layout.ConstrainProportions = false;
		Icon2.ElementData.Layout.PositionX			= 0;
		Icon2.ElementData.Layout.PositionY			= 0;
		Icon2.ElementData.Layout.Width				= 40;
		Icon2.ElementData.Layout.WidthPercentage	= true;
		Icon2.ElementData.Layout.Height				= 100;
		Icon2.ElementData.Layout.HeightPercentage	= true;
		
		Icon3.Style.Default.Colour					= new() {
			R = 0,
			G = 0,
			B = 0,
			A = 1
		};
		//I could probably make a higher rez version if I wanted to.
		Icon3.Image = "TEXTURES/UI/HUD/LASERICON.DDS";
		Icon3.ElementData.Layout.AnchorPercent		= true;
		Icon3.ElementData.Layout.ConstrainProportions = false;
		Icon3.ElementData.Layout.PositionX			= 50;
		Icon3.ElementData.Layout.PositionY			= 50;
		Icon3.ElementData.Layout.Width				= 50;
		Icon3.ElementData.Layout.WidthPercentage	= false;
		Icon3.ElementData.Layout.Height				= 50;
		Icon3.ElementData.Layout.HeightPercentage	= false;
		
		WeaponName.Style.Default.Colour					= new() {
			R = 0,
			G = 0,
			B = 0,
			A = 1
		};
		WeaponName.Style.Default.FontIndex				= 1;
		WeaponName.Style.Default.FontSpacing			= 9;
		WeaponName.Style.Default.Align.Horizontal		= TkNGuiAlignment.HorizontalEnum.Center;
		WeaponName.Style.Default.Align.Vertical			= TkNGuiAlignment.VerticalEnum.Middle;
		WeaponName.Style.Default.HasDropShadow			= false;
		WeaponName.Style.Default.HasOutline				= false;
		WeaponName.Style.Default.ForceUpperCase			= true;		//Does nothing :(
		WeaponName.ElementData.Layout.AnchorPercent 	= false;
		WeaponName.ElementData.Layout.PositionX			= 350;
		WeaponName.ElementData.Layout.PositionY			= 2.5f;
		WeaponName.ElementData.Layout.Width				= 364;
		WeaponName.ElementData.Layout.WidthPercentage	= false;
		WeaponName.ElementData.Layout.Height			= 37;
		WeaponName.ElementData.Layout.HeightPercentage	= false;
	}
	
	protected void HudBoarder() {
		libMBIN.NMS.Colour DefaultDarkColourData = new() {
			R = 0,
			G = 0,
			B = 0,
			A = 1
		};
		libMBIN.NMS.Colour DefaultLightColourData = new() {
			R = 1,
			G = 1,
			B = 1,
			A = 1
		};
		
		TkNGuiGraphicStyleData DefaultStyleData = new() {
			Colour					= DefaultLightColourData,
			IconColour				= DefaultLightColourData,
			StrokeColour			= DefaultLightColourData,
			StrokeGradientColour	= DefaultLightColourData,
			GradientColour			= DefaultDarkColourData,
			SolidColour = true
		};
		
		GcNGuiGraphicData WeaponBoarder = new() {
			ElementData = new GcNGuiElementData {
				ID			= "WEAPONBOARDER",
				IsHidden	= false,
				Layout = new GcNGuiLayoutData {
					Anchor			= true,
					AnchorPercent	= true,
					PositionX		= 100,
					PositionY		= 0,
					Width			= 470,
					Height			= 247.3684f,
					ConstrainAspect = 1,
					Align = new TkNGuiAlignment {
						Vertical	= TkNGuiAlignment.VerticalEnum.Top,
						Horizontal	= TkNGuiAlignment.HorizontalEnum.Right
					}
				}
			},
			Style = new TkNGuiGraphicStyle {
				Default = DefaultStyleData,
				Highlight = DefaultStyleData,
				Active = DefaultStyleData,
				CustomMinStart = new(1, 1),
				CustomMaxStart = new(1, 1),
				HighlightTime = 0.1f,
				HighlightScale = 1,
				GlobalFade = 1,
				AnimTime = 0.5f,
				AnimSplit = 0.4f
			},
			Image = "TEXTURES/UI/HUD/WEAPONBOARDER.DDS"
		};
		
		var mbin = ExtractMbin<GcNGuiLayerData> (
			"UI/HUD/HUDWEAPONS.MBIN"
		);
		
		var Root = mbin.Children[0] as GcNGuiLayerData;
		
		Root.Children.Insert(0, WeaponBoarder);
	}
	
	protected void PlayerGlobals() {
		var mbin = ExtractMbin<GcPlayerGlobals>(
			"GCPLAYERGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.ShowLowAmmoWarning = false;
	}
	
	protected void UIGlobals() {
		var mbin = ExtractMbin<GcUIGlobals>(
			"GCUIGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.PulseDamageColour = new(0f, 0f, 0f, 0.1f);
		mbin.PulseAlertColour = new(0f, 0f, 0f, 0.1f);
	}

	//...........................................................
}

//=============================================================================
