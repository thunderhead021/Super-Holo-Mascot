using UnityEngine;

public class JobzSkill : BaseSkill
{
	public Mascot RoseKnight1;
	public Mascot RoseKnight2;
	public Mascot RoseKnight3;

	public override void AnyFaint(MascotDisplay other)
	{
		if (mascot.opo == other.opo && mascot.timeUseSkill > 0) 
		{
			foreach (GameObject slot in mascot.opo ? gameManager.opoTray : gameManager.playerTray)
			{
				if (!slot.activeSelf || slot.GetComponent<MascotDisplay>().death)
				{
					MascotDisplay token = slot.GetComponent<MascotDisplay>();
					token.mascot = level == 1 ? RoseKnight1 : (level == 2 ? RoseKnight2 : RoseKnight3);
					token.CreateMascot(other.opo, true);
					token.SetLvl(level, 0);
					token.gameObject.SetActive(true);
					mascot.timeUseSkill -= 1;
					gameManager.AllySummonedActivate(token);
					break;
				}
			}
			
		}
	}
}
