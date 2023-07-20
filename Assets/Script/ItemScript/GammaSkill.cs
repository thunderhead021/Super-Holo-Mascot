using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaSkill : ItemTriggerBase
{
    public override void Single(MascotDisplay mascot)
    {
        mascot.ChangeStats(mascot.hp.text, mascot.atk.text);
    }
}
