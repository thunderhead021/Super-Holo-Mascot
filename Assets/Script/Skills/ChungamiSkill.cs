using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChungamiSkill : BaseSkill
{
    public override void StartOfShop()
    {
        if (level <= 2) 
        {
            gameManager.AddExp(mascot);
        }
    }

    public override void StartOfBattle()
    {
        if (level == 3) 
        {
            int atk = 1;
            int hp = 1;
            foreach (GameObject slot in mascot.opo ? gameManager.opoTray : gameManager.playerTray) 
            {
                if (slot.gameObject.activeSelf && slot != mascot.gameObject) 
                {
                    if ((slot.GetComponent<MascotDisplay>().GetAtk() + slot.GetComponent<MascotDisplay>().GetHp()) > (atk + hp)) 
                    {
                        atk = slot.GetComponent<MascotDisplay>().GetAtk();
                        hp = slot.GetComponent<MascotDisplay>().GetHp();
                    }
                }
            }

			mascot.art.sprite = mascot.opo ? Token.opoArt : Token.playerArt;
			mascot.ChangeStats(atk.ToString(), hp.ToString());
			gameManager.AllySummonedActivate(mascot);
		}
    }
}
