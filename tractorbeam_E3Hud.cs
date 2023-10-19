//=============================================================================

public class tractorbeam_E3Hud : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		//Believe me, I've tried making my own mbins instead of editing the old ones. The game does NOT like them.

		UIGlobals();
		Compass();
		Hud();
	//	Hazard();
	//	Health();
	//	Weapon();
	}
	
	//...........................................................
	
	protected void UIGlobals() {
		var mbin = ExtractMbin<GcUIGlobals>(
			"GCUIGLOBALS.GLOBAL.MBIN"
		);
		
		//Fixes issue where the "Scanner damaged, repair required" line wouldn't play on a new save.
		var ScannerStartup = mbin.IntroTiming.HUDStartup.Find(INFO => INFO.RequiresTechBroken == "SCAN1");	
		ScannerStartup.RequiresTechBroken = "";
		
		//Turn off parallax, inventory UI update is next in line and this is just laying down the groundwork.
		mbin.NGuiMin2DParallax = new(0f, 0f);
		mbin.NGuiMax2DParallax = new(0f, 0f);
		mbin.NGuiModelParallax = new(0f, 0f);
		mbin.NGuiShipInteractParallax = new(0f, 0f);
		mbin.InteractionWorldParallax = new(0f, 0f);
		
	}
	
	protected void Compass() {
		var Root = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/HUDCOMPASSLINES.MBIN"
		);
		
		var Sphere = Root.Children[0] as GcNGuiGraphicData;
		
		Sphere.ElementData.Layout.Height	= 60;
		Sphere.Style.Default.StrokeColour	= new(0f, 0f, 0f, 0f);
		
		//Todo: Add the arrow.
	}
	
	protected void Hud() {
		var mbin = ExtractMbin<GcNGuiLayerData>(
			"UI/HUD/HUD.MBIN"
		);
		
		var TopLeftGrp = mbin.Children[0] as GcNGuiLayerData;
		
		var Mission = TopLeftGrp.Children[1] as GcNGuiLayerData;
		
		var MissionBoarder = GraphicTemplate(Mission.Children, 0, true);
		MissionBoarder.ElementData.ID = "MISSIONBOARDER";
		MissionBoarder.Style.Default.Colour = new(1f, 1f, 1f, 1f);
		MissionBoarder.Image = "TEXTURES/UI/HUD/MISSIONBOARDER.DDS";
		MissionBoarder.ElementData.Layout.Width = 100.5f;	//fixes weird rendering error.
		
		TopLeftGrp.Children.Insert(0, TopLeftGrp.Children[1]);
		TopLeftGrp.Children.RemoveAt(2);
	}
	
	//...........................................................
	
	protected void Hazard() {
		
	}
	
	//...........................................................
	
	protected void Health() {
		
	}
	
	//...........................................................
	
	protected void Weapon() {
		
	}
	
	//...........................................................
	
	libMBIN.NMS.Colour colourBlack = new() {
		R = 0,
		G = 0,
		B = 0,
		A = 1
	};
	libMBIN.NMS.Colour colourWhite = new() {
		R = 1,
		G = 1,
		B = 1,
		A = 1
	};
	
	protected GcNGuiLayerData LayerTemplate(List<libMBIN.NMSTemplate> rootChildren = null, bool fullscreen = false) {
		TkNGuiGraphicStyleData defaultStyleData = new() {
			Colour					= colourBlack,
			IconColour				= colourWhite,
			StrokeColour			= colourWhite,
			GradientColour			= colourBlack,
			StrokeGradientColour	= colourWhite,
			SolidColour = true
		};
		
		TkNGuiGraphicStyle defaultStyle = new() {
			Default			= defaultStyleData,
			Highlight 		= defaultStyleData,
			Active			= defaultStyleData,
			CustomMinStart	= new(1, 1),
			CustomMaxStart	= new(1, 1),
			HighlightTime	= 0.1f,
			HighlightScale	= 1,
			GlobalFade		= 1,
			AnimTime		= 0.5f,
			AnimSplit		= 0.4f
		};
		
		GcNGuiLayoutData defaultLayout = new() {};
		if (fullscreen) {
			defaultLayout = new() {
				WidthPercentage		= true,
				HeightPercentage	= true,
				Width				= 100,
				Height				= 100,
				ConstrainAspect		= 1,
				Anchor = true
			};
		} else {
			defaultLayout = new() {
				Width			= 50,
				Height			= 50,
				ConstrainAspect	= 1,
				Anchor = true
			};
		}
		
		GcNGuiElementData defaultElementData = new() {
			Layout = defaultLayout
		};
		
		GcNGuiLayerData Layout = new() {
			ElementData		= defaultElementData,
			Style			= defaultStyle,
			Children		= new List<libMBIN.NMSTemplate> {}
		};
		
		if (rootChildren != null) {
			rootChildren.AddUnique(Layout);
			
			return (GcNGuiLayerData)rootChildren[^1];
		}
		
		return Layout;
	}
	
	GcNGuiGraphicData GraphicTemplate(List<libMBIN.NMSTemplate> rootChildren = null, int indexNum = 0, bool fullscreen = false) {
		TkNGuiGraphicStyleData defaultStyleData = new() {
			Colour					= colourWhite,
			IconColour				= colourWhite,
			StrokeColour			= colourWhite,
			GradientColour			= colourBlack,
			StrokeGradientColour	= colourWhite,
			SolidColour = true
		};
		
		TkNGuiGraphicStyle defaultStyle = new() {
			Default			= defaultStyleData,
			Highlight 		= defaultStyleData,
			Active			= defaultStyleData,
			CustomMinStart	= new(1, 1),
			CustomMaxStart	= new(1, 1),
			HighlightTime	= 0.1f,
			HighlightScale	= 1,
			GlobalFade		= 1,
			AnimTime		= 0.5f,
			AnimSplit		= 0.4f
		};
		
		GcNGuiLayoutData defaultLayout = new() {};
		if (fullscreen) {
			defaultLayout = new() {
				WidthPercentage		= true,
				HeightPercentage	= true,
				Width				= 100,
				Height				= 100,
				ConstrainAspect		= 1,
				Anchor = true
			};
		} else {
			defaultLayout = new() {
				Width			= 50,
				Height			= 50,
				ConstrainAspect	= 1,
				Anchor = true
			};
		}
		
		GcNGuiElementData defaultElementData = new() {
			Layout = defaultLayout
		};
		
		GcNGuiGraphicData Layout = new() {
			ElementData		= defaultElementData,
			Style			= defaultStyle
		};
		
		if (rootChildren != null) {
			rootChildren.Insert(indexNum, Layout);
			
			return (GcNGuiGraphicData)rootChildren[indexNum];
		}
		
		return Layout;
	}
	
	GcNGuiTextData TextTemplate(List<libMBIN.NMSTemplate> rootChildren = null) {
		TkNGuiTextStyleData defaultTextStyleData = new() {
			Colour				= colourBlack,
			ShadowColour		= colourWhite,
			OutlineColour		= colourBlack,
			FontHeight			= 16,
			DropShadowOffset	= 1,
			OutlineSize			= 1,
			AutoAdjustFontHeight = true
		};
		
		TkNGuiTextStyle defaultTextStyle = new() {
			Default		= defaultTextStyleData,
			Highlight	= defaultTextStyleData,
			Active		= defaultTextStyleData
		};
		
		GcNGuiLayoutData defaultLayout = new() {
			Width			= 0,
			Height			= 0,
			ConstrainAspect	= 1,
			Anchor = true
		};
		
		GcNGuiElementData defaultElementData = new() {
			Layout = defaultLayout
		};
		
		GcNGuiTextData Layout = new() {
			ElementData		= defaultElementData,
			Style			= defaultTextStyle
		};
		
		if (rootChildren != null) {
			rootChildren.AddUnique(Layout);
			
			return (GcNGuiTextData)rootChildren[^1];
		}
		
		return Layout;
	}
}

//=============================================================================
