using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmeWatchSkill : BaseSkill
{
    public override void FaintSummon()
    {
		foreach (GameObject slot in mascot.opo ? gameManager.opoTray : gameManager.playerTray)
		{
			if (!slot.activeSelf || slot.GetComponent<MascotDisplay>().death)
			{
				MascotDisplay token = slot.GetComponent<MascotDisplay>();
				if (!token.itemEffectActivated)
				{
					token.death = false;
					token.ChangeStats((1).ToString(), (1).ToString());
					token.gameObject.SetActive(true);
					token.itemEffectActivated = true;
					gameManager.AllySummonedActivate(token);
					break;
				}
			}
		}
	}
}
