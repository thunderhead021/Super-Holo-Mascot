using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEKOSkill : BaseSkill
{
	public override void BeforeAtk()
	{
		if (mascot.timeUseSkill > 0 && isSoleFromSide()) 
		{
			mascot.StatsBuff((int)(int.Parse(mascot.atk.text) * 0.5 * level), 0);
			mascot.timeUseSkill -= 1;
		}
		
	}

	private bool isSoleFromSide() 
	{
		if (mascot.opo)
		{
			foreach (GameObject slot in gameManager.opoTray)
			{
				if (slot.activeSelf && slot != mascot.gameObject)
					return false;
			}
		}
		else 
		{
			foreach (GameObject slot in gameManager.playerTray)
			{
				if (slot.activeSelf && slot != mascot.gameObject)
					return false;
			}
		}
		return true;
	}
}
