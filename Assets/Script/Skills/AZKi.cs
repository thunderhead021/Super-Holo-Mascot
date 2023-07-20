using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AZKi : BaseSkill
{
	public override void StartOfBattle()
	{
		int rand = Random.Range(1, GetActiveMascotNum(mascot.opo));
		gameManager.Pull(rand, 0, mascot);
	}
}
