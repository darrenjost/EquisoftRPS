using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquisoftRPS
{
	class Player
	{
		int type; // 1 = human, 2 = basic cpu, 3 = random cpu
		int score  = 0;
		Weapon currentWeapon = null;
		Weapon lastWeapon = null;
		public Player(int type)
		{
			this.type = type;
		}

		public void SetWeapon(Weapon weapon)//set the active weapon for the player
		{
			this.currentWeapon = weapon;
		}
		public void SetLastWepon(Weapon weapon)//set the previous weapon for the player
		{
			this.lastWeapon = weapon;
		}
		public int GetPlayerType()//return player type human or cpu
		{
			return type;
		}

		public Weapon GetPlayerWeapon()//returns the players current weapon
		{
			return currentWeapon;
		}

		public Weapon GetPlayerLastWeapon()//returns the players previous weapon
		{
			return lastWeapon;
		}

		public  void Win()//give player 1 point
		{
			score++;
		}

		public int GetScore()//returns players current score
		{
			return score;
		}



	
	}
}
