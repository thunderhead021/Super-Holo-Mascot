using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugTypeAsacocoSkill : ItemTriggerBase
{
    public override void Single(MascotDisplay mascot)
    {
        mascot.death = true;
        gameManager.DeathInShop(mascot);
	}
}
