using System.Collections.Generic;
using UnityEngine;

public class SpannerSkill : BaseSkill
{
    public Material Shield;
    public override void AnyFaint(MascotDisplay other)
    {
        int pos = GetSlotPos();
		List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
        if (pos - 1 >= 0 && tray[pos - 1] == other.gameObject) 
        {
            AddEffectSlot(pos, tray, gameManager.GetAllSlot(), Shield, -2);
            BuffSlot(pos, tray, gameManager.GetAllSlot(), 1, 0);
        }
	}
}
