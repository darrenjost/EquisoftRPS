using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquisoftRPS
{
	class Match
	{
		int gameSelection; //1 for player vs player, 2 for player vs basic cpu, 3 for player vs random cpu
		int weaponChoice; //players current choice of weapon 		
		const int totalweapons = 4; // 4 types of weapons, can be changed in the future
		int round = 1; //current round
		Player player1; //always human
		Player player2; //can be human or cpu
		List<Weapon> weapons = new List<Weapon>();
		Random rnd = new Random();// for selecting random weapons

		public Match()
		{
			while (true)//loop forever, can exit console if finished playing
			{
				Setup();//set up the game
				Console.Clear();
			}
		}

		private void Setup()
		{
			round = 1;//initialize round to 1
			Console.WriteLine("***Rock Paper Scissors***\n");//create menu
			Console.WriteLine("To begin, please choose the number of players:");
			

			while (true)
			{
				Console.WriteLine("Player vs Player (enter 1)");
				Console.WriteLine("Player vs Basic Computer (enter 2)");
				Console.WriteLine("Player vs Random Computer (enter 3)");

				

				bool success = int.TryParse(Console.ReadLine(), out gameSelection);//read selection and verify if integer
				if (success)
				{
					if (gameSelection == 1 || gameSelection == 2 || gameSelection == 3)//must be 1 of 3 game types
					{
						CreatePlayers();//create player 1 and 2 depending on selection will be all human or human vs cpu
						CreateWeapons();//create all weapons and what can beat them
						StartGame();//begin the match after players and weapons are created
						
						break;
					}

					else
					{
						Console.Clear();
						Console.WriteLine("Invalid selection please try again...");//must choose valid game between 1 and 3
					}
				}

				else
				{
					Console.Clear();
					Console.WriteLine("Selection my be numerical...");
				}
			}			

		}

		private void StartGame()
		{
			while (player1.GetScore() < 3 && player2.GetScore() < 3)//check if either player has reached 3 points
			{				
				Console.Clear();
				Console.WriteLine("Round: " + round);//display current round
				Console.WriteLine("Score: Player 1- " + player1.GetScore() + " Player 2- " + player2.GetScore());//display players scores
				Console.WriteLine();
				Console.WriteLine("Player 1 please choose a weapon:");//prompt player 1 for their weapon

				while (true)
				{
					Console.WriteLine("Rock (enter 1)");
					Console.WriteLine("Paper (enter 2)");
					Console.WriteLine("Scissors (enter 3)");
					Console.WriteLine("Flamethrower (enter 4)");

					bool success = int.TryParse(Console.ReadLine(), out weaponChoice);		//weapon must be integer		
					
					if (success)
					{
						if (weaponChoice >= 1 && weaponChoice <= totalweapons)//weapon must be within the list of total weapons
						{
							player1.SetWeapon(weapons[weaponChoice - 1]);//assign the weapon to the player for this round
							player1.SetLastWepon(player1.GetPlayerWeapon());//not needed to keep history of last weapon for player 1 but implemented anyways
							Console.Clear();
							break;
						}
						else
						{
							Console.Clear();
							Console.WriteLine("Invalid selection please try again...");
						}
					}
					else
					{
						Console.Clear();
						Console.WriteLine("Selection my be numerical...");
					}
				}

				switch (player2.GetPlayerType())//depending on if human or cpu logic will change
				{
					case 1://player 2 is a human, treat the same as player 1
						//Console.Clear();
						Console.WriteLine("Player 2 please choose a weapon:");

						while (true)
						{
							Console.WriteLine("Rock (enter 1)");
							Console.WriteLine("Paper (enter 2)");
							Console.WriteLine("Scissors (enter 3)");
							Console.WriteLine("Flamethrower (enter 4)");

							bool success = int.TryParse(Console.ReadLine(), out weaponChoice);//same as before must be integer

							Console.Clear();

							if (success)
							{
								{
									if (weaponChoice >= 1 && weaponChoice <= totalweapons)//same as before must be within weapons range
									{
										player2.SetWeapon(weapons[weaponChoice - 1]);//assign weapon to player 2 based on choise
										player2.SetLastWepon(player2.GetPlayerWeapon());//save last weapon 
										break;
									}
									else
									{
										Console.Clear();
										Console.WriteLine("Invalid selection please try again...");
									}
								}
							}
							else
							{
								Console.Clear();
								Console.WriteLine("Selection my be numerical...");
							}
						}
						break;

					case 2://player 2 is basic cpu, always choose what beats previous weapon (if first move random selection)
						if (player2.GetPlayerLastWeapon() == null)
						{
							player2.SetWeapon(weapons[rnd.Next(0, totalweapons)]);//choose random weapon for first weapon
						}
						else
						{
							List<int> beatenBy = player2.GetPlayerLastWeapon().GetBeatenBy();//get the players last weapon and then a list of weapons that can beat it
							Weapon randWeapon = new Weapon(beatenBy[rnd.Next(0, beatenBy.Count)]);//choose a random weapon from the list of weapons that can beat the previously used weapon
							player2.SetWeapon(randWeapon);
						}
						player2.SetLastWepon(player2.GetPlayerWeapon());//set last weapon to new weapon
						break;

					case 3://player 3 is random cpu, always choose random weapon
						player2.SetWeapon(weapons[rnd.Next(0, totalweapons)]);
						player2.SetLastWepon(player2.GetPlayerWeapon());
						break;
				}

				DetermineWinner(player1, player2);//find winner from both players
			}

			if (player1.GetScore() == 3)//once a player has reached 3 points display the winer
			{
				Console.WriteLine("Player 1 wins this game!");
			}
			else if (player2.GetScore() == 3)
			{
				Console.WriteLine("Player 2 wins this game!");
			}
			Console.WriteLine("Press any key to continue...");
			Console.ReadLine();

		}

		private void DetermineWinner(Player player1, Player player2)
		{
			Console.WriteLine("Player 1 chooses: " + player1.GetPlayerWeapon().GetWeaponName());//display both players weapons
			Console.WriteLine("Player 2 chooses: " + player2.GetPlayerWeapon().GetWeaponName());
			if (player1.GetPlayerWeapon().GetBeatenBy().Contains(player2.GetPlayerWeapon().GetWeaponType()))// if player2's weapon is found within the list of weapons that can beat player2s weapon, then player 2 is the winner
			{
				player2.Win();
				Console.WriteLine("Player 2 wins this round!");

			}
			else if (player2.GetPlayerWeapon().GetBeatenBy().Contains(player1.GetPlayerWeapon().GetWeaponType()))// if player1's weapon is found within the list of weapons that can beat player2s weapon, then player 2 is the winner
			{
				player1.Win();
				Console.WriteLine("Player 1 wins this round!");
			}
			else//neither player can beat the other so it is a tie
			{
				Console.WriteLine("This round is a tie!");
			}

			round++;
			Console.WriteLine("Press any key to continue...");//pause for next round
			Console.ReadLine();
		}

		private void CreateWeapons()
		{
			for (int i = 1; i <= totalweapons; i++)//loop through all weapons and create them in the list
			{
				Weapon weapon = new Weapon(i);
				weapons.Add(weapon);
			}
			
		}

		private void CreatePlayers()//create plyer 1 as human, player 2 depends on selection
		{
			player1 = new Player(0);
			player2 = new Player(gameSelection);
		}
	}
}
