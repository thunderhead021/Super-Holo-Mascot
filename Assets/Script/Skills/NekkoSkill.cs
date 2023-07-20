using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekkoSkill : BaseSkill
{
    public override void Sell()
    {
		if (!mascot.effectActivated) 
		{
			for (int i = 0; i < level; i++)
			{
				CreateNewNekko();
			}
		}
    }

    private void CreateNewNekko() 
    {
		foreach (GameObject slot in gameManager.playerTray)
		{
			if (!slot.activeSelf)
			{
				gameManager.AddToPlayer(Token, null, slot);
				break;
			}
		}
	}
}
