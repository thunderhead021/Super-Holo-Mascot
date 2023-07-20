using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaatonSkill : BaseSkill
{
	public Mascot head1;
	public Mascot head2;
	public Mascot head3;
	public override void FaintSummon()
	{
		foreach (GameObject slot in mascot.opo ? gameManager.opoTray : gameManager.playerTray)
		{
			if (!slot.activeSelf || slot.GetComponent<MascotDisplay>().death)
			{
				MascotDisplay token = slot.GetComponent<MascotDisplay>();
				token.mascot = level == 1 ? head1 : (level == 2 ? head2 : head3);
				token.CreateMascot(mascot.opo, true);
				token.SetLvl(level, 0);
				token.gameObject.SetActive(true);
				gameManager.AllySummonedActivate(token);
				break;
			}
		}
	}
}
