using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorosSkill : BaseSkill
{
	public override void AllySummoned(MascotDisplay ally)
	{
		if (!gameManager.IsBattle() && ally.mascot.id != Token.id && ally.level < 3) 
		{
			gameManager.AddExp(ally, level);
		}
	}
}
