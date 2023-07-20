using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoyamaShienSkill : BaseSkill
{
    public override void EndOfBattle()
    {
        int pos = GetSlotPos();
		string[] allSlot = gameManager.GetAllSlot();

		for (int i = 1; i <= level; i++) 
        {
            if (pos - i < 0)
                break;
			if (gameManager.playerTray[pos - i].activeSelf) 
			{
				MascotDisplay mascotDisplay = gameManager.playerTray[pos - i].GetComponent<MascotDisplay>();
				mascotDisplay.StatsBuff(1, 1, mascot);
				string info = allSlot[mascotDisplay.startSlot];
				string[] infos = info.Split('_');
				infos[1] = (int.Parse(infos[1]) + 1).ToString();
				infos[2] = (int.Parse(infos[2]) + 1).ToString();
				allSlot[mascotDisplay.startSlot] = gameManager.CreateNewInfo(infos);
			}
        }
		gameManager.CreateNewInfo();
	}
}
