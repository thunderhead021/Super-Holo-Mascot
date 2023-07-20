using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendSkill : BaseSkill
{
	public override void LvlUp()
	{
		string[] allSlot = gameManager.GetAllSlot();
		foreach (GameObject slot in mascot.opo ? gameManager.opoTray : gameManager.playerTray) 
		{
			if (slot.activeSelf && slot != mascot.gameObject) 
			{
				slot.GetComponent<MascotDisplay>().StatsBuff(level - 1, level - 1, mascot);
				if (!slot.GetComponent<MascotDisplay>().opo && gameManager.IsBattle())
				{
					string info = allSlot[slot.GetComponent<MascotDisplay>().startSlot];
					string[] infos = info.Split('_'); 
					infos[1] = (int.Parse(infos[1]) + level - 1).ToString();
					infos[2] = (int.Parse(infos[2]) + level - 1).ToString();
					allSlot[slot.GetComponent<MascotDisplay>().startSlot] = gameManager.CreateNewInfo(infos);
				}
			}
		}
		gameManager.CreateNewInfo();
	}
}
