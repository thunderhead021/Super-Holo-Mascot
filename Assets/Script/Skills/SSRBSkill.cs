using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSRBSkill : BaseSkill
{
    public override void Faint()
    {
        gameManager.DmgToAll(2 * level, mascot);
    }
}
