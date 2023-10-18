//=============================================================================

public class tractorbeam_SentinelTweaks : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		RobotGlobals();
		ExperienceSpawnTable();
		RemoveLootBonus();
	}
	
	//...........................................................
	
	protected void RobotGlobals() {
		var mbin = ExtractMbin<GcRobotGlobals>(
			"GCROBOTGLOBALS.MBIN"
		);
		
		foreach (GcSentinelResource resource in mbin.SentinelResources) {
			resource.BaseHealth *= 3;
			resource.RepairTime *= 3;
		}
		
		var limit = 10;
		mbin.SentinelSpawnLimits[0] *= limit;
		mbin.SentinelSpawnLimits[1] *= limit;
		mbin.SentinelSpawnLimits[2] *= limit;
		mbin.SentinelSpawnLimits[3] *= limit;
		mbin.SentinelSpawnLimits[4] *= limit;
		mbin.SentinelSpawnLimits[5] *= limit;
		mbin.SentinelSpawnLimits[6] *= limit;
		mbin.SentinelSpawnLimits[7] *= limit;
		mbin.SentinelSpawnLimits[8] *= limit;
		mbin.SentinelSpawnLimits[9] *= limit;
		mbin.SentinelSpawnLimits[10] *= limit;
		
		mbin.RobotHUDMarkerRange	= 30;		//80
		mbin.RobotHUDMarkerFalloff	= 25;		//50
		
		mbin.MaxNumPatrolDrones		= 2;		//1
		mbin.CombatWaveSpawnTime	= 0;		//10
		mbin.RobotSightAngle		= 45;		//80
		mbin.DroneSquadSpawnRadius	= 50;		//4
		
		mbin.DroneUpdateDistForMax	= 50;		//5
		mbin.DroneUpdateDistForMin	= 300;		//30
		mbin.DroneReAttackTime		= 0;		//10
		mbin.DroneSpawnTime			= 0.01f;	//2
		mbin.DroneSpawnFadeTime		= 0;		//0.8
		
		mbin.SummonerDroneCooldownOffset	= 0;	//10
		mbin.SummonerDroneBuildupTime		= 2.1f;	//4.2
		
		mbin.WalkerLaserOvershootStart		= 3;		//9
		mbin.WalkerLaserOvershootEnd		= -1;		//-7
		mbin.WalkerLaser.LaserTime			= 12;		//1.8
		mbin.WalkerLegShotDefendTime		= 5;		//15
		
		//Good riddance
		foreach (GcSentinelMechWeaponData weapon in mbin.SentinelMechWeaponData) {
			if (weapon.Id == "SENMECHCANON") {
				weapon.ProjectilesPerShot = 0;
			}
		}
		
		foreach (GcSentinelQuadWeaponData weapon in mbin.QuadWeapons) {
			if (weapon.Id == "QUADGRENADE")
				weapon.NumProjectiles = 0;
		}
		
		foreach (GcDroneWeaponData weapon in mbin.DroneWeapons) {
			if (weapon.Id == "ROBOTGRENADE" ||
			    weapon.Id == "CORRUPTGRENADE")
				weapon.NumProjectiles = 0;
		}
	}
	
	//...........................................................
	
	protected void ExperienceSpawnTable() {
		var spawntable = ExtractMbin<GcExperienceSpawnTable>(
			"METADATA/SIMULATION/SCENE/EXPERIENCESPAWNTABLE.MBIN"
		);
		
		var patroldrones = spawntable.SentinelSpawns.Find( item => item.Id == "BASIC_PATROL" );
		patroldrones.Spawns[0].MinAmount = 1;	//2
		patroldrones.Spawns[0].MaxAmount = 3;	//2
		
		var ReinforceOne = spawntable.SentinelSpawns.Find( item => item.Id == "REINFORCE_1" );
		
		ReinforceOne.Spawns.Clear();
		ReinforceOne.Spawns = new() {
			new GcSentinelSpawnData {
				Type = new GcSentinelTypes {
					SentinelType = GcSentinelTypes.SentinelTypeEnum.CombatDrone
				},
				MinAmount = 2,
				MaxAmount = 2
			}
		};
		
		var ReinforceTwo = spawntable.SentinelSpawns.Find( item => item.Id == "REINFORCE_2" );
		
		ReinforceTwo.Spawns.Clear();
		ReinforceTwo.Spawns = new() {
			new GcSentinelSpawnData {
				Type = new GcSentinelTypes {
					SentinelType = GcSentinelTypes.SentinelTypeEnum.CombatDrone
				},
				MinAmount = 3,
				MaxAmount = 3
			}
		};
		
		var ReinforceThr = spawntable.SentinelSpawns.Find( item => item.Id == "REINFORCE_3" );
		
		ReinforceThr.Spawns.Clear();
		ReinforceThr.Spawns = new() {
			new GcSentinelSpawnData {
				Type = new GcSentinelTypes {
					SentinelType = GcSentinelTypes.SentinelTypeEnum.SummonerDrone
				},
				MinAmount = 1,
				MaxAmount = 1
			},
			new GcSentinelSpawnData {
				Type = new GcSentinelTypes {
					SentinelType = GcSentinelTypes.SentinelTypeEnum.CombatDrone
				},
				MinAmount = 1,
				MaxAmount = 2
			}
		};
		
		//	The whole point of this is that it's supposed to *feel* like you have the entire planet's worth of sentinels
		//	on your ass. If you didn't, why would they be gone after?
		
		spawntable.SentinelSpawns.AddUnique( new() {
			Id = "WAVE1",
			Spawns = new List<GcSentinelSpawnData> {
				new GcSentinelSpawnData {
					Type = new GcSentinelTypes {
						SentinelType = GcSentinelTypes.SentinelTypeEnum.PatrolDrone
					},
					MinAmount = 1,
					MaxAmount = 3
				}
			},
			ReinforceAt = 2
		});
		
		spawntable.SentinelSpawns.AddUnique( new() {
			Id = "WAVE2_1",
			Spawns = new List<GcSentinelSpawnData> {
				new GcSentinelSpawnData {
					Type = new GcSentinelTypes {
						SentinelType = GcSentinelTypes.SentinelTypeEnum.PatrolDrone
					},
					MinAmount = 2,
					MaxAmount = 4
				},
				new GcSentinelSpawnData {
					Type = new GcSentinelTypes {
						SentinelType = GcSentinelTypes.SentinelTypeEnum.MedicDrone
					},
					MinAmount = 0,
					MaxAmount = 1
				}
			},
			ReinforceAt = 2
		});
		
		spawntable.SentinelSpawns.AddUnique( new() {
			Id = "WAVE2_2",
			Spawns = new List<GcSentinelSpawnData> {
				new GcSentinelSpawnData {
					Type = new GcSentinelTypes {
						SentinelType = GcSentinelTypes.SentinelTypeEnum.PatrolDrone
					},
					MinAmount = 0,
					MaxAmount = 4
				},
				new GcSentinelSpawnData {
					Type = new GcSentinelTypes {
						SentinelType = GcSentinelTypes.SentinelTypeEnum.SummonerDrone
					},
					MinAmount = 1,
					MaxAmount = 1
				},
			},
			ReinforceAt = 2
		});
				
		
		foreach (GcSentinelSpawnNamedSequence item in spawntable.SentinelSequences) {
			if (!item.Id.Value.Contains("WANTED")) continue;
			Log.AddInformation($"Found {item.Id.Value}");
			
			//The corrupted sentinels exist out of defiance and free will, they are fine with their numbers.
			if (item.Id.Value.Contains("CR")) continue;
			if (item.Id.Value.Contains("EX")) continue;
			
			if (item.Id.Value.Contains("0")) continue;
			
			item.Waves.Clear();
			Log.AddInformation("Cleared its contents");
			
			//Remember: last wave is the only wave you have to kill before going up a wanted lvl. Prev waves can still
			//			have live members and still bump you up a grade. Use this to your advantage.
			
			//	*	1-3 patrol
			//	*	1-3 patrol
			if (item.Id.Value.Contains("1")) {				
				Log.AddInformation("Adding Wanted_1 Set...");
				
			    item.Waves = new List<GcSentinelSpawnSequenceStep> {
					new GcSentinelSpawnSequenceStep {
						WavePool = new List<libMBIN.NMS.NMSString0x10> {
	               			"WAVE1"
			    		}
			    	},
					new GcSentinelSpawnSequenceStep {
						WavePool = new List<libMBIN.NMS.NMSString0x10> {
			    			"WAVE1"
			    		}
					}
				};
			}
			
			if (item.Id.Value.Contains("2")) {
				Log.AddInformation("Adding Wanted_2 Set...");
				
			    item.Waves = new List<GcSentinelSpawnSequenceStep> {
					new GcSentinelSpawnSequenceStep {
						WavePool = new List<libMBIN.NMS.NMSString0x10> {
					        "WAVE2_1"
				    	}
					},
					new GcSentinelSpawnSequenceStep {
						WavePool = new List<libMBIN.NMS.NMSString0x10> {
				        	"WAVE2_2"
				    	}
				    }
				};
			}
			

			if (item.Id.Value.Contains("3")) {
				Log.AddInformation("Adding Wanted_3 Set...");
				
				
			}
			
			
			if (item.Id.Value.Contains("4")) {
				Log.AddInformation("Adding Wanted_4 Set...");
				
			    
			}
			
			
			if (item.Id.Value.Contains("5")) {
				Log.AddInformation("Adding Wanted_5 Set...");
				
			    
			}
		}
	}
	
	//...........................................................
	
	protected void RemoveLootBonus() {
		var mbin = ExtractMbin<GcRewardTable>(
			"METADATA/REALITY/TABLES/REWARDTABLE.MBIN"
		);
		
		void findAndRemoveStats(List<GcRewardTableItem> list) {
			list.ForEach(ITEM => {
				if (ITEM.Reward.GetType() == typeof(GcRewardShield)) ITEM.PercentageChance = 0;
				if (ITEM.Reward.GetType() == typeof(GcRewardRefreshHazProt)) ITEM.PercentageChance = 0;
				if (ITEM.Reward.GetType() == typeof(GcRewardHealth)) ITEM.PercentageChance = 0;
			});
		}
		
		foreach (var entry in mbin.GenericTable) {
			if (!entry.Id.Value.EndsWith("_LOOT")) continue;
			switch(entry.Id.Value) {
				case var expression when (entry.Id.Value.StartsWith("DRONE")):
					findAndRemoveStats(entry.List.List);
					break;
				case var expression when (entry.Id.Value.StartsWith("QUAD")):
					findAndRemoveStats(entry.List.List);
					break;
				case var expression when (entry.Id.Value.StartsWith("MECH")):
					findAndRemoveStats(entry.List.List);
					break;
				case var expression when (entry.Id.Value.StartsWith("WALKER")):
					findAndRemoveStats(entry.List.List);
					break;
				case var expression when (entry.Id.Value.StartsWith("SPIDER")):
					findAndRemoveStats(entry.List.List);
					break;
				case var expression when (entry.Id.Value.StartsWith("CORRUPT")):
					findAndRemoveStats(entry.List.List);
					break;
			}
		}
	}
}

//=============================================================================
