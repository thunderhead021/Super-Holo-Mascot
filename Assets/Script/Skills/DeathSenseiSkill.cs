using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSenseiSkill : BaseSkill
{
    public override void AnyFaint(MascotDisplay other)
    {
        int rand = Random.Range(0, 100);
        BuffSlot(GetSlotPos(), mascot.opo ? gameManager.opoTray : gameManager.playerTray, gameManager.GetAllSlot(), rand < 50 ? 0 : 1, rand < 50 ? 1 : 0);
    }
}
