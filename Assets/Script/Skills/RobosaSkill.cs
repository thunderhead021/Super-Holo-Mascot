using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobosaSkill : BaseSkill
{
    public Material Shield;

    public override void Faint()
    {
        int pos = GetSlotPos();
		List<GameObject> tray = mascot.opo ?  gameManager.opoTray : gameManager.playerTray;

		for (int i = 1; i <= level; i++) 
        {
            if (pos + i <= 4 && tray[pos + i].activeSelf) 
            {
                AddEffectSlot(pos + i, tray, gameManager.GetAllSlot(), Shield, -2);
            }
        }
    }
}
