using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iofi : BaseSkill
{
    public override void FaintSummon()
    {
		switch (mascot.mascot.tier) 
		{
			case 1:
				mascot.mascot = gameManager.mascotTier1[Random.Range(0, gameManager.mascotTier1.Count)];
				break;
			case 2:
				mascot.mascot = gameManager.mascotTier2[Random.Range(0, gameManager.mascotTier2.Count)];
				break;
			case 3:
				mascot.mascot = gameManager.mascotTier3[Random.Range(0, gameManager.mascotTier3.Count)];
				break;
			case 4:
				mascot.mascot = gameManager.mascotTier4[Random.Range(0, gameManager.mascotTier4.Count)];
				break;
			case 5:
				mascot.mascot = gameManager.mascotTier5[Random.Range(0, gameManager.mascotTier5.Count)];
				break;
		}
		mascot.CreateMascot(mascot.opo, true);
		mascot.gameObject.SetActive(true);
		gameManager.AllySummonedActivate(mascot);
	}
}
