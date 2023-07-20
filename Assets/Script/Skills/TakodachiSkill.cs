using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakodachiSkill : BaseSkill
{
    public Mascot takodachi1;
    public Mascot takodachi2;
    public Mascot takodachi3;


	public override void FaintSummon()
    {
        int sumNum = 2;
        foreach (GameObject slot in mascot.opo ? gameManager.opoTray : gameManager.playerTray) 
        {
            if (sumNum > 0 && (!slot.activeSelf || slot.GetComponent<MascotDisplay>().death)) 
            {
				MascotDisplay token = slot.GetComponent<MascotDisplay>();
				token.mascot = level == 1 ? takodachi1 : (level == 2 ? takodachi2 : takodachi3);
				token.CreateMascot(mascot.opo, true);
				token.SetLvl(level, 0);
				token.gameObject.SetActive(true);
				gameManager.AllySummonedActivate(token);
				sumNum--;
			}
        }
    }
}
