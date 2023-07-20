using System.Collections.Generic;
using UnityEngine;

public class WatamateSkill : BaseSkill
{
	public override void Faint()
    {
		int pos = GetSlotPos();
		List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
		if (pos + 1 <= 4)
		{			
			if (tray[pos + 1].activeSelf)
			{
				gameManager.AddExp(tray[pos + 1].GetComponent<MascotDisplay>(), level);
				
				if (!mascot.opo && gameManager.IsBattle())
				{
					SaveInfo(tray[pos + 1].GetComponent<MascotDisplay>());
				}
			}
		}
	}
	private void SaveInfo(MascotDisplay target) 
	{
		string[] allSlot = gameManager.GetAllSlot();
		string info = allSlot[target.startSlot];
		string[] infos = info.Split('_');
		infos[3] = target.level.ToString();
		int expLvl = 0;
		switch (level)
		{
			case 1:
				expLvl = target.levelPart1.activeSelf ? 1 : 0;
				break;
			case 2:
				foreach (GameObject exp in target.levelPart2)
				{
					if (!exp.activeSelf)
					{
						break;
					}
					expLvl++;
				}
				break;
		}
		infos[4] = expLvl.ToString();
		string result = infos[0];
		for (int i = 1; i < 6; i++)
		{
			result += "_" + infos[i];
		}
		allSlot[target.startSlot] = result;
		gameManager.CreateNewInfo();
	}
}
