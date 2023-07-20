using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfriendSkill : BaseSkill
{
    public override void AllyLvlUp(MascotDisplay mascot)
    {
        if (this.mascot != mascot)
            BuffSlot(mascot.startSlot, this.mascot.opo ? gameManager.opoTray : gameManager.playerTray, gameManager.GetAllSlot(), 2, 2, this.mascot);
    }
}
