using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiofaSkill : BaseSkill
{
	public override void Sell()
	{
		gameManager.freeRefreshTimes += level;
	}
}
