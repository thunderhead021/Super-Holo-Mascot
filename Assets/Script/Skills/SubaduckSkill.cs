using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubaduckSkill : BaseSkill
{
	public override void StartOfBattle()
	{
		gameManager.Push(mascot, level);
	}
}
