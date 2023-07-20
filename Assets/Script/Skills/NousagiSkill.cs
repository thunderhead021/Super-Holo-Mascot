using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NousagiSkill : BaseSkill
{
    public override int StatBuffTime()
    {
        return 1 + level;
    }
}
