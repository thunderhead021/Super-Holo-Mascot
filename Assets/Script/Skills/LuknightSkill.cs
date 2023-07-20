using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LuknightSkill : BaseSkill
{
    public override bool ProtectAlly(MascotDisplay whoHurt)
    {
        int allySlot = GetAllySlot(whoHurt);
        if (allySlot < 0)
            return false;
        if (mascot.opo)
        {
            if (gameManager.opoTray[allySlot + 1] != mascot.gameObject)
                return false;
            return true;
        }
        else 
        {
			if (gameManager.playerTray[allySlot + 1] != mascot.gameObject)
				return false;
			return true;
		}
    }

    public override void ShieldAlly(int dmg, MascotDisplay fromWho)
    {
        switch (level) 
        {
            case 1:
                mascot.ReciveDmg(dmg, fromWho.death, fromWho, true);
                break;
            case 2:
				mascot.ReciveDmg(dmg/2 <= 1 ? 1 : (dmg / 2), fromWho.death, fromWho, true);
				break;
            case 3:
				mascot.ReciveDmg(dmg / 2 <= 1 ? 1 : (dmg / 2), fromWho.death, fromWho, true);
                if (!mascot.death) 
                {
					mascot.poyoyoAnimation(mascot.opo);
					fromWho.ReciveDmg(int.Parse(mascot.atk.text), mascot.death, mascot);
				}
                break;
        }
    }

    private int GetAllySlot(MascotDisplay ally) 
    {
        int allySlot = 0;
        if (mascot.opo)
        {
            foreach (GameObject slot in gameManager.opoTray)
            {
                if (slot == ally.gameObject)
                    return allySlot;
                allySlot++;
            }
        }
        else 
        {
			foreach (GameObject slot in gameManager.playerTray)
			{
				if (slot == ally.gameObject)
					return allySlot;
				allySlot++;
			}
		}
        return -1;
    }
}
