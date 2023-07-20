using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnkimoSkill : BaseSkill
{
	public override void EndOfBattle()
	{
		string[] allSlot = gameManager.GetAllSlot();
		for (int i = 0; i < 5; i++)
		{
			BuffSlot(i, gameManager.playerTray, allSlot, 1, 1, mascot);
		}
	}
}
