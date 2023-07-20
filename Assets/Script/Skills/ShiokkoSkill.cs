using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiokkoSkill : BaseSkill
{
	public override void AllySummoned(MascotDisplay ally)
	{
		if (mascot.gameObject.activeSelf && ally != mascot && (ally.opo == mascot.opo)) 
		{
			ally.StatsBuff(2 * level, 3 * level, mascot);
			string[] allSlot = gameManager.GetAllSlot();
			string info = allSlot[ally.startSlot];
			if (!info.Equals("")) 
			{
				string[] infos = info.Split('_');  //get infos[1] ATK and infos[2] HP
				infos[1] = (int.Parse(infos[1]) + 2 * level).ToString();
				infos[2] = (int.Parse(infos[2]) + 3 * level).ToString();
				allSlot[ally.startSlot] = gameManager.CreateNewInfo(infos);
			}
			
		}	
	}
}