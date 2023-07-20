using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbifrionSkill : BaseSkill
{
	public override void Faint()
	{
		int pos = GetSlotPos();
		int rand = GetActiveMascotNum(mascot.opo) == 1 ? 0 : Random.Range(0, GetActiveMascotNum(mascot.opo));
		string[] allSlot = gameManager.GetAllSlot();
		List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
		for (int i = 0; i < 5; i++)
		{
			if (rand < 0)
				break;
			if (i == pos)
				continue;
			if (rand == 0) 
			{
				if (!tray[i].activeSelf)
					continue;
				BuffSlot(i, tray, allSlot, 2, 1, mascot);
			}
			rand--;
		}
	}
}
