using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokoroSkill : BaseSkill
{
    public override void StartOfBattle()
    {
        int dmg = 2 * level * gameManager.GetNumOfSSMember(mascot.opo);
        if (dmg > 0) 
        {
            int rand = Random.Range(0, GetNumOfSlot(mascot.opo ? false : true));
            MascotDisplay target = mascot.opo ? gameManager.playerTray[rand].GetComponent<MascotDisplay>() :
                gameManager.opoTray[rand].GetComponent<MascotDisplay>();
            target.ReciveDmg(dmg, mascot.death, mascot, true);
		}
    }
}
