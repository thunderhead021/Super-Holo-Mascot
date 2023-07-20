using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KapuMinSkill : BaseSkill
{
	public override void DealDmg()
	{
		if (mascot.timeUseSkill - 1 - 1 * level >= 0) 
		{
			mascot.StatsBuff(0, 1 + 1 * level);
			mascot.timeUseSkill -= (1 + 1 * level);
		}
		
	}
}
