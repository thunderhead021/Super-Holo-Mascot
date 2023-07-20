using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoruneuSkill : BaseSkill
{
    public override void Buy()
    {
        int slot = GetSlotPos();
        if (slot + 1 <= 4 && gameManager.playerTray[slot + 1].activeSelf) 
        {
			gameManager.playerTray[slot + 1].GetComponent<MascotDisplay>().StatsBuff(level, level, mascot);
		}
		if (slot - 1 >= 0 && gameManager.playerTray[slot - 1].activeSelf)
		{
			gameManager.playerTray[slot - 1].GetComponent<MascotDisplay>().StatsBuff(level, level, mascot);
		}
	}
}
