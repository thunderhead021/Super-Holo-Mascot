using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BibiSkill : BaseSkill
{
    public override void AllySummoned(MascotDisplay ally)
    {
		if (HaveEffect() && mascot.gameObject.activeSelf && !gameManager.IsBattle() && ally != mascot)
		{
			if (mascot.timeUseSkill == 0 && !mascot.effectActivated)
			{
				mascot.effectActivated = true;
				mascot.timeUseSkill = level;
			}
			if (mascot.timeUseSkill > 0 && mascot.effectActivated)
			{
				Item item = gameManager.itemList[int.Parse(mascot.GetEffect())];
				ally.AddEffect(item.effect, item.id);
				mascot.timeUseSkill -= 1;
			} 
		}
	}

	private bool HaveEffect() 
	{
		return !mascot.GetEffect().Equals("n");
	}
}
