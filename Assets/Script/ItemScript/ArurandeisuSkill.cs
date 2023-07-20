using UnityEngine;

public class ArurandeisuSkill : ItemTriggerBase
{
    public override void Multiple()
    {
        foreach (GameObject slot in gameManager.playerTray) 
        {
            if (slot.activeSelf) 
            {
                item.Buff(slot.GetComponent<MascotDisplay>(), 1, 0);
			}
        }
    }
}
