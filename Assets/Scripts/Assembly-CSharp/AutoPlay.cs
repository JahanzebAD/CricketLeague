using UnityEngine;

public class AutoPlay : Singleton<AutoPlay>
{
	public bool BackKeyEnable;

	public bool isautoplayBatting;

	public bool isautoplayBowling;

	private int skipLength;

	private int skipCount;

	private int runstobeScored;

	private int currentRRScore;

	private int skippedOvers;

	private int skippedBalls;

	private int remainingOvers;

	private int completedOvers;

	private int ramainingballsinOver;

	private int currentSelectedOver;

	private int remainingballsinOver;

	private int extraTobegiven;

	private int bowlIndex;

	public int autoPlayCurrentBowlerInd;

	public int ballsPlayedfromLastskip;

	public void AutoplayCalculation(int batTeamRank, int bowlTeamRank)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		remainingOvers = CONTROLLER.totalOvers - CONTROLLER.ballsBowledPerDay / 6;
		skippedOvers = remainingOvers;
		remainingballsinOver = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 % 6;
		autoPlayCurrentBowlerInd = CONTROLLER.CurrentBowlerIndex;
		bowlIndex = CONTROLLER.BowlingTeam[CONTROLLER.currentInnings];
		runstobeScored = 0;
		CONTROLLER.isAutoPlayed = true;
		CONTROLLER.isAutoplay = true;
		if (remainingballsinOver != 0)
		{
			skippedBalls = 6 - remainingballsinOver + (skippedOvers - 1) * 6;
		}
		else
		{
			skippedBalls = skippedOvers * 6;
		}
		int num7;
		if (batTeamRank > bowlTeamRank)
		{
			num7 = batTeamRank - bowlTeamRank;
			if (num7 > 5 && num7 <= 10)
			{
				extraTobegiven = ((skippedOvers > 5) ? Random.Range(7, 11) : Random.Range(0, 4));
			}
			if (num7 > 10)
			{
				extraTobegiven = ((skippedOvers > 5) ? Random.Range(5, 8) : Random.Range(0, 3));
			}
		}
		else
		{
			num7 = bowlTeamRank - batTeamRank;
			if (num7 > 5 && num7 <= 10)
			{
				extraTobegiven = ((skippedOvers > 5) ? Random.Range(8, 15) : Random.Range(1, 5));
			}
			if (num7 > 10)
			{
				extraTobegiven = ((skippedOvers > 5) ? Random.Range(5, 20) : Random.Range(0, 3));
			}
		}
		if (CONTROLLER.lastWicketball == 0)
		{
			CONTROLLER.nextWicketball = 0;
			NextWicketCalculation(num7, batTeamRank, bowlTeamRank);
		}
		while (CONTROLLER.isAutoplay)
		{
			autoPlayCurrentBowlerInd = CONTROLLER.CurrentBowlerIndex;
			int num8 = Random.Range(0, 101);
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 % 6 == 0)
			{
				if (bowlTeamRank < batTeamRank)
				{
					if (num7 < 4)
					{
						flag = (((num8 > 25 && num8 < 45) || (num8 > 65 && num8 < 80)) ? true : false);
					}
					else if (num7 >= 4 && num7 < 8)
					{
						flag = (((num8 >= 25 && num8 < 50) || (num8 >= 65 && num8 < 83)) ? true : false);
					}
					else if (num7 >= 8)
					{
						flag = (((num8 >= 25 && num8 <= 45) || (num8 >= 65 && num8 <= 85)) ? true : false);
					}
				}
				else if (num7 < 4)
				{
					flag = (((num8 > 25 && num8 < 45) || (num8 > 65 && num8 < 80)) ? true : false);
				}
				else if (num7 >= 4 && num7 < 8)
				{
					flag = (((num8 > 25 && num8 <= 42) || (num8 >= 65 && num8 <= 78)) ? true : false);
				}
				else if (num7 >= 8)
				{
					flag = (((num8 > 25 && num8 < 40) || (num8 > 65 && num8 <= 73)) ? true : false);
				}
			}
			if (flag)
			{
				num5 = 0;
				num6 = 0;
			}
			else
			{
				if (flag2)
				{
					if (CONTROLLER.totalOvers <= 30)
					{
						num6 = ((CONTROLLER.totalOvers != 15) ? ((Random.Range(0, 3) == 1) ? 1 : 0) : ((Random.Range(0, 15) <= 5) ? 1 : 0));
					}
					else if (num7 < 4)
					{
						num6 = ((num8 == 3 || (num8 >= 61 && num8 <= 65) || (num8 >= 28 && num8 <= 35)) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "1")
					{
						num6 = ((num8 > 21 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "2")
					{
						num6 = ((num8 > 22 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "3")
					{
						num6 = ((num8 > 23 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "4")
					{
						num6 = ((num8 > 24 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "5")
					{
						num6 = ((num8 > 25 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "6")
					{
						num6 = ((num8 > 26 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "7")
					{
						num6 = ((num8 > 27 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "8")
					{
						num6 = ((num8 > 28 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "9")
					{
						num6 = ((num8 > 29 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "10")
					{
						num6 = ((num8 > 30 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "11")
					{
						num6 = ((num8 > 31 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "12")
					{
						num6 = ((num8 > 32 && num8 < 40) ? 1 : 0);
					}
				}
				else
				{
					num6 = 0;
				}
				if (num6 == 0)
				{
					if (CONTROLLER.difficultyMode == "hard")
					{
						if (batTeamRank < bowlTeamRank)
						{
							if (num7 <= 4)
							{
								num5 = ((CONTROLLER.totalOvers != 15 || batTeamRank >= 4) ? GetRuns(num8, 60, 70, 80, 85, 94, 100) : GetRuns(num8, 55, 65, 80, 85, 93, 100));
							}
							else if (num7 > 4 && num7 < 8)
							{
								num5 = GetRuns(num8, 60, 80, 85, 90, 95, 100);
							}
							else if (num7 >= 8)
							{
								num5 = GetRuns(num8, 55, 77, 85, 91, 95, 100);
							}
						}
						else if (num7 <= 4)
						{
							num5 = ((CONTROLLER.totalOvers != 15 || batTeamRank >= 4) ? GetRuns(num8, 60, 70, 80, 85, 94, 100) : GetRuns(num8, 55, 65, 80, 85, 93, 100));
						}
						else if (num7 > 4 && num7 < 8)
						{
							num5 = GetRuns(num8, 62, 84, 89, 92, 96, 100);
						}
						else if (num7 >= 8)
						{
							num5 = GetRuns(num8, 68, 84, 88, 93, 97, 100);
						}
					}
					else if (batTeamRank < bowlTeamRank)
					{
						if (num7 <= 4)
						{
							num5 = ((CONTROLLER.totalOvers != 15 || batTeamRank >= 4) ? GetRuns(num8, 48, 65, 80, 85, 94, 100) : GetRuns(num8, 45, 60, 73, 82, 93, 100));
						}
						else if (num7 > 4 && num7 < 8)
						{
							num5 = GetRuns(num8, 45, 73, 84, 90, 95, 100);
						}
						else if (num7 >= 8)
						{
							num5 = GetRuns(num8, 40, 68, 82, 90, 96, 100);
						}
					}
					else if (num7 <= 4)
					{
						num5 = ((CONTROLLER.totalOvers != 15 || batTeamRank >= 4) ? GetRuns(num8, 48, 65, 80, 85, 94, 100) : GetRuns(num8, 45, 60, 73, 83, 93, 100));
					}
					else if (num7 > 4 && num7 < 8)
					{
						num5 = GetRuns(num8, 55, 75, 88, 91, 95, 100);
					}
					else if (num7 >= 8)
					{
						num5 = GetRuns(num8, 60, 85, 90, 92, 96, 100);
					}
				}
				else
				{
					num5 = 0;
				}
			}
			if (num5 == 0 && num6 == 0 && num4 < extraTobegiven && !flag)
			{
				num3 = 1;
				num4++;
			}
			num2 += num5 + num3;
			num++;
			if (CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex && runstobeScored != 0)
			{
				if (num2 <= runstobeScored)
				{
					currentRRScore += num5;
				}
				else
				{
					num5 = 0;
				}
			}
			CONTROLLER.ballsBowledPerDay++;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1++;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeam[CONTROLLER.currentInnings]].PlayerList[autoPlayCurrentBowlerInd].BowlerList.TMBallsBowled1++;
			TestMatchBatsman.SetBallsPlayed(CONTROLLER.BattingTeam[CONTROLLER.currentInnings], CONTROLLER.StrikerIndex, 1);
			CONTROLLER.TeamList[CONTROLLER.BowlingTeam[CONTROLLER.currentInnings]].PlayerList[autoPlayCurrentBowlerInd].BowlerList.TMRunsGiven1 += num5 + num3;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores1 += num5 + num3;
			CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].currentMatchExtras += num3;
			TestMatchBatsman.SetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex, num5);
			Singleton<GameModel>.instance.runsScoredInOver += num5 + num3;
			if (num5 == 0)
			{
				CONTROLLER.DotInAOver++;
			}
			int num9 = 0;
			if (num6 != 0)
			{
				int catcherID = 0;
				num9 = Random.Range(1, 7);
				if (num9 == 3 || num9 == 5)
				{
					catcherID = Random.Range(0, 10);
				}
				TMWicketBall(CONTROLLER.StrikerIndex, num9, autoPlayCurrentBowlerInd, catcherID, CONTROLLER.StrikerIndex);
			}
			if (num5 == 4)
			{
				TestMatchBatsman.SetFours(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex, 1);
			}
			if (num5 == 6)
			{
				TestMatchBatsman.SetSixes(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex, 1);
			}
			if (CONTROLLER.currentInnings < 2)
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 1;
			}
			else
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 2;
			}
			Singleton<BattingScoreCard>.instance.UpdateScoreCard();
			if (CONTROLLER.BatTeamIndex == 0)
			{
				Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
			}
			else
			{
				Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
			}
			Singleton<Scoreboard>.instance.UpdateScoreCard();
			if (num5 == 1 || num5 == 3)
			{
				int strikerIndex = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = strikerIndex;
			}
			flag3 = Singleton<GameModel>.instance.CheckForAIDeclaration();
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 % 6 == 0)
			{
				if (Singleton<GameModel>.instance.runsScoredInOver <= 0)
				{
					CONTROLLER.TeamList[bowlIndex].PlayerList[autoPlayCurrentBowlerInd].BowlerList.Maiden++;
					Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
				}
				Singleton<GameModel>.instance.runsScoredInOver = 0;
				Singleton<Scoreboard>.instance.NewOver();
				Singleton<BowlingScoreCard>.instance.OverCompleted();
				if (num < skippedOvers * 6 - 5)
				{
					Singleton<GameModel>.instance.setSession();
				}
				int strikerIndex2 = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = strikerIndex2;
			}
			if (skippedOvers != 0)
			{
				if (num6 == 1)
				{
					CONTROLLER.lastWicketball = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1;
					flag2 = false;
					NextWicketCalculation(num7, batTeamRank, bowlTeamRank);
				}
				flag2 = ((CONTROLLER.nextWicketball <= CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 && !flag2) ? true : false);
			}
			if (skippedBalls <= num || flag3 || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= CONTROLLER.totalWickets || (CONTROLLER.currentInnings == 3 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores2 > CONTROLLER.TargetToChase))
			{
				CONTROLLER.isAutoplay = false;
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= CONTROLLER.totalWickets)
				{
					CONTROLLER.lastWicketball = 0;
					CONTROLLER.nextWicketball = 0;
				}
				AutoSave.currentBall = -1;
				CONTROLLER.isFromAutoPlay = true;
				CONTROLLER.TMisFromAutoPlay = true;
				if (flag3)
				{
					flag3 = false;
					CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared1 = true;
					Singleton<TMFollowOn>.instance.ShowAIDeclaration();
				}
				else
				{
					Singleton<GameModel>.instance.AutoplayCheckForOverComplete();
				}
				if (CONTROLLER.ballsBowledPerDay % 6 != 0)
				{
					CONTROLLER.ballsBowledPerDay += 6 - CONTROLLER.ballsBowledPerDay % 6;
				}
			}
			num3 = 0;
		}
	}

	public void AutoplayCalculationSecondInnings(int batTeamRank, int bowlTeamRank)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		remainingOvers = CONTROLLER.totalOvers - CONTROLLER.ballsBowledPerDay / 6;
		skippedOvers = remainingOvers;
		remainingballsinOver = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2 % 6;
		autoPlayCurrentBowlerInd = CONTROLLER.CurrentBowlerIndex;
		bowlIndex = CONTROLLER.BowlingTeam[CONTROLLER.currentInnings];
		runstobeScored = 0;
		CONTROLLER.isAutoPlayed = true;
		CONTROLLER.isAutoplay = true;
		if (remainingballsinOver != 0)
		{
			skippedBalls = 6 - remainingballsinOver + (skippedOvers - 1) * 6;
		}
		else
		{
			skippedBalls = skippedOvers * 6;
		}
		int num7;
		if (batTeamRank > bowlTeamRank)
		{
			num7 = batTeamRank - bowlTeamRank;
			if (num7 > 5 && num7 <= 10)
			{
				extraTobegiven = ((skippedOvers > 5) ? Random.Range(7, 11) : Random.Range(0, 4));
			}
			if (num7 > 10)
			{
				extraTobegiven = ((skippedOvers > 5) ? Random.Range(5, 8) : Random.Range(0, 3));
			}
		}
		else
		{
			num7 = bowlTeamRank - batTeamRank;
			if (num7 > 5 && num7 <= 10)
			{
				extraTobegiven = ((skippedOvers > 5) ? Random.Range(8, 15) : Random.Range(1, 5));
			}
			if (num7 > 10)
			{
				extraTobegiven = ((skippedOvers > 5) ? Random.Range(5, 20) : Random.Range(0, 3));
			}
		}
		if (CONTROLLER.lastWicketball == 0)
		{
			CONTROLLER.nextWicketball = 0;
			NextWicketCalculationSecondInnings(num7, batTeamRank, bowlTeamRank);
		}
		while (CONTROLLER.isAutoplay)
		{
			autoPlayCurrentBowlerInd = CONTROLLER.CurrentBowlerIndex;
			int num8 = Random.Range(0, 101);
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2 % 6 == 0)
			{
				if (bowlTeamRank < batTeamRank)
				{
					if (num7 < 4)
					{
						flag = (((num8 > 25 && num8 < 45) || (num8 > 65 && num8 < 80)) ? true : false);
					}
					else if (num7 >= 4 && num7 < 8)
					{
						flag = (((num8 >= 25 && num8 < 50) || (num8 >= 65 && num8 < 83)) ? true : false);
					}
					else if (num7 >= 8)
					{
						flag = (((num8 >= 25 && num8 <= 45) || (num8 >= 65 && num8 <= 85)) ? true : false);
					}
				}
				else if (num7 < 4)
				{
					flag = (((num8 > 25 && num8 < 45) || (num8 > 65 && num8 < 80)) ? true : false);
				}
				else if (num7 >= 4 && num7 < 8)
				{
					flag = (((num8 > 25 && num8 <= 42) || (num8 >= 65 && num8 <= 78)) ? true : false);
				}
				else if (num7 >= 8)
				{
					flag = (((num8 > 25 && num8 < 40) || (num8 > 65 && num8 <= 73)) ? true : false);
				}
			}
			if (flag)
			{
				num5 = 0;
				num6 = 0;
			}
			else
			{
				if (flag2)
				{
					if (CONTROLLER.totalOvers <= 30)
					{
						num6 = ((CONTROLLER.totalOvers != 15) ? ((Random.Range(0, 3) == 1) ? 1 : 0) : ((Random.Range(0, 15) <= 5) ? 1 : 0));
					}
					else if (num7 < 4)
					{
						num6 = ((num8 == 3 || (num8 >= 61 && num8 <= 65) || (num8 >= 28 && num8 <= 35)) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "1")
					{
						num6 = ((num8 > 21 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "2")
					{
						num6 = ((num8 > 22 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "3")
					{
						num6 = ((num8 > 23 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "4")
					{
						num6 = ((num8 > 24 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "5")
					{
						num6 = ((num8 > 25 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "6")
					{
						num6 = ((num8 > 26 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "7")
					{
						num6 = ((num8 > 27 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "8")
					{
						num6 = ((num8 > 28 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "9")
					{
						num6 = ((num8 > 29 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "10")
					{
						num6 = ((num8 > 30 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "11")
					{
						num6 = ((num8 > 31 && num8 < 40) ? 1 : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "12")
					{
						num6 = ((num8 > 32 && num8 < 40) ? 1 : 0);
					}
				}
				else
				{
					num6 = 0;
				}
				if (num6 == 0)
				{
					if (CONTROLLER.difficultyMode == "hard")
					{
						if (batTeamRank < bowlTeamRank)
						{
							if (num7 <= 4)
							{
								num5 = ((CONTROLLER.totalOvers != 15 || batTeamRank >= 4) ? GetRuns(num8, 60, 70, 80, 85, 94, 100) : GetRuns(num8, 55, 65, 80, 85, 93, 100));
							}
							else if (num7 > 4 && num7 < 8)
							{
								num5 = GetRuns(num8, 60, 80, 85, 90, 95, 100);
							}
							else if (num7 >= 8)
							{
								num5 = GetRuns(num8, 55, 77, 85, 91, 95, 100);
							}
						}
						else if (num7 <= 4)
						{
							num5 = ((CONTROLLER.totalOvers != 15 || batTeamRank >= 4) ? GetRuns(num8, 60, 70, 80, 85, 94, 100) : GetRuns(num8, 55, 65, 80, 85, 93, 100));
						}
						else if (num7 > 4 && num7 < 8)
						{
							num5 = GetRuns(num8, 62, 84, 89, 92, 96, 100);
						}
						else if (num7 >= 8)
						{
							num5 = GetRuns(num8, 68, 84, 88, 93, 97, 100);
						}
					}
					else if (batTeamRank < bowlTeamRank)
					{
						if (num7 <= 4)
						{
							num5 = ((CONTROLLER.totalOvers != 15 || batTeamRank >= 4) ? GetRuns(num8, 48, 65, 80, 85, 94, 100) : GetRuns(num8, 45, 60, 73, 82, 93, 100));
						}
						else if (num7 > 4 && num7 < 8)
						{
							num5 = GetRuns(num8, 45, 73, 84, 90, 95, 100);
						}
						else if (num7 >= 8)
						{
							num5 = GetRuns(num8, 40, 68, 82, 90, 96, 100);
						}
					}
					else if (num7 <= 4)
					{
						num5 = ((CONTROLLER.totalOvers != 15 || batTeamRank >= 4) ? GetRuns(num8, 48, 65, 80, 85, 94, 100) : GetRuns(num8, 45, 60, 73, 83, 93, 100));
					}
					else if (num7 > 4 && num7 < 8)
					{
						num5 = GetRuns(num8, 55, 75, 88, 91, 95, 100);
					}
					else if (num7 >= 8)
					{
						num5 = GetRuns(num8, 60, 85, 90, 92, 96, 100);
					}
				}
				else
				{
					num5 = 0;
				}
			}
			if (num5 == 0 && num6 == 0 && num4 < extraTobegiven && !flag)
			{
				num3 = 1;
				num4++;
			}
			num2 += num5 + num3;
			num++;
			if (CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex && runstobeScored != 0)
			{
				if (num2 <= runstobeScored)
				{
					currentRRScore += num5;
				}
				else
				{
					num5 = 0;
				}
			}
			CONTROLLER.ballsBowledPerDay++;
			CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2++;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeam[CONTROLLER.currentInnings]].PlayerList[autoPlayCurrentBowlerInd].BowlerList.TMBallsBowled2++;
			CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMBallsPlayed2++;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeam[CONTROLLER.currentInnings]].PlayerList[autoPlayCurrentBowlerInd].BowlerList.TMRunsGiven2 += num5 + num3;
			CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores2 += num5 + num3;
			CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].currentMatchExtras += num3;
			CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2 += num5;
			Singleton<GameModel>.instance.runsScoredInOver += num5 + num3;
			if (num5 == 0)
			{
				CONTROLLER.DotInAOver++;
			}
			int num9 = 0;
			if (num6 != 0)
			{
				int catcherID = 0;
				num9 = Random.Range(1, 7);
				if (num9 == 3 || num9 == 5)
				{
					catcherID = Random.Range(0, 10);
				}
				TMWicketBallSecondInnings(CONTROLLER.StrikerIndex, num9, autoPlayCurrentBowlerInd, catcherID, CONTROLLER.StrikerIndex);
			}
			if (num5 == 4)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMFours2++;
			}
			if (num5 == 6)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMSixes2++;
			}
			if (CONTROLLER.currentInnings < 2)
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 1;
			}
			else
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 2;
			}
			Singleton<BattingScoreCard>.instance.UpdateScoreCard();
			if (CONTROLLER.BatTeamIndex == 0)
			{
				Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
			}
			else
			{
				Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
			}
			Singleton<Scoreboard>.instance.UpdateScoreCard();
			if (num5 == 1 || num5 == 3)
			{
				int strikerIndex = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = strikerIndex;
			}
			flag3 = Singleton<GameModel>.instance.CheckForAIDeclaration();
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2 % 6 == 0)
			{
				if (Singleton<GameModel>.instance.runsScoredInOver <= 0)
				{
					CONTROLLER.TeamList[bowlIndex].PlayerList[autoPlayCurrentBowlerInd].BowlerList.Maiden++;
					Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
				}
				Singleton<GameModel>.instance.runsScoredInOver = 0;
				Singleton<Scoreboard>.instance.NewOver();
				Singleton<BowlingScoreCard>.instance.OverCompleted();
				if (num < skippedOvers * 6 - 5)
				{
					Singleton<GameModel>.instance.setSession();
				}
				int strikerIndex2 = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = strikerIndex2;
			}
			if (skippedOvers != 0)
			{
				if (num6 == 1)
				{
					CONTROLLER.lastWicketball = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2;
					flag2 = false;
					NextWicketCalculationSecondInnings(num7, batTeamRank, bowlTeamRank);
				}
				flag2 = ((CONTROLLER.nextWicketball <= CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2 && !flag2) ? true : false);
			}
			if (skippedBalls <= num || flag3 || CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= CONTROLLER.totalWickets || (CONTROLLER.currentInnings == 3 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores2 > CONTROLLER.TargetToChase))
			{
				CONTROLLER.isAutoplay = false;
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= CONTROLLER.totalWickets)
				{
					CONTROLLER.lastWicketball = 0;
					CONTROLLER.nextWicketball = 0;
				}
				AutoSave.currentBall = -1;
				CONTROLLER.isFromAutoPlay = true;
				CONTROLLER.TMisFromAutoPlay = true;
				if (flag3)
				{
					flag3 = false;
					CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared2 = true;
					Singleton<TMFollowOn>.instance.ShowAIDeclaration();
				}
				else
				{
					Singleton<GameModel>.instance.AutoplayCheckForOverComplete();
				}
				if (CONTROLLER.ballsBowledPerDay % 6 != 0)
				{
					CONTROLLER.ballsBowledPerDay += 6 - CONTROLLER.ballsBowledPerDay % 6;
				}
			}
			num3 = 0;
		}
	}

	private int GetRuns(int _randomRange, int _zeroRun, int _oneRun, int _twoRun, int _threeRun, int _fourRun, int _sixRun)
	{
		if (_randomRange > _zeroRun && _randomRange <= _oneRun)
		{
			return 1;
		}
		if (_randomRange > _oneRun && _randomRange <= _twoRun)
		{
			return 2;
		}
		if (_randomRange > _twoRun && _randomRange <= _threeRun)
		{
			return 3;
		}
		if (_randomRange > _threeRun && _randomRange <= _fourRun)
		{
			return 4;
		}
		if (_randomRange > _fourRun && _randomRange <= _sixRun)
		{
			return 6;
		}
		return 0;
	}

	private void NextWicketCalculation(int _diff, int _batRank, int _bowlRank)
	{
		int num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 - CONTROLLER.nextWicketball;
		if (_batRank > _bowlRank)
		{
			if (_diff < 4)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + ((_batRank < 4) ? Random.Range(Mathf.CeilToInt(CONTROLLER.totalOvers / 2), CONTROLLER.totalOvers - CONTROLLER.totalOvers / 3) : ((_batRank < 4 || _batRank >= 8) ? (CONTROLLER.totalOvers / 3 + Random.Range(1, 10)) : Random.Range(CONTROLLER.totalOvers / 3, CONTROLLER.totalOvers / 2)));
				if (_batRank < 4 && CONTROLLER.totalOvers == 15)
				{
					if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= 7)
					{
						CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(1, 10);
					}
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= 5)
				{
					CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + (CONTROLLER.totalOvers / 3 + Random.Range(1, 10));
				}
			}
			else if (_diff >= 4 && _diff < 8)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + ((_batRank <= 4 || _batRank >= 8) ? Random.Range(Mathf.CeilToInt(CONTROLLER.totalOvers / 4), CONTROLLER.totalOvers / 3) : Random.Range(CONTROLLER.totalOvers / 3, Mathf.CeilToInt(CONTROLLER.totalOvers / 2)));
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= 5)
				{
					CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(CONTROLLER.totalOvers / 4, CONTROLLER.totalOvers / 3);
				}
			}
			else
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + ((CONTROLLER.totalOvers <= 30) ? Random.Range(CONTROLLER.totalOvers / 3, Mathf.CeilToInt(CONTROLLER.totalOvers / 2)) : Random.Range(CONTROLLER.totalOvers / 5, CONTROLLER.totalOvers / 3));
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= 5)
				{
					CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(CONTROLLER.totalOvers / 4, CONTROLLER.totalOvers / 3);
				}
			}
		}
		else if (_diff < 4)
		{
			CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + ((_bowlRank < 4) ? Random.Range(CONTROLLER.totalOvers / 2, CONTROLLER.totalOvers - CONTROLLER.totalOvers / 3) : ((_bowlRank < 4 || _bowlRank >= 8) ? (CONTROLLER.totalOvers / 3 + Random.Range(1, 10)) : Random.Range(CONTROLLER.totalOvers / 3, CONTROLLER.totalOvers / 2)));
			if (_batRank < 4 && CONTROLLER.totalOvers == 15)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= 7)
				{
					CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(1, 10);
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= 5)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Mathf.CeilToInt((float)CONTROLLER.totalOvers / 1.5f) + Random.Range(5, 15);
			}
		}
		else if (_diff >= 4 && _diff < 8)
		{
			CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(CONTROLLER.totalOvers / 2, Mathf.CeilToInt(CONTROLLER.totalOvers / 2)) + ((_bowlRank < 5 || _bowlRank > 10) ? Random.Range(10, 15) : 0);
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= 5)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Mathf.CeilToInt((float)CONTROLLER.totalOvers / 2.5f) + Random.Range(5, 15);
			}
		}
		else
		{
			CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Mathf.CeilToInt(CONTROLLER.totalOvers / 2);
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= 5)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Mathf.CeilToInt((float)CONTROLLER.totalOvers / 2.5f);
			}
		}
		if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 > 7 && CONTROLLER.totalOvers != 15)
		{
			if (_diff <= 5)
			{
				CONTROLLER.nextWicketball = Random.Range(Mathf.CeilToInt((float)CONTROLLER.nextWicketball / 1.5f), CONTROLLER.nextWicketball);
			}
			else if (_diff > 5 && _diff <= 10)
			{
				CONTROLLER.nextWicketball = Random.Range(Mathf.CeilToInt((float)CONTROLLER.nextWicketball / 1.25f), CONTROLLER.nextWicketball);
			}
			else
			{
				CONTROLLER.nextWicketball = Random.Range(Mathf.CeilToInt((float)CONTROLLER.nextWicketball / 1.15f), CONTROLLER.nextWicketball);
			}
		}
		CONTROLLER.nextWicketball -= ((_diff < 5) ? (num / 2) : 0);
	}

	private void NextWicketCalculationSecondInnings(int _diff, int _batRank, int _bowlRank)
	{
		int num = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2 - CONTROLLER.nextWicketball;
		if (_batRank > _bowlRank)
		{
			if (_diff < 4)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + ((_batRank < 4) ? Random.Range(Mathf.CeilToInt(CONTROLLER.totalOvers / 2), CONTROLLER.totalOvers - CONTROLLER.totalOvers / 3) : ((_batRank < 4 || _batRank >= 8) ? (CONTROLLER.totalOvers / 3 + Random.Range(1, 10)) : Random.Range(CONTROLLER.totalOvers / 3, CONTROLLER.totalOvers / 2)));
				if (_batRank < 4 && CONTROLLER.totalOvers == 15)
				{
					if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= 7)
					{
						CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(1, 10);
					}
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= 5)
				{
					CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + (CONTROLLER.totalOvers / 3 + Random.Range(1, 10));
				}
			}
			else if (_diff >= 4 && _diff < 8)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + ((_batRank <= 4 || _batRank >= 8) ? Random.Range(Mathf.CeilToInt(CONTROLLER.totalOvers / 4), CONTROLLER.totalOvers / 3) : Random.Range(CONTROLLER.totalOvers / 3, Mathf.CeilToInt(CONTROLLER.totalOvers / 2)));
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= 5)
				{
					CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(CONTROLLER.totalOvers / 4, CONTROLLER.totalOvers / 3);
				}
			}
			else
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + ((CONTROLLER.totalOvers <= 30) ? Random.Range(CONTROLLER.totalOvers / 3, Mathf.CeilToInt(CONTROLLER.totalOvers / 2)) : Random.Range(CONTROLLER.totalOvers / 5, CONTROLLER.totalOvers / 3));
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= 5)
				{
					CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(CONTROLLER.totalOvers / 4, CONTROLLER.totalOvers / 3);
				}
			}
		}
		else if (_diff < 4)
		{
			CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + ((_bowlRank < 4) ? Random.Range(CONTROLLER.totalOvers / 2, CONTROLLER.totalOvers - CONTROLLER.totalOvers / 3) : ((_bowlRank < 4 || _bowlRank >= 8) ? (CONTROLLER.totalOvers / 3 + Random.Range(1, 10)) : Random.Range(CONTROLLER.totalOvers / 3, CONTROLLER.totalOvers / 2)));
			if (_batRank < 4 && CONTROLLER.totalOvers == 15)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= 7)
				{
					CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(1, 10);
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= 5)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Mathf.CeilToInt((float)CONTROLLER.totalOvers / 1.5f) + Random.Range(5, 15);
			}
		}
		else if (_diff >= 4 && _diff < 8)
		{
			CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Random.Range(CONTROLLER.totalOvers / 2, Mathf.CeilToInt(CONTROLLER.totalOvers / 2)) + ((_bowlRank < 5 || _bowlRank > 10) ? Random.Range(10, 15) : 0);
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= 5)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Mathf.CeilToInt((float)CONTROLLER.totalOvers / 2.5f) + Random.Range(5, 15);
			}
		}
		else
		{
			CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Mathf.CeilToInt(CONTROLLER.totalOvers / 2);
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= 5)
			{
				CONTROLLER.nextWicketball = CONTROLLER.lastWicketball + Mathf.CeilToInt((float)CONTROLLER.totalOvers / 2.5f);
			}
		}
		if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 > 7 && CONTROLLER.totalOvers != 15)
		{
			if (_diff <= 5)
			{
				CONTROLLER.nextWicketball = Random.Range(Mathf.CeilToInt((float)CONTROLLER.nextWicketball / 1.5f), CONTROLLER.nextWicketball);
			}
			else if (_diff > 5 && _diff <= 10)
			{
				CONTROLLER.nextWicketball = Random.Range(Mathf.CeilToInt((float)CONTROLLER.nextWicketball / 1.25f), CONTROLLER.nextWicketball);
			}
			else
			{
				CONTROLLER.nextWicketball = Random.Range(Mathf.CeilToInt((float)CONTROLLER.nextWicketball / 1.15f), CONTROLLER.nextWicketball);
			}
		}
		CONTROLLER.nextWicketball -= ((_diff < 5) ? (num / 2) : 0);
	}

	public void AutoplayOvers(int battingteamRank, int bowlingteamRank, int Overs)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		skippedOvers = Overs;
		currentSelectedOver = skippedOvers;
		runstobeScored = 0;
		CONTROLLER.isAutoPlayed = true;
		CONTROLLER.isAutoplay = true;
		bool flag = true;
		int num7 = 0;
		int num8 = 0;
		ramainingballsinOver = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 6;
		if (ramainingballsinOver != 0)
		{
			skippedBalls = 6 - ramainingballsinOver + (skippedOvers - 1) * 6;
		}
		else
		{
			skippedBalls = skippedOvers * 6;
		}
		int num9 = ((battingteamRank <= bowlingteamRank) ? (bowlingteamRank - battingteamRank) : (battingteamRank - bowlingteamRank));
		CheckforRunrate();
		while (CONTROLLER.isAutoplay)
		{
			int num10 = Random.Range(0, 91);
			if (flag)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "1")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 21 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "2")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 22 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "3")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 23 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "4")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 24 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "5")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 25 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "6")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 25 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "7")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 25 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "8")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 26 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "9")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 27 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "10")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 27 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "11")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 28 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "12")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 29 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "13")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 30 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "14")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 30 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "15")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 30 && num10 < 35) ? 1 : 0));
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].rank == "16")
				{
					num8 = ((num9 < 4) ? ((num10 == 3 || (num10 >= 61 && num10 <= 65) || num10 == 28 || num10 == 57 || num10 == 98 || num10 == 19) ? 1 : 0) : ((num10 > 30 && num10 < 35) ? 1 : 0));
				}
			}
			else
			{
				num8 = 0;
			}
			if (num8 == 0)
			{
				if (CONTROLLER.difficultyMode == "easy")
				{
					if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "1")
					{
						num7 = ((num10 > 13) ? ((num10 > 13 && num10 <= 39) ? 1 : ((num10 > 39 && num10 <= 60) ? 2 : ((num10 > 60 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "2")
					{
						num7 = ((num10 > 14) ? ((num10 > 14 && num10 <= 40) ? 1 : ((num10 > 40 && num10 <= 60) ? 2 : ((num10 > 60 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "3")
					{
						num7 = ((num10 > 14) ? ((num10 > 14 && num10 <= 41) ? 1 : ((num10 > 41 && num10 <= 60) ? 2 : ((num10 > 60 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "4")
					{
						num7 = ((num10 > 15) ? ((num10 > 15 && num10 <= 42) ? 1 : ((num10 > 42 && num10 <= 61) ? 2 : ((num10 > 61 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "5")
					{
						num7 = ((num10 > 15) ? ((num10 > 15 && num10 <= 43) ? 1 : ((num10 > 43 && num10 <= 61) ? 2 : ((num10 > 61 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "6")
					{
						num7 = ((num10 > 16) ? ((num10 > 16 && num10 <= 44) ? 1 : ((num10 > 44 && num10 <= 61) ? 2 : ((num10 > 61 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "7")
					{
						num7 = ((num10 > 16) ? ((num10 > 16 && num10 <= 45) ? 1 : ((num10 > 45 && num10 <= 62) ? 2 : ((num10 > 62 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "8")
					{
						num7 = ((num10 > 17) ? ((num10 > 17 && num10 <= 46) ? 1 : ((num10 > 46 && num10 <= 62) ? 2 : ((num10 > 62 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "9")
					{
						num7 = ((num10 > 17) ? ((num10 > 17 && num10 <= 47) ? 1 : ((num10 > 47 && num10 <= 62) ? 2 : ((num10 > 62 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "10")
					{
						num7 = ((num10 > 18) ? ((num10 > 18 && num10 <= 48) ? 1 : ((num10 > 48 && num10 <= 63) ? 2 : ((num10 > 63 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "11")
					{
						num7 = ((num10 > 18) ? ((num10 > 18 && num10 <= 49) ? 1 : ((num10 > 49 && num10 <= 63) ? 2 : ((num10 > 63 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "12")
					{
						num7 = ((num10 > 19) ? ((num10 > 19 && num10 <= 50) ? 1 : ((num10 > 50 && num10 <= 63) ? 2 : ((num10 > 63 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "13")
					{
						num7 = ((num10 > 20) ? ((num10 > 20 && num10 <= 51) ? 1 : ((num10 > 51 && num10 <= 64) ? 2 : ((num10 > 64 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "14")
					{
						num7 = ((num10 > 21) ? ((num10 > 21 && num10 <= 52) ? 1 : ((num10 > 52 && num10 <= 64) ? 2 : ((num10 > 64 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "15")
					{
						num7 = ((num10 > 22) ? ((num10 > 22 && num10 <= 53) ? 1 : ((num10 > 53 && num10 <= 65) ? 2 : ((num10 > 65 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "16")
					{
						num7 = ((num10 > 23) ? ((num10 > 23 && num10 <= 53) ? 1 : ((num10 > 53 && num10 <= 66) ? 2 : ((num10 > 66 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
					}
				}
				else if (CONTROLLER.difficultyMode == "medium")
				{
					if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "1")
					{
						num7 = ((num10 > 13) ? ((num10 > 13 && num10 <= 39) ? 1 : ((num10 > 39 && num10 <= 60) ? 2 : ((num10 > 60 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "2")
					{
						num7 = ((num10 > 14) ? ((num10 > 14 && num10 <= 40) ? 1 : ((num10 > 40 && num10 <= 60) ? 2 : ((num10 > 60 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "3")
					{
						num7 = ((num10 > 14) ? ((num10 > 14 && num10 <= 41) ? 1 : ((num10 > 41 && num10 <= 60) ? 2 : ((num10 > 60 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "4")
					{
						num7 = ((num10 > 15) ? ((num10 > 15 && num10 <= 42) ? 1 : ((num10 > 42 && num10 <= 61) ? 2 : ((num10 > 61 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "5")
					{
						num7 = ((num10 > 15) ? ((num10 > 15 && num10 <= 43) ? 1 : ((num10 > 43 && num10 <= 61) ? 2 : ((num10 > 61 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "6")
					{
						num7 = ((num10 > 16) ? ((num10 > 16 && num10 <= 44) ? 1 : ((num10 > 44 && num10 <= 61) ? 2 : ((num10 > 61 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "7")
					{
						num7 = ((num10 > 16) ? ((num10 > 16 && num10 <= 45) ? 1 : ((num10 > 45 && num10 <= 62) ? 2 : ((num10 > 62 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "8")
					{
						num7 = ((num10 > 17) ? ((num10 > 17 && num10 <= 46) ? 1 : ((num10 > 46 && num10 <= 62) ? 2 : ((num10 > 62 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "9")
					{
						num7 = ((num10 > 17) ? ((num10 > 17 && num10 <= 47) ? 1 : ((num10 > 47 && num10 <= 62) ? 2 : ((num10 > 62 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "10")
					{
						num7 = ((num10 > 18) ? ((num10 > 18 && num10 <= 48) ? 1 : ((num10 > 48 && num10 <= 63) ? 2 : ((num10 > 63 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "11")
					{
						num7 = ((num10 > 18) ? ((num10 > 18 && num10 <= 49) ? 1 : ((num10 > 49 && num10 <= 63) ? 2 : ((num10 > 63 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "12")
					{
						num7 = ((num10 > 19) ? ((num10 > 19 && num10 <= 50) ? 1 : ((num10 > 50 && num10 <= 63) ? 2 : ((num10 > 63 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "13")
					{
						num7 = ((num10 > 20) ? ((num10 > 20 && num10 <= 51) ? 1 : ((num10 > 51 && num10 <= 64) ? 2 : ((num10 > 64 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "14")
					{
						num7 = ((num10 > 21) ? ((num10 > 21 && num10 <= 52) ? 1 : ((num10 > 52 && num10 <= 64) ? 2 : ((num10 > 64 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "15")
					{
						num7 = ((num10 > 22) ? ((num10 > 22 && num10 <= 53) ? 1 : ((num10 > 53 && num10 <= 65) ? 2 : ((num10 > 65 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "16")
					{
						num7 = ((num10 > 23) ? ((num10 > 23 && num10 <= 53) ? 1 : ((num10 > 53 && num10 <= 66) ? 2 : ((num10 > 66 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
					}
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "1")
				{
					num7 = ((num10 > 13) ? ((num10 > 13 && num10 <= 39) ? 1 : ((num10 > 39 && num10 <= 60) ? 2 : ((num10 > 60 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "2")
				{
					num7 = ((num10 > 14) ? ((num10 > 14 && num10 <= 40) ? 1 : ((num10 > 40 && num10 <= 60) ? 2 : ((num10 > 60 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "3")
				{
					num7 = ((num10 > 14) ? ((num10 > 14 && num10 <= 41) ? 1 : ((num10 > 41 && num10 <= 60) ? 2 : ((num10 > 60 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "4")
				{
					num7 = ((num10 > 15) ? ((num10 > 15 && num10 <= 42) ? 1 : ((num10 > 42 && num10 <= 61) ? 2 : ((num10 > 61 && num10 <= 69) ? 3 : ((num10 > 69 && num10 <= 78) ? 4 : ((num10 > 78 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "5")
				{
					num7 = ((num10 > 15) ? ((num10 > 15 && num10 <= 43) ? 1 : ((num10 > 43 && num10 <= 61) ? 2 : ((num10 > 61 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "6")
				{
					num7 = ((num10 > 16) ? ((num10 > 16 && num10 <= 44) ? 1 : ((num10 > 44 && num10 <= 61) ? 2 : ((num10 > 61 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "7")
				{
					num7 = ((num10 > 16) ? ((num10 > 16 && num10 <= 45) ? 1 : ((num10 > 45 && num10 <= 62) ? 2 : ((num10 > 62 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "8")
				{
					num7 = ((num10 > 17) ? ((num10 > 17 && num10 <= 46) ? 1 : ((num10 > 46 && num10 <= 62) ? 2 : ((num10 > 62 && num10 <= 70) ? 3 : ((num10 > 70 && num10 <= 79) ? 4 : ((num10 > 79 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "9")
				{
					num7 = ((num10 > 17) ? ((num10 > 17 && num10 <= 47) ? 1 : ((num10 > 47 && num10 <= 62) ? 2 : ((num10 > 62 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "10")
				{
					num7 = ((num10 > 18) ? ((num10 > 18 && num10 <= 48) ? 1 : ((num10 > 48 && num10 <= 63) ? 2 : ((num10 > 63 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "11")
				{
					num7 = ((num10 > 18) ? ((num10 > 18 && num10 <= 49) ? 1 : ((num10 > 49 && num10 <= 63) ? 2 : ((num10 > 63 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "12")
				{
					num7 = ((num10 > 19) ? ((num10 > 19 && num10 <= 50) ? 1 : ((num10 > 50 && num10 <= 63) ? 2 : ((num10 > 63 && num10 <= 71) ? 3 : ((num10 > 71 && num10 <= 80) ? 4 : ((num10 > 80 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "13")
				{
					num7 = ((num10 > 20) ? ((num10 > 20 && num10 <= 51) ? 1 : ((num10 > 51 && num10 <= 64) ? 2 : ((num10 > 64 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "14")
				{
					num7 = ((num10 > 21) ? ((num10 > 21 && num10 <= 52) ? 1 : ((num10 > 52 && num10 <= 64) ? 2 : ((num10 > 64 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "15")
				{
					num7 = ((num10 > 22) ? ((num10 > 22 && num10 <= 53) ? 1 : ((num10 > 53 && num10 <= 65) ? 2 : ((num10 > 65 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
				}
				else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "16")
				{
					num7 = ((num10 > 23) ? ((num10 > 23 && num10 <= 53) ? 1 : ((num10 > 53 && num10 <= 66) ? 2 : ((num10 > 66 && num10 <= 72) ? 3 : ((num10 > 72 && num10 <= 81) ? 4 : ((num10 > 81 && num10 <= 90) ? 6 : 0))))) : 0);
				}
			}
			else
			{
				num7 = 0;
			}
			if (num7 == 0 && num8 == 0 && num6 < 2)
			{
				num4 = 1;
				num6++;
			}
			num2 += num7 + num4;
			num++;
			if (CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex && runstobeScored != 0)
			{
				if (num2 <= runstobeScored)
				{
					currentRRScore += num7;
				}
				else
				{
					num7 = 0;
				}
			}
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls++;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.BallsBowled++;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.BallsPlayed++;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.RunsGiven += num7 + num4;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores += num7 + num4;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras += num4;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored += num7;
			if (CONTROLLER.PlayModeSelected == 7)
			{
				if (CONTROLLER.currentInnings < 2)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1++;
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.TMBallsBowled1++;
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMBallsPlayed1++;
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.TMRunsGiven1 += num7 + num4;
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores1 += num7 + num4;
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored1 += num7;
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2++;
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.TMBallsBowled2++;
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMBallsPlayed2++;
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.TMRunsGiven2 += num7 + num4;
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores2 += num7 + num4;
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2 += num7;
				}
			}
			Singleton<GameModel>.instance.runsScoredInOver += num7 + num4;
			if (num7 == 0)
			{
				CONTROLLER.DotInAOver++;
			}
			int num11 = 0;
			if (num8 == 0)
			{
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.WicketsInBallCount = 0;
			}
			else
			{
				int catcherID = 0;
				num11 = Random.Range(1, 7);
				if (num11 == 3 || num11 == 5)
				{
					catcherID = Random.Range(0, 10);
				}
				WicketBall(CONTROLLER.StrikerIndex, num11, CONTROLLER.CurrentBowlerIndex, catcherID, CONTROLLER.StrikerIndex);
			}
			if (num7 == 4)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Fours++;
				if (CONTROLLER.PlayModeSelected == 7)
				{
					TestMatchBatsman.SetFours(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex, 1);
				}
			}
			if (num7 == 6)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Sixes++;
				if (CONTROLLER.PlayModeSelected == 7)
				{
					TestMatchBatsman.SetSixes(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex, 1);
				}
			}
			Singleton<GameModel>.instance.ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
			Singleton<GameModel>.instance.ExtraStr = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras;
			Singleton<GameModel>.instance.OversStr = Singleton<GameModel>.instance.GetOverStr() + "(" + CONTROLLER.totalOvers + ")";
			if (CONTROLLER.currentInnings < 2)
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 1;
			}
			else
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 2;
			}
			Singleton<BattingScoreCard>.instance.UpdateScoreCard();
			Singleton<Scoreboard>.instance.UpdateScoreCard();
			if (CONTROLLER.PlayModeSelected != 7)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 6 == 0)
				{
					Singleton<Scoreboard>.instance.NewOver();
					Singleton<BowlingScoreCard>.instance.OverCompleted();
				}
			}
			else if (CONTROLLER.currentInnings < 2)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 % 6 == 0)
				{
					Singleton<Scoreboard>.instance.NewOver();
					Singleton<BowlingScoreCard>.instance.OverCompleted();
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 % 6 == 0)
			{
				Singleton<Scoreboard>.instance.NewOver();
				Singleton<BowlingScoreCard>.instance.OverCompleted();
			}
			if (num7 == 1 || num7 == 3)
			{
				int strikerIndex = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = strikerIndex;
			}
			if (currentSelectedOver != 0)
			{
				if (num8 == 1)
				{
					flag = false;
					if (battingteamRank > bowlingteamRank)
					{
						currentSelectedOver = num + ((num9 < 4) ? (CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] / 2) : ((num9 >= 4 || num9 >= 9) ? (CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] / 3) : ((int)((float)CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] / 2.5f))));
					}
					else
					{
						currentSelectedOver = num + CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] / 2;
					}
				}
				if (currentSelectedOver <= num && !flag)
				{
					flag = true;
				}
			}
			if (skippedBalls <= num || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets >= 10 || (CONTROLLER.currentInnings == 1 && CONTROLLER.TargetToChase <= CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores))
			{
				CONTROLLER.isAutoplay = false;
				Singleton<PauseGameScreen>.instance.midPageGO.SetActive(value: true);
				Singleton<PauseGameScreen>.instance.BG.SetActive(value: true);
				AutoSave.currentBall = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 6;
				CONTROLLER.isFromAutoPlay = true;
				Singleton<GameModel>.instance.AutoplayCheckForOverComplete();
			}
			num4 = 0;
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 30 == 0)
			{
				num6 = 0;
			}
		}
		if (CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex)
		{
			isautoplayBatting = true;
		}
		else
		{
			isautoplayBowling = true;
		}
	}

	private void CheckforRunrate()
	{
		if (CONTROLLER.myTeamIndex != CONTROLLER.BattingTeamIndex)
		{
			return;
		}
		if (ballsPlayedfromLastskip != 0)
		{
			ballsPlayedfromLastskip = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls - ballsPlayedfromLastskip;
			if (CONTROLLER.PlayModeSelected == 7)
			{
				if (CONTROLLER.currentInnings < 2)
				{
					ballsPlayedfromLastskip = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 - ballsPlayedfromLastskip;
				}
				else
				{
					ballsPlayedfromLastskip = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 - ballsPlayedfromLastskip;
				}
			}
		}
		else
		{
			ballsPlayedfromLastskip = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls;
		}
		if ((float)ballsPlayedfromLastskip >= (float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6) * 0.4f)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "1" || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "2")
			{
				runstobeScored = Mathf.RoundToInt(CONTROLLER.RunRate) + 6;
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "3" || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "4")
			{
				runstobeScored = Mathf.RoundToInt(CONTROLLER.RunRate) + 5;
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "5" || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "6")
			{
				runstobeScored = Mathf.RoundToInt(CONTROLLER.RunRate) + 4;
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "7" || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "8" || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "9" || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "10")
			{
				runstobeScored = Mathf.RoundToInt(CONTROLLER.RunRate) + 3;
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "11" || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "12" || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "13" || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].rank == "14")
			{
				runstobeScored = Mathf.RoundToInt(CONTROLLER.RunRate) + 2;
			}
			runstobeScored *= skippedOvers;
		}
	}

	private void WicketBall(int batsmanID, int wicketType, int bowlerID, int catcherID, int batsmanOut)
	{
		CONTROLLER.wicketType = wicketType;
		if (Singleton<GameModel>.instance.NewBatsmanIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length - 1)
		{
			Singleton<GameModel>.instance.NewBatsmanIndex++;
		}
		if (Singleton<GameModel>.instance.NewBatsmanIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[Singleton<GameModel>.instance.NewBatsmanIndex].BatsmanList.Status = "not out";
			if (CONTROLLER.StrikerIndex == batsmanOut)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipBalls = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipBalls = 0;
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipBalls = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipBalls = 0;
			}
		}
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets++;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.FOW = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
		if (wicketType == 4)
		{
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount = 0;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "run out";
		}
		else
		{
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.Wicket++;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount++;
			switch (wicketType)
			{
			case 1:
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				break;
			case 2:
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "lbw " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				break;
			case 3:
				catcherID = CONTROLLER.FieldersArray[catcherID];
				if (bowlerID == catcherID)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "ct off b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "ct off b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				}
				break;
			case 5:
				catcherID = CONTROLLER.FieldersArray[catcherID];
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "ct behind b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				break;
			case 6:
				if (CONTROLLER.BowlerType == 1)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "st, b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "lbw " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				}
				break;
			}
		}
		if (CONTROLLER.currentInnings < 2)
		{
			Singleton<BattingScoreCard>.instance.SelectedInnings = 1;
		}
		else
		{
			Singleton<BattingScoreCard>.instance.SelectedInnings = 2;
		}
		Singleton<BattingScoreCard>.instance.UpdateWicket(batsmanOut);
		if (wicketType == 4)
		{
			if (catcherID == 0 && batsmanOut == CONTROLLER.StrikerIndex)
			{
				CONTROLLER.StrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
			else if (catcherID == 1 && batsmanOut == CONTROLLER.StrikerIndex)
			{
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
			else if (catcherID == 0 && batsmanOut == CONTROLLER.NonStrikerIndex)
			{
				CONTROLLER.NonStrikerIndex = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
			else if (catcherID == 1 && batsmanOut == CONTROLLER.NonStrikerIndex)
			{
				CONTROLLER.NonStrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
		}
		else if (batsmanOut == CONTROLLER.StrikerIndex)
		{
			CONTROLLER.StrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
		}
		else if (batsmanOut == CONTROLLER.NonStrikerIndex)
		{
			CONTROLLER.NonStrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
		}
	}

	private void TMWicketBall(int batsmanID, int wicketType, int bowlerID, int catcherID, int batsmanOut)
	{
		CONTROLLER.wicketType = wicketType;
		if (Singleton<GameModel>.instance.NewBatsmanIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length - 1)
		{
			Singleton<GameModel>.instance.NewBatsmanIndex++;
		}
		if (Singleton<GameModel>.instance.NewBatsmanIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length)
		{
			TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, Singleton<GameModel>.instance.NewBatsmanIndex, "not out");
			if (CONTROLLER.StrikerIndex == batsmanOut)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipBalls = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipBalls = 0;
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipBalls = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipBalls = 0;
			}
		}
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1++;
		TestMatchBatsman.SetFOW(CONTROLLER.BattingTeamIndex, batsmanOut, CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores1);
		if (wicketType == 4)
		{
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount = 0;
			TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "run out");
		}
		else
		{
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMWicket1++;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount++;
			switch (wicketType)
			{
			case 1:
				TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID));
				break;
			case 2:
				TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "lbw " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID));
				break;
			case 3:
				catcherID = CONTROLLER.FieldersArray[catcherID];
				if (bowlerID == catcherID)
				{
					TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "ct & b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID));
				}
				else
				{
					TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "ct off b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID));
				}
				break;
			case 5:
				catcherID = CONTROLLER.FieldersArray[catcherID];
				TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "ct behind b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID));
				break;
			case 6:
				if (CONTROLLER.BowlerType == 1)
				{
					TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "st, b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID));
				}
				else
				{
					TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "lbw " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID));
				}
				break;
			}
		}
		if (CONTROLLER.currentInnings < 2)
		{
			Singleton<BattingScoreCard>.instance.SelectedInnings = 1;
		}
		else
		{
			Singleton<BattingScoreCard>.instance.SelectedInnings = 2;
		}
		Singleton<BattingScoreCard>.instance.UpdateWicket(batsmanOut);
		Singleton<BattingScoreCard>.instance.UpdateScoreCard();
		if (wicketType == 4)
		{
			if (catcherID == 0 && batsmanOut == CONTROLLER.StrikerIndex)
			{
				CONTROLLER.StrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
			else if (catcherID == 1 && batsmanOut == CONTROLLER.StrikerIndex)
			{
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
			else if (catcherID == 0 && batsmanOut == CONTROLLER.NonStrikerIndex)
			{
				CONTROLLER.NonStrikerIndex = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
			else if (catcherID == 1 && batsmanOut == CONTROLLER.NonStrikerIndex)
			{
				CONTROLLER.NonStrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
		}
		else if (batsmanOut == CONTROLLER.StrikerIndex)
		{
			CONTROLLER.StrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
		}
		else if (batsmanOut == CONTROLLER.NonStrikerIndex)
		{
			CONTROLLER.NonStrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
		}
	}

	private void TMWicketBallSecondInnings(int batsmanID, int wicketType, int bowlerID, int catcherID, int batsmanOut)
	{
		CONTROLLER.wicketType = wicketType;
		if (Singleton<GameModel>.instance.NewBatsmanIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length - 1)
		{
			Singleton<GameModel>.instance.NewBatsmanIndex++;
		}
		if (Singleton<GameModel>.instance.NewBatsmanIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[Singleton<GameModel>.instance.NewBatsmanIndex].BatsmanList.TMStatus2 = "not out";
			if (CONTROLLER.StrikerIndex == batsmanOut)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipBalls = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipBalls = 0;
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipBalls = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipBalls = 0;
			}
		}
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets2++;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.TMFOW2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores2;
		if (wicketType == 4)
		{
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount = 0;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.TMStatus2 = "run out";
		}
		else
		{
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMWicket2++;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount++;
			switch (wicketType)
			{
			case 1:
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.TMStatus2 = "b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				break;
			case 2:
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.TMStatus2 = "lbw " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				break;
			case 3:
				catcherID = CONTROLLER.FieldersArray[catcherID];
				if (bowlerID == catcherID)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.TMStatus2 = "ct off b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.TMStatus2 = "ct off b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				}
				break;
			case 5:
				catcherID = CONTROLLER.FieldersArray[catcherID];
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.TMStatus2 = "ct behind b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				break;
			case 6:
				if (CONTROLLER.BowlerType == 1)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.TMStatus2 = "st, b " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.TMStatus2 = "lbw " + Singleton<GameModel>.instance.GetBowlerShortName(bowlerID);
				}
				break;
			}
		}
		if (CONTROLLER.currentInnings < 2)
		{
			Singleton<BattingScoreCard>.instance.SelectedInnings = 1;
		}
		else
		{
			Singleton<BattingScoreCard>.instance.SelectedInnings = 2;
		}
		Singleton<BattingScoreCard>.instance.UpdateWicket(batsmanOut);
		Singleton<BattingScoreCard>.instance.UpdateScoreCard();
		if (wicketType == 4)
		{
			if (catcherID == 0 && batsmanOut == CONTROLLER.StrikerIndex)
			{
				CONTROLLER.StrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
			else if (catcherID == 1 && batsmanOut == CONTROLLER.StrikerIndex)
			{
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
			else if (catcherID == 0 && batsmanOut == CONTROLLER.NonStrikerIndex)
			{
				CONTROLLER.NonStrikerIndex = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
			else if (catcherID == 1 && batsmanOut == CONTROLLER.NonStrikerIndex)
			{
				CONTROLLER.NonStrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
			}
		}
		else if (batsmanOut == CONTROLLER.StrikerIndex)
		{
			CONTROLLER.StrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
		}
		else if (batsmanOut == CONTROLLER.NonStrikerIndex)
		{
			CONTROLLER.NonStrikerIndex = Singleton<GameModel>.instance.NewBatsmanIndex;
		}
	}
}
