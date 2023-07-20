using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazoSkill : BaseSkill
{
    public override void Buy()
    {
        int newHp = 1;
        foreach (GameObject slot in gameManager.playerTray) 
        {
			if (slot.gameObject.activeSelf && slot != mascot.gameObject)
			{
				if (slot.GetComponent<MascotDisplay>().GetHp()> newHp)
				{
					newHp = slot.GetComponent<MascotDisplay>().GetHp();
				}
			}
		}
		if (newHp > 1)
			mascot.StatsBuff(0, (int)(newHp * 0.5f * level) - 1);
    }
}
