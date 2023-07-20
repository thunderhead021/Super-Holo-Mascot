using UnityEngine;

public class ROBELSkill : ItemTriggerBase
{
    public override void Multiple()
    {
		int max = GetNumOfSlot();
		int rand1 = Random.Range(0, max);
		int rand2 = Random.Range(0, max - 1);
		GameObject slot1 = null;
		for (int i = 0; i < 5; i++)
		{
			if (rand1 == 0 && gameManager.playerTray[i].activeSelf)
			{
				slot1 = gameManager.playerTray[i];
				item.Buff(gameManager.playerTray[i].GetComponent<MascotDisplay>(), 1, 1);
				break;
			}
			if (rand1 > 0)
				rand1--;
		}
		for (int i = 4; i >= 0; i--)
		{
			if (rand2 == 0 && gameManager.playerTray[i].activeSelf && gameManager.playerTray[i] != slot1)
			{
				item.Buff(gameManager.playerTray[i].GetComponent<MascotDisplay>(), 1, 1);
				break;
			}
			if (rand2 > 0)
				rand2--;
		}
	}

	private int GetNumOfSlot()
	{
		int result = 0;
		for (int i = 0; i < 5; i++)
		{
			if (gameManager.playerTray[i].activeSelf)
			{
				result++;
			}
		}
		return result;
	}
}
