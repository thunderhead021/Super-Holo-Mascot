using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotoriSkill : BaseSkill
{
    public override void FaintSummon()
    {
        int numSum = level;
        int atk = int.Parse(mascot.atk.text)/2;
        foreach (GameObject slot in mascot.opo ? gameManager.opoTray : gameManager.playerTray) 
        {
            if (numSum <= 0)
                break;
            if (numSum > 0 && (!slot.activeSelf || slot.GetComponent<MascotDisplay>().death)) 
            {
                MascotDisplay token = slot.GetComponent<MascotDisplay>();
				token.mascot = Token;
				token.CreateMascot(mascot.opo, true);
				token.SetLvl(1, 0);
                token.ChangeStats(atk.ToString(), 1.ToString());
				token.gameObject.SetActive(true);
				gameManager.AllySummonedActivate(token);
                Debug.Log(token.mascot.id);
                numSum--;
			}
        }
    }
}
