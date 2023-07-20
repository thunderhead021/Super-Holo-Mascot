using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyomuSuzumeSkill : BaseSkill
{
	public override void StartOfBattle()
	{
		int buffNum = 0;
		List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
		foreach (GameObject slot in tray) 
		{
			if (slot.activeSelf) 
			{
				MascotDisplay slotMascot = slot.GetComponent<MascotDisplay>();
				if(slotMascot.IsOverided("Faint") || slotMascot.IsOverided("FaintSummon"))
					buffNum++;
			}
		}
		if(buffNum > 0)
			BuffSlot(GetSlotPos(), tray, gameManager.GetAllSlot(), buffNum, buffNum);
	}
}
