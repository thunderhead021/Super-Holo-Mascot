using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuCornSkill : BaseSkill
{
	public override void Faint()
	{
		int pos = GetSlotPos();
		string[] allSlot = gameManager.GetAllSlot();
		List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
		if (pos + 2 <= 4)
		{
			BuffSlot(pos + 2, tray, allSlot, 1 * level, 1 * level, mascot);
			BuffSlot(pos + 1, tray, allSlot, 1 * level, 1 * level, mascot);
		}
		else if(pos + 1 <= 4)
		{
			BuffSlot(pos + 1, tray, allSlot, 1 * level, 1 * level, mascot);
		}	
	}
}
