using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomAndGloom : BaseSkill
{
    public override void FaintSummon()
    {
        string[] twoHalf = mascot.BandGStatHelper.Split('/');
        mascot.GenerateMascot(twoHalf[0], mascot.opo, GetSlotPos());
		gameManager.AllySummonedActivate(mascot);
		List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
        for (int i = 0; i < 5; i++) 
        {
            if (!tray[i].activeSelf) 
            {
                tray[i].GetComponent<MascotDisplay>().GenerateMascot(twoHalf[1], mascot.opo, i);
                tray[i].SetActive(true);
				gameManager.AllySummonedActivate(tray[i].GetComponent<MascotDisplay>());
				break;
            }
        }
	}
}
