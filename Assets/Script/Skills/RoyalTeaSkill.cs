using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalTeaSkill : BaseSkill
{
    public override void StartOfShop()
    {
        mascot.StatsBuff(1, 0);
    }
}
