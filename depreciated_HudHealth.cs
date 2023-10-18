//=============================================================================

public class depreciated_HudHealth : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		//	Old script, it's a mess
		
		var mbin = ExtractMbin<GcNGuiLayerData>(
			"/UI/HUD/HUDHEALTH.MBIN"
		);
		
		ShieldBar(mbin);
		HUDHealth(mbin);
		HealthIcons();
		HudHazard();
		Mission();
		
		UIGlobals();
		GameplayGlobals();
		PlayerGlobals();
		GraphicsGlobals();
	}
	
	//...........................................................
	
	protected void HUDHealth(GcNGuiLayerData mbin)
	{
		mbin.ElementData.Layout.AnchorPercent = false;
		
		mbin.ElementData.Layout.PositionX	= 0;
		mbin.ElementData.Layout.PositionY	= -20;
		mbin.ElementData.Layout.Height		= 600;
		mbin.Style.Animate					= TkNGuiGraphicStyle.AnimateEnum.SimpleWipeDown;
		
		var HealthIcons = mbin.Children[1] as GcNGuiLayerData;
		HealthIcons.ElementData.Layout.PositionX	= 148;
		HealthIcons.ElementData.Layout.PositionY	= 119;
		HealthIcons.ElementData.Layout.Height		= 60;
		
		var ShieldIcon = mbin.Children[2] as GcNGuiLayerData;
		ShieldIcon.ElementData.IsHidden = true;
		
		var ShieldBar = mbin.Children[4] as GcNGuiLayerData;
		ShieldBar.ElementData.Layout.PositionX	= 77;
		ShieldBar.ElementData.Layout.PositionY	= 81;
		ShieldBar.ElementData.Layout.Width		= 355.2f;
		ShieldBar.ElementData.Layout.Height		= 30;
		
		var ShieldBarBackground = ShieldBar.Children[0] as GcNGuiGraphicData;
		ShieldBarBackground.ElementData.IsHidden = true;
		
		var ShieldBarAmount = ShieldBar.Children[2] as GcNGuiLayerData;
		ShieldBarAmount.ElementData.Layout.PositionX		= 0;
		ShieldBarAmount.ElementData.Layout.PositionY		= 0;
		ShieldBarAmount.ElementData.Layout.Align.Vertical	= TkNGuiAlignment.VerticalEnum.Top;
		ShieldBarAmount.ElementData.Layout.Align.Horizontal	= TkNGuiAlignment.HorizontalEnum.Left;
		ShieldBarAmount.ElementData.Layout.WidthPercentage	= true;
		ShieldBarAmount.ElementData.Layout.HeightPercentage	= true;
		ShieldBarAmount.ElementData.Layout.Width			= 60;
		ShieldBarAmount.ElementData.Layout.Height			= 100;
		ShieldBarAmount.Style.Default.Colour				= new(0, 0, 0, 0);
		
		var ShieldBarAmountTip = ShieldBarAmount.Children[0] as GcNGuiGraphicData;
		ShieldBarAmountTip.ElementData.IsHidden = true;
		
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
		
		GcNGuiGraphicData HealthBoarder = new() {
			ElementData = new GcNGuiElementData {
				ID			= "HEALTHBOARDER",
				IsHidden	= false,
				Layout = new GcNGuiLayoutData {
					Anchor			= true,
					PositionX		= 0,
					PositionY		= 0,
					Width			= 550,
					Height			= 566.0583f,
					ConstrainAspect = 1,
					Align = new TkNGuiAlignment {
						Vertical	= TkNGuiAlignment.VerticalEnum.Top,
						Horizontal	= TkNGuiAlignment.HorizontalEnum.Left
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
			Image = "TEXTURES/UI/HUD/HEALTHBOARDER.DDS"
		};
		
		mbin.Children.Insert(1, HealthBoarder);
	}
	
	protected void ShieldBar(GcNGuiLayerData mbin) {
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
			Colour					= DefaultDarkColourData,
			IconColour				= DefaultLightColourData,
			StrokeColour			= DefaultLightColourData,
			StrokeGradientColour	= DefaultLightColourData,
			GradientColour			= DefaultDarkColourData,
			SolidColour = true
		};
		
		GcNGuiGraphicData ShieldBarAmountGraphic = new() {
			ElementData = new GcNGuiElementData {
				ID			= "SHIELDGRAPHIC",
				IsHidden	= false,
				Layout = new GcNGuiLayoutData {
					Anchor			= true,
					PositionX		= 0,
					PositionY		= 0,
					Width			= 351.648f,
					Height			= 99,
					HeightPercentage = true,
					ConstrainAspect = 1,
					Align = new TkNGuiAlignment {
						Vertical	= TkNGuiAlignment.VerticalEnum.Top,
						Horizontal	= TkNGuiAlignment.HorizontalEnum.Left
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
			Image = "TEXTURES/UI/HUD/SHIELDBAR.DDS"
		};
		
		ShieldBarAmountGraphic.Style.Default.Colour.A = 0.1647f;
		ShieldBarAmountGraphic.Style.Highlight.Colour.A = 0.1647f;
		ShieldBarAmountGraphic.Style.Active.Colour.A = 0.1647f;
		
		var ShieldBar			= mbin.Children[4] as GcNGuiLayerData;
		var ShieldBarAmount		= ShieldBar.Children[2] as GcNGuiLayerData;
		
		ShieldBarAmount.Children.Add(ShieldBarAmountGraphic);
	}
	
	protected void HealthIcons() {
		var HuHealthIcon = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/COMPONENTS/HUHEALTHICON.MBIN"
		);
		
		HuHealthIcon.ElementData.Layout.ConstrainProportions	= false;
		HuHealthIcon.ElementData.Layout.Width					= 40;	//35
		HuHealthIcon.ElementData.Layout.Height					= 56;	//34
		
		var HealthIcon = HuHealthIcon.Children[1] as GcNGuiGraphicData;
		
		HealthIcon.ElementData.Layout.PositionX			= 20;	//20
		HealthIcon.ElementData.Layout.PositionY			= 27;	//20
		HealthIcon.ElementData.Layout.Width				= 61.1102f;	//64
		HealthIcon.ElementData.Layout.Height			= 61.1102f;	//64
		
		//The stock ship health is set up differently than player health, needs changing too.
		var ShipIcon = HuHealthIcon.Children[3] as GcNGuiGraphicData;
		
		ShipIcon.ElementData.Layout.PositionX			= 20;	//20
		ShipIcon.ElementData.Layout.PositionY			= 27;	//20
		
		ShipIcon.ElementData.Layout.WidthPercentage		= false;
		ShipIcon.ElementData.Layout.Width				= 61.1102f;	//157.99997
		
		ShipIcon.ElementData.Layout.HeightPercentage	= false;
		ShipIcon.ElementData.Layout.Height				= 61.1102f;	//157.99997
		
		var HudRowHealthIcons = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/COMPONENTS/HUDROWHEALTHICONS.MBIN"
		);
		
		HudRowHealthIcons.ElementData.Layout.Height		= 60;	//41
	}
	
	protected void UIGlobals() {
		var mbin = ExtractMbin<GcUIGlobals>(
			"GCUIGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.CompassHeight			= 30;	//60
		
		mbin.OSDRareItemRewardEffect.BoxSizeStart	= new(60f	, 70f);
		mbin.OSDRareItemRewardEffect.BoxSizeEnd		= new(500f	, 70f);
		mbin.OSDEpicItemRewardEffect.BoxSizeStart	= new(60f	, 75f);
		mbin.OSDEpicItemRewardEffect.BoxSizeEnd		= new(800f	, 85f);
		
		mbin.HUDDisplayTime			= float.MaxValue;
		mbin.ShieldSpringTime		= 0.2f;	//0.6
		mbin.ShieldHUDAlwaysOn		= true;
		
		mbin.AlwaysOnHazardMultiplierTox	= 0;
		mbin.AlwaysOnHazardMultiplierHeat	= 0;
		mbin.AlwaysOnHazardMultiplierRad	= 0;
		mbin.AlwaysOnHazardMultiplierCold	= 0;
		
		mbin.ShieldColour			= new(	0,	0,	0,	0);
		mbin.ShieldDamageColour		= new(	1,	0,	0,	0);
	}
	
	protected void PlayerGlobals() {
		var mbin = ExtractMbin<GcPlayerGlobals>(
			"GCPLAYERGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.MaxHealthPips = 7;
	}
	
	protected void GameplayGlobals() {
		var mbin = ExtractMbin<GcGameplayGlobals>(
			"GCGAMEPLAYGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.HUDStoreFlashTime = float.MaxValue;
	}
	
	protected void GraphicsGlobals() {
		var mbin = ExtractMbin<GcGraphicsGlobals>(
			"GCGRAPHICSGLOBALS.GLOBAL.MBIN"
		);
		
		mbin.ShieldDownScanlineTime = 1.5f;
	}
	
	protected void HudHazard() {
		var hud = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/HUD.MBIN"
		);
		
		var root = hud.Children[4] as GcNGuiLayerData;
		
		var health = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/HUDHEALTH.MBIN"
		);
		
		health.Children.Add(root);
		hud.Children.RemoveAt(4);
		
		var mbin = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/HUDHAZARD.MBIN"
		);
		
		var child1 = mbin.Children[1] as GcNGuiLayerData;
		var child2 = mbin.Children[2] as GcNGuiLayerData;
		var child3 = mbin.Children[3] as GcNGuiLayerData;
		var child4 = mbin.Children[4] as GcNGuiLayerData;
		var child5 = mbin.Children[5] as GcNGuiLayerData;
		
		child1.ElementData.IsHidden = true;
		child2.ElementData.IsHidden = true;
		child3.ElementData.IsHidden = true;
		child4.ElementData.IsHidden = true;
		child5.ElementData.IsHidden = true;
		
		var Root = mbin.Children[0] as GcNGuiLayerData;
		var FirstLayer = Root.Children[0] as GcNGuiLayerData;
		var SecondLayer = FirstLayer.Children[0] as GcNGuiLayerData;
		
		var EnvironInfo				= SecondLayer.Children[0] as GcNGuiLayerData;
		var PlanetName				= SecondLayer.Children[1] as GcNGuiLayerData;
		var		ThickSlash			= PlanetName.Children[0] as GcNGuiGraphicData;
		var		Planet				= PlanetName.Children[1] as GcNGuiTextData;
		
		
		EnvironInfo.ElementData.IsHidden = true;	//might change in the future
		ThickSlash.ElementData.IsHidden = true;
		
		//More of the fun stuff
		
		mbin.ElementData.Layout.PositionX = 0;
		mbin.ElementData.Layout.PositionY = 0;
		mbin.ElementData.Layout.Align.Vertical = TkNGuiAlignment.VerticalEnum.Top;
		mbin.ElementData.Layout.Align.Horizontal = TkNGuiAlignment.HorizontalEnum.Left;
		
		Root.ElementData.Layout.PositionX = 0;
		Root.ElementData.Layout.PositionY = 0;
		Root.ElementData.Layout.Align.Vertical = TkNGuiAlignment.VerticalEnum.Top;
		Root.ElementData.Layout.Align.Horizontal = TkNGuiAlignment.HorizontalEnum.Left;
			
		FirstLayer.ElementData.Layout.PositionX = 0;
		FirstLayer.ElementData.Layout.PositionY = 0;
		FirstLayer.ElementData.Layout.Align.Vertical = TkNGuiAlignment.VerticalEnum.Top;
		FirstLayer.ElementData.Layout.Align.Horizontal = TkNGuiAlignment.HorizontalEnum.Left;
			
		SecondLayer.ElementData.Layout.PositionX = 0;
		SecondLayer.ElementData.Layout.PositionY = 0;
		SecondLayer.ElementData.Layout.Align.Vertical = TkNGuiAlignment.VerticalEnum.Top;
		SecondLayer.ElementData.Layout.Align.Horizontal = TkNGuiAlignment.HorizontalEnum.Left;
		
		PlanetName.ElementData.Layout.PositionX			= 70;
		PlanetName.ElementData.Layout.PositionY			= 30;
		PlanetName.ElementData.Layout.Align.Vertical	= TkNGuiAlignment.VerticalEnum.Top;
		PlanetName.ElementData.Layout.Align.Horizontal	= TkNGuiAlignment.HorizontalEnum.Left;
		PlanetName.ElementData.Layout.WidthPercentage	= false;
		PlanetName.ElementData.Layout.HeightPercentage	= false;
		PlanetName.ElementData.Layout.Width				= 372;
		PlanetName.ElementData.Layout.Height			= 45;
		
		Planet.ElementData.Layout.PositionX = -0.75f;
		Planet.ElementData.Layout.PositionY = 4.5f;
		Planet.ElementData.Layout.Align.Vertical	= TkNGuiAlignment.VerticalEnum.Top;
		Planet.ElementData.Layout.Align.Horizontal	= TkNGuiAlignment.HorizontalEnum.Left;
		Planet.ElementData.Layout.WidthPercentage	= true;
		Planet.ElementData.Layout.HeightPercentage	= true;
		Planet.ElementData.Layout.Width				= 100;
		Planet.ElementData.Layout.Height			= 90;
		Planet.Style.Default.FontSpacing			= 7;
		Planet.Style.Default.Colour					= new libMBIN.NMS.Colour {
			R = 0,
			G = 0,
			B = 0,
			A = 1
		};
		Planet.Style.Default.FontIndex				= 1;
		Planet.Style.Default.HasDropShadow			= false;
		Planet.Style.Default.HasOutline				= false;
	}
	
	protected void Mission() {
		
		var mbin = ExtractMbin<GcNGuiLayerData> (
			"UI/HUD/HUD.MBIN"
		);
		
		var TopLeftGrp		= mbin.Children[0] as GcNGuiLayerData;
		var 	Root		= TopLeftGrp.Children[1] as GcNGuiLayerData;
		
		Root.ElementData.Layout.Width				= 766.15f;
		Root.ElementData.Layout.Height				= 120;
		Root.ElementData.Layout.AnchorPercent		= false;
		Root.ElementData.Layout.PositionX			= 50;
		Root.ElementData.Layout.PositionY			= 161;
		Root.ElementData.Layout.Align.Vertical		= TkNGuiAlignment.VerticalEnum.Top;
		Root.ElementData.Layout.Align.Horizontal	= TkNGuiAlignment.HorizontalEnum.Left;
		
		Root.Style.Animate							= TkNGuiGraphicStyle.AnimateEnum.CustomWipe;
		Root.Style.CustomMinStart					= new(0, 0);
		Root.Style.CustomMaxStart					= new(1, 0);
		
		//Icon is the default, HexBoarder/HexIcons are the ones that will change.
		var MissionSlash	= Root.Children[0] as GcNGuiGraphicData;
		var MissionBox		= Root.Children[1] as GcNGuiGraphicData;
		var Icon			= Root.Children[2] as GcNGuiGraphicData;
		var HexBoarder		= Root.Children[3] as GcNGuiGraphicData;
		var HexIcon			= Root.Children[4] as GcNGuiGraphicData;
		var Title			= Root.Children[5] as GcNGuiTextData;
		var Status			= Root.Children[6] as GcNGuiTextData;
		var OneLineStatus	= Root.Children[7] as GcNGuiTextData;
		var Objective		= Root.Children[8] as GcNGuiTextData;
		var ObjectiveBullet	= Root.Children[9] as GcNGuiGraphicData;
		
		MissionSlash.ElementData.IsHidden = true;
		
		MissionBox.ElementData.IsHidden				= false;
		MissionBox.Image							= "";
		MissionBox.ElementData.Layout.AnchorPercent	= true;
		MissionBox.ElementData.Layout.ConstrainProportions = false;
		MissionBox.ElementData.Layout.PositionX		= 4;
		MissionBox.ElementData.Layout.PositionY		= 60;
		MissionBox.ElementData.Layout.WidthPercentage = true;
		MissionBox.ElementData.Layout.Width			= 92;
		MissionBox.ElementData.Layout.HeightPercentage = true;
		MissionBox.ElementData.Layout.Height		= 50;
		MissionBox.ElementData.Layout.Align.Vertical = TkNGuiAlignment.VerticalEnum.Middle;
		MissionBox.ElementData.Layout.Align.Horizontal = TkNGuiAlignment.HorizontalEnum.Left;
		MissionBox.Style.Default.Colour				= new(0f, 0f, 0f, 0.3f);
		MissionBox.Style.Default.Gradient			= TkNGuiGraphicStyleData.GradientEnum.Horizontal;
		MissionBox.Style.Default.GradientColour		= new(0f, 0f, 0f, 0f);
		MissionBox.Style.Default.GradientOffsetPercent = true;
		MissionBox.Style.Default.GradientStartOffset = 30;
		
		Icon.ElementData.Layout.AnchorPercent		= true;
		Icon.ElementData.Layout.PositionX			= 10;
		Icon.ElementData.Layout.PositionY			= 60;
		Icon.ElementData.Layout.Align.Vertical		= TkNGuiAlignment.VerticalEnum.Middle;
		Icon.ElementData.Layout.Align.Horizontal	= TkNGuiAlignment.HorizontalEnum.Center;
		
		HexBoarder.ElementData.Layout.AnchorPercent		= true;
		HexBoarder.ElementData.Layout.PositionX			= 10;
		HexBoarder.ElementData.Layout.PositionY			= 60;
		HexBoarder.ElementData.Layout.Align.Vertical	= TkNGuiAlignment.VerticalEnum.Middle;
		HexBoarder.ElementData.Layout.Align.Horizontal	= TkNGuiAlignment.HorizontalEnum.Center;
		
		HexIcon.ElementData.Layout.AnchorPercent		= true;
		HexIcon.ElementData.Layout.PositionX			= 10;
		HexIcon.ElementData.Layout.PositionY			= 60;
		HexIcon.ElementData.Layout.Align.Vertical		= TkNGuiAlignment.VerticalEnum.Middle;
		HexIcon.ElementData.Layout.Align.Horizontal		= TkNGuiAlignment.HorizontalEnum.Center;
		
		Title.ElementData.Layout.AnchorPercent		= true;
		Title.ElementData.Layout.PositionX			= 50;
		Title.ElementData.Layout.PositionY			= 5;
		Title.ElementData.Layout.Align.Vertical		= TkNGuiAlignment.VerticalEnum.Top;
		Title.ElementData.Layout.Align.Horizontal	= TkNGuiAlignment.HorizontalEnum.Center;
		Title.Style.Default.Align.Vertical			= TkNGuiAlignment.VerticalEnum.Top;
		Title.Style.Default.Align.Horizontal		= TkNGuiAlignment.HorizontalEnum.Center;
		Title.Style.Default.Colour					= new(0f, 0f, 0f, 1f);
		Title.Style.Default.HasDropShadow			= true;	//Coloured by exe, must do this for visibility
		Title.Style.Default.FontSpacing				= 5;
		
		Status.ElementData.Layout.AnchorPercent		= true;
		Status.ElementData.Layout.PositionX			= 16;
		Status.ElementData.Layout.PositionY			= 40;
		Status.Style.Default.HasDropShadow			= false;
		Status.Style.Default.HasOutline				= false;
		Status.Style.Default.Colour					= new(1f, 1f, 1f, 1f);
		
		OneLineStatus.ElementData.Layout.AnchorPercent = true;
		OneLineStatus.ElementData.Layout.Align.Vertical = TkNGuiAlignment.VerticalEnum.Top;
		OneLineStatus.ElementData.Layout.Align.Horizontal = TkNGuiAlignment.HorizontalEnum.Left;
		OneLineStatus.ElementData.Layout.PositionX	= 17;
		OneLineStatus.ElementData.Layout.PositionY	= 50;
		OneLineStatus.Style.Default.HasDropShadow	= false;
		OneLineStatus.Style.Default.HasOutline		= false;
		OneLineStatus.Style.Default.Colour			= new(1f, 1f, 1f, 1f);
		
		Objective.ElementData.Layout.AnchorPercent	= true;
		Objective.ElementData.Layout.PositionX		= 20;
		Objective.ElementData.Layout.PositionY		= 60;
		Objective.ElementData.Layout.Width			= 65;
		Objective.ElementData.Layout.Height			= 50;
		Objective.ElementData.Layout.Align.Vertical	= TkNGuiAlignment.VerticalEnum.Top;
		Objective.ElementData.Layout.Align.Horizontal = TkNGuiAlignment.HorizontalEnum.Left;
		Objective.Style.Default.HasDropShadow		= false;
		Objective.Style.Default.HasOutline			= false;
		Objective.Style.Default.Colour				= new(1f, 1f, 1f, 1f);
		
		ObjectiveBullet.ElementData.Layout.AnchorPercent	= true;
		ObjectiveBullet.ElementData.Layout.PositionX		= 17;
		ObjectiveBullet.ElementData.Layout.PositionY		= 60;
		ObjectiveBullet.Style.Default.Colour				= new(1f, 1f, 1f, 1f);
	}
}

//=============================================================================
