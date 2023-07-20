using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanmoSkill : BaseSkill
{
    public override void EndOfBattle()
    {
		string[] allSlot = gameManager.GetAllSlot();
		for (int i = 0; i < 5; i++) 
		{
			if (gameManager.playerTray[i].activeSelf && gameManager.playerTray[i].GetComponent<MascotDisplay>().GetEffect().Equals("7"))
			{
				BuffSlot(i, gameManager.playerTray, allSlot, 1, 2, mascot);
			}
		}
	}
}
