using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotatsuSkill : BaseSkill
{
    public override void StartOfShop()
    {
        gameManager.coin += level;
        gameManager.coinShow.ChangeAmount(gameManager.coin);
    }
}
