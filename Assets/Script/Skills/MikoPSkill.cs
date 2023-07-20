using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikoPSkill : BaseSkill
{
    public override void BeforeAtk()
    {
        int pos = GetSlotPos();
        List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
        if (mascot.timeUseSkill - 3 + level > 0) 
        {
			for (int i = pos + 1; i < 5; i++)
			{
				if (tray[i].activeSelf)
				{
					tray[i].GetComponent<MascotDisplay>().ReciveDmg(1, mascot.death, mascot, true);
					mascot.timeUseSkill -= 1;
					break;
				}
			}
		}    
    }
}
