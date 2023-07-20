using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaifukuSkill : BaseSkill
{
    public override void StartOfShop()
    {
		Shop shop = gameManager.shop;
		if (shop.GetAllMascotInFreezer() >= 0)
		{
			shop.slotWReduceCost[0] = (shop.GetAllMascotInFreezer()) == 1 ? 1 : Random.Range(1, shop.GetAllMascotInFreezer());
			shop.slotWReduceCost[1] = 3 - mascot.level;
		}
	}  
}
