using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokobeeSkill : BaseSkill
{
    public override void Knockout(int excessDmg)
    {
		int slot = GetSlotPos();
		List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
		string[] allSlot = gameManager.GetAllSlot();
		int buff = level + gameManager.GetNumOfSSMember(mascot.opo);
		if (tray[slot].activeSelf)
		{
			tray[slot].GetComponent<MascotDisplay>().StatsBuff(buff, buff);
			if (!tray[slot].GetComponent<MascotDisplay>().opo)
			{
				string info = allSlot[tray[slot].GetComponent<MascotDisplay>().startSlot];
				string[] infos = info.Split('_'); 
				infos[1] = (int.Parse(infos[1]) + buff).ToString();
				infos[2] = (int.Parse(infos[2]) + buff).ToString();
				allSlot[tray[slot].GetComponent<MascotDisplay>().startSlot] = gameManager.CreateNewInfo(infos);
			}
		}
		gameManager.CreateNewInfo();
	}
}
