using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiyamaruSkill : BaseSkill
{
	public override void StartOfBattle()
	{
		int dmg = GetAllLevel();
		int max = GetNumOfSlot(mascot.opo ? false : true);
		int rand2;
		int rand1 = max == 1 ? 0 : Random.Range(0, max);
		GameObject slot1 = null;
		GameObject slot2 = null;
		foreach (GameObject slot in mascot.opo ? gameManager.playerTray : gameManager.opoTray) 
		{
			if (slot.activeSelf) 
			{
				if (rand1 == 0)
				{
					slot.GetComponent<MascotDisplay>().ReciveDmg(dmg, mascot.death, mascot, true);
					slot1 = slot;
					break;
				}
				else if(rand1 > 0)
				{
					rand1--;
				}
			}
		}

		if (level >= 2 && max - 1 >= 1) 
		{
			rand2 = Random.Range(0, max - 1);
			foreach (GameObject slot in mascot.opo ? gameManager.playerTray : gameManager.opoTray)
			{
				if (slot.activeSelf)
				{
					if (rand2 == 0 && slot != slot1)
					{
						slot.GetComponent<MascotDisplay>().ReciveDmg(dmg, mascot.death, mascot, true);
						slot2 = slot;
						break;
					}
					else if (rand2 > 0)
					{
						rand2--;
					}
				}
			}
		}
		if (level == 3 && max - 2 >= 1) 
		{
			int rand3 = Random.Range(0, max - 2);
			foreach (GameObject slot in mascot.opo ? gameManager.playerTray : gameManager.opoTray)
			{
				if (slot.activeSelf)
				{
					if (rand3 == 0 && slot != slot1 && slot != slot2)
					{
						slot.GetComponent<MascotDisplay>().ReciveDmg(dmg, mascot.death, mascot, true);
						break;
					}
					else if (rand3 > 0)
					{
						rand3--;
					}
				}
			}
		}
		
	}

	private int GetAllLevel() 
	{
		int result = 0;
		foreach (GameObject slot in mascot.opo ? gameManager.opoTray : gameManager.playerTray) 
		{
			if (slot.activeSelf) 
			{
				result += slot.GetComponent<MascotDisplay>().level;
			}
		}
		return result;
	}
}
