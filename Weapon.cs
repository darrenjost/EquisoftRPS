using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquisoftRPS
{
	class Weapon
	{
		string name; // rock, paper,scissors, flamethrower , etc...
		int type; // 1 = rock, 2 = paper, 3 == scissors, 4 == flamethrower
		List<int> beatenBy = new List<int>();//list of weapons that can beat this weapon, can be modified below if new weapons are added in the future

		public Weapon(int type)
		{
			this.type = type;

			switch (type)
			{
				case 1:
					this.name = "Rock";
					beatenBy.Add(2);//beaten by paper
					break;
				case 2:
					this.name = "Paper";
					beatenBy.Add(3);//beaten by scissors and flamethrower
					beatenBy.Add(4);
					break;
				case 3:
					this.name = "Scissors";
					beatenBy.Add(1);//beaten by rock
					break;
				case 4:
					this.name = "Flamethrower";
					beatenBy.Add(1);//beaten by scissors and rock
					beatenBy.Add(3);
					break;
				default:
					break;

			}
		}

		public List<int> GetBeatenBy()//return a list of all the weapons that can beat this weapon
		{
			return beatenBy;
		}

		public int GetWeaponType()//return type of weapon int
		{
			return type;
		}

		public string GetWeaponName()//return the name of the weapon
		{
			return name;
		}

		
	}
}
