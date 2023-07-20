using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemuSkill : BaseSkill
{
	public Item apple1;
	public Item apple2;

	public override void Sell()
	{
		switch (level) 
		{
			case 1:
				int rand = Random.Range(0, 100);
				int i = rand < 50 ? 0 : 1;
				gameManager.shop.ReplaceItem(i, apple1);
				break;
			case 2:
				gameManager.shop.ReplaceItemsShop(apple1);
				break;
			case 3:
				gameManager.shop.ReplaceItemsShop(apple2);
				break;
		}
	}
}
