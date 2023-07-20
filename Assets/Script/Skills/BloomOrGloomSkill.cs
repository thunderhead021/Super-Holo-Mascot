using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomOrGloomSkill : BaseSkill
{
    public Mascot otherHalf;
    public Mascot BloomAndGloomv1;
    public Mascot BloomAndGloomv2;

	public override void StartOfBattle()
    {
        int pos = GetSlotPos();
        Mascot combine = null;
		List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
        for (int i = pos; i < 5; i++) 
        {
            if (tray[i].activeSelf) 
            {
                MascotDisplay other = tray[i].GetComponent<MascotDisplay>();
                if (other.mascot.id == otherHalf.id) 
                {
					combine = (level == 3 && other.level == 3) ? BloomAndGloomv2 : BloomAndGloomv1;
					mascot.BandGStatHelper = mascot.GetMascotInfo() + "/" + other.GetMascotInfo();
					mascot.mascot = combine;
					mascot.CreateMascot(mascot.opo, true);				
					mascot.ChangeStats((int.Parse(mascot.atk.text) + int.Parse(other.atk.text)).ToString(), 
                        (int.Parse(mascot.hp.text) + int.Parse(other.hp.text)).ToString());
                    if (level >= 2 && other.level >= 2) 
                    {
                        string thisEffect = mascot.GetEffect();
                        string otherEffect = other.GetEffect();
						if (!thisEffect.Equals("n") && otherEffect.Equals("n"))
						{
							AddEffect(thisEffect);
						}
						else if (thisEffect.Equals("n") && !otherEffect.Equals("n"))
						{
							AddEffect(otherEffect);
						}
						else if (!thisEffect.Equals("n") && !otherEffect.Equals("n")) 
						{
							int rand = Random.Range(0, 100);
							AddEffect(rand < 50 ? thisEffect : otherEffect);
						}
                    }
                    other.gameObject.SetActive(false);
				}    
			}    
		}
    }

    private void AddEffect(string effect) 
    {
		if (int.Parse(effect) >= 0)
		{
			Item item = gameManager.itemList[int.Parse(effect)];
			mascot.AddEffect(item.effect, item.id);
		}
		else
		{
			switch (int.Parse(effect))
			{
				case -1:
					mascot.AddEffect(gameManager.itemtokenList[0], -1);
					break;
			}
		}
	}
}
