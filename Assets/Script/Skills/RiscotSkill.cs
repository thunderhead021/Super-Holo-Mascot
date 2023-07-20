using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiscotSkill : BaseSkill
{
    public Item nut;
    public Item betterNut;
    public Item ultNut;

    public override void Buy()
    {
        gameManager.shop.ReplaceItemsShop(level == 1 ? nut : (level == 2 ? betterNut : ultNut));
    }
}
