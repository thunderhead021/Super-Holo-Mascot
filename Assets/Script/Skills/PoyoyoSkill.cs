using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoyoyoSkill : BaseSkill
{
	public override void BeforeAtk()
	{
		if (mascot.timeUseSkill - 3 + level > 0)
		{
			MascotDisplay player = gameManager.playerBattleSlot.GetComponent<MascotDisplay>();
			MascotDisplay opo = gameManager.opoBattleSlot.GetComponent<MascotDisplay>();
			if (mascot == player)
			{
				player.poyoyoAnimation(false);
				opo.ReciveDmg(int.Parse(player.atk.text), player.death, player, true);	
			}
			else
			{
				opo.poyoyoAnimation(true);
				player.ReciveDmg(int.Parse(opo.atk.text), opo.death, opo, true);
			}
			mascot.timeUseSkill -= 1;
		}
	}
}
