using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DokurokunSkill : BaseSkill
{
    public override void StartOfShop()
    {
        gameManager.coin += 1;
        gameManager.coinShow.ChangeAmount(gameManager.coin);
    }
}
