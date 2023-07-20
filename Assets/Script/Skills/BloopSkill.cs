using UnityEngine;

public class BloopSkill : BaseSkill
{
    public override void StartOfBattle()
    {
        int pos = GetSlotPos();
        if (pos - 1 >= 0) 
        {
            GameObject buffSlot = getBuffSlot(pos);
            if (buffSlot != null) 
            {
                buffSlot.GetComponent<MascotDisplay>().StatsBuff((int)(mascot.GetAtk() * 0.5f * level), (int)(mascot.GetHp() * 0.5f * level), mascot);
            }
		}
        mascot.death = true;
    }

    private GameObject getBuffSlot(int pos)
    {        
        int i = 1;
        while (pos - i >= 0) 
        {
			GameObject buffSlot = mascot.opo ? gameManager.opoTray[pos - i] : gameManager.playerTray[pos - i];
            if (buffSlot.activeSelf)
                return buffSlot;
            else
                i++;
		}
        return null;
	}
}
