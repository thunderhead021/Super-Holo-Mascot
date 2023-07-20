using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moona : BaseSkill
{
    public override void Knockout(int excessDmg)
    {
        if (excessDmg > 0) 
        {
            GameObject enemy = mascot.opo ? gameManager.playerTray[1] 
                : gameManager.opoTray[1];
            if (enemy.activeSelf && !enemy.GetComponent<MascotDisplay>().death) 
            {
                gameManager.AttackCall(mascot, enemy.GetComponent<MascotDisplay>(), int.Parse(enemy.GetComponent<MascotDisplay>().atk.text), excessDmg);
			}
        }
    }
}
