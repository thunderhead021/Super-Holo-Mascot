using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSkill : BaseSkill
{
    public override void EndOfBattle()
    {
        if (mascot.GetEffect().Equals("7")) 
        {
			BuffSlot(GetSlotPos(), gameManager.playerTray, gameManager.GetAllSlot(), 1, 1);
		}
    }
}
