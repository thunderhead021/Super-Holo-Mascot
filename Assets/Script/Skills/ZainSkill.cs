using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZainSkill : BaseSkill
{
    public override void ReceiveItem()
    {
        int max = GetNumOfSlot(false) - 1;
		if (max >= 1) 
		{
			int rand1 = max == 1 ? 0 : Random.Range(0, max);	
			GameObject slot1 = null;
			for (int i = 0; i < 5; i++)
			{
				if (rand1 == 0 && gameManager.playerTray[i].activeSelf && gameManager.playerTray[i] != mascot.gameObject)
				{
					slot1 = gameManager.playerTray[i];
					gameManager.playerTray[i].GetComponent<MascotDisplay>().StatsBuff(1 * level, 1 * level, mascot);
					break;
				}
				if (rand1 > 0)
					rand1--;
			}

			if (max - 1 > 0) 
			{
				int rand2 = max - 1 > 1 ? Random.Range(0, max - 1) : 0;
				for (int i = 4; i >= 0; i--)
				{
					if (rand2 == 0 && gameManager.playerTray[i].activeSelf && gameManager.playerTray[i] != mascot.gameObject && gameManager.playerTray[i] != slot1)
					{
						gameManager.playerTray[i].GetComponent<MascotDisplay>().StatsBuff(1 * level, 1 * level, mascot);
						break;
					}
					if (rand2 > 0)
						rand2--;
				}
			}	
		}		
	}	
}
