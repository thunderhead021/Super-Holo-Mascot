using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InuSkill : BaseSkill
{
    public override void Sell()
    {
        int members = gameManager.GetNumOfSSMember(false);
		if (members > 0) 
        {
            int rand = members == 1 ? 0 : Random.Range(0, members);
            for (int i = 0; i < 5; i++) 
            {
                if (rand == 0 && gameManager.playerTray[i].activeSelf && gameManager.playerTray[i] != mascot.gameObject && gameManager.playerTray[i].GetComponent<MascotDisplay>().GetEffect().Equals("7"))
                {
                    gameManager.playerTray[i].GetComponent<MascotDisplay>().StatsBuff(1 * level, 2 * level, mascot);
					break;
                }
                else 
                {
                    if (rand > 0)
                        rand--;
                }
			}
		}
    }
}
