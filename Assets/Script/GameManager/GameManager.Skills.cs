using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
	[HideInInspector]
	public bool effectOn = false;
	private bool effectOn1 = false;

	IEnumerator AllySummoned(MascotDisplay ally) 
	{
		effectOn = true;
		for (int i = 0; i < 5; i++) 
		{
			MascotDisplay mascotDisplay1 = playerTray[i].GetComponent<MascotDisplay>();
			
			if (playerTray[i].activeSelf && mascotDisplay1.IsOverided("AllySummoned"))
			{
				mascotDisplay1.AllySummoned(ally);
				yield return new WaitForSeconds(0.8f);
			}
			if (battleMode) 
			{
				MascotDisplay mascotDisplay2 = opoTray[i].GetComponent<MascotDisplay>();
				if (opoTray[i].activeSelf && mascotDisplay2.IsOverided("AllySummoned"))
				{
					mascotDisplay2.AllySummoned(ally);
					yield return new WaitForSeconds(0.8f);
				}
			}	
		}
		effectOn = false;
	}

	IEnumerator BeforeAtk(MascotDisplay player, MascotDisplay opo) 
	{
		effectOn = true;
		if (player.IsOverided("BeforeAtk") && !player.death)
		{
			player.BeforeAtk();
			yield return new WaitForSeconds(1);
			yield return StartCoroutine(CheckForDeath());
		}
		if (opo.IsOverided("BeforeAtk") && !opo.death)
		{
			opo.BeforeAtk();
			yield return new WaitForSeconds(1);
			yield return StartCoroutine(CheckForDeath());
		}
		effectOn = false;
	}

	IEnumerator EndOfBatlle() 
    {
		effectOn = true;
		foreach (GameObject slot in playerTray) 
        {
            if (slot.activeSelf && slot.GetComponent<MascotDisplay>().IsOverided("EndOfBattle")) 
            {
                slot.GetComponent<MascotDisplay>().EndOfBattle();
				yield return new WaitForSeconds(1.2f);
			}
        }
		effectOn = false;
	}

	IEnumerator Death(MascotDisplay mascot, bool fromOpo)
	{

		effectOn = true;
		float timeWait = 2.5f;
		if (mascot.death)
		{			
			if (mascot.IsOverided("Faint"))
			{
				mascot.Faint();
				timeWait -= 1.5f;
				yield return new WaitForSeconds(1.5f);
			}

			if (mascot.IsOverided("FaintSummon") && !mascot.effectActivated && !mascot.itemEffectActivated)
			{
				mascot.deathSummonAnimation(fromOpo);
				yield return new WaitForSeconds(1.3f);
				mascot.FaintSummon();
				yield return new WaitForSeconds(0.2f);
			}
			else 
			{
				mascot.deathAnimation(fromOpo);
			}
			yield return StartCoroutine(AnyFaint(mascot));
		}
		else
		{
			mascot.winAnimation(fromOpo);
			if (mascot.IsOverided("DealDmg"))
			{
				timeWait -= 1.5f;
				yield return new WaitForSeconds(1.5f);
				mascot.DealDmg();
			}
		}
		yield return new WaitForSeconds(timeWait);
		effectOn = false;
	}

	public int StatBuffTime() 
	{
		int result = 0;
		foreach (GameObject slot in playerTray)
		{
			if (slot.activeSelf)
			{
				int tmp = slot.GetComponent<MascotDisplay>().StatBuffTime();
				if(tmp >= result)
					result = tmp;
			}
		}
		return result;
	}

	IEnumerator StartOfBattle()
	{
		effectOn = true;
		for (int i = 0; i < 5; i++) 
		{
			MascotDisplay mascotDisplay1 = playerTray[i].GetComponent<MascotDisplay>();
			MascotDisplay mascotDisplay2 = opoTray[i].GetComponent<MascotDisplay>();
			if (playerTray[i].activeSelf && mascotDisplay1.IsOverided("StartOfBattle"))
			{
				mascotDisplay1.StartOfBattle();
				yield return StartCoroutine(CheckForDeath());
			}
			if (opoTray[i].activeSelf && mascotDisplay2.IsOverided("StartOfBattle"))
			{
				mascotDisplay2.StartOfBattle();
				yield return StartCoroutine(CheckForDeath());
			}
		}
		effectOn = false;
	}

	IEnumerator CheckForDeath() 
	{
		for (int i = 0; i < 5; i++) 
		{
			if (playerTray[i].activeSelf)
			{
				yield return StartCoroutine(DeathB4AtkHelper(playerTray[i].GetComponent<MascotDisplay>(), false));
			}
			if (opoTray[i].activeSelf)
			{
				yield return StartCoroutine(DeathB4AtkHelper(opoTray[i].GetComponent<MascotDisplay>(), true));
			}
		}
	}

	public void StartOfShop() 
	{
		StartCoroutine(StartOfShopHelper());
	}

	IEnumerator StartOfShopHelper()
	{
		effectOn = true;
		foreach (GameObject slot in playerTray)
		{
			MascotDisplay mascotDisplay = slot.GetComponent<MascotDisplay>();
			if (slot.activeSelf && mascotDisplay.IsOverided("StartOfShop"))
			{
				mascotDisplay.StartOfShop();
				yield return new WaitForSeconds(0.5f);
			}

		}
		effectOn = false;
	}

	IEnumerator AllyLvlUp(MascotDisplay mascot)
	{
		yield return new WaitForSeconds(1);
		effectOn = true;
		if (mascot.IsOverided("LvlUp")) 
		{
			mascot.LvlUp();
			yield return new WaitForSeconds(0.5f);
		}
		List<GameObject> tray = mascot.opo ? opoTray : playerTray;
		for (int i = 0; i < 5; i++) 
		{
			MascotDisplay mascotDisplay = tray[i].GetComponent<MascotDisplay>();
			if (tray[i].activeSelf && mascotDisplay.IsOverided("AllyLvlUp"))
			{
				mascotDisplay.AllyLvlUp(mascot);
				yield return new WaitForSeconds(0.5f);
			}
		}
		effectOn = false;
	}

	public void DeathB4Atk(MascotDisplay mascot, bool fromOpo) 
	{
		StartCoroutine(DeathB4AtkHelper(mascot, fromOpo));
	}

	IEnumerator DeathB4AtkHelper(MascotDisplay mascot, bool fromOpo)
	{
		yield return new WaitForSeconds(0.7f);
		if (mascot.death || int.Parse(mascot.hp.text) <= 0)
		{			
			if (mascot.IsOverided("FaintSummon") && !mascot.effectActivated)
			{
				mascot.deathSummonAnimation(fromOpo);
				yield return new WaitForSeconds(1.6f);
				mascot.FaintSummon();
				yield return new WaitForSeconds(0.2f);
				yield break;
			}
			if (mascot.IsOverided("Faint"))
			{
				mascot.Faint();
				yield return new WaitForSeconds(1.5f);

			}
			mascot.deathAnimation(fromOpo);
			yield return StartCoroutine(AnyFaint(mascot));
		}
	}

	public void DeathInShop(MascotDisplay mascot) 
	{
		StartCoroutine(DeathInShopHelper(mascot));
	}

	IEnumerator DeathInShopHelper(MascotDisplay mascot)
	{
		if (mascot.death)
		{
			mascot.FaintInShop();
			if (mascot.IsOverided("FaintSummon") && !mascot.effectActivated)
			{	
				yield return new WaitForSeconds(2);
				mascot.FaintSummon();
				yield return new WaitForSeconds(0.5f);
				yield break;
			}
			if (mascot.IsOverided("Faint"))
			{
				yield return new WaitForSeconds(2);
				mascot.Faint();
			}
			yield return StartCoroutine(AnyFaint(mascot));
		}
	}

	public void DmgToAll(int dmg, MascotDisplay fromWho) 
	{
		StartCoroutine(DmgToAllHelper(dmg, fromWho));
	}

	IEnumerator DmgToAllHelper(int dmg, MascotDisplay fromWho) 
	{
		effectOn1 = true;
		for (int i = 0; i < 5; i++) 
		{
			if (playerTray[i].activeSelf && playerTray[i] != fromWho.gameObject)
			{
				playerTray[i].GetComponent<MascotDisplay>().ReciveDmg(dmg, false, fromWho, true);
				yield return new WaitForSeconds(1);
				if (battleMode)
					yield return StartCoroutine(DeathB4AtkHelper(playerTray[i].GetComponent<MascotDisplay>(), false));
				else
					yield return StartCoroutine(DeathInShopHelper(playerTray[i].GetComponent<MascotDisplay>()));
			}
			if (battleMode && opoTray[i].activeSelf && opoTray[i] != fromWho.gameObject)
			{
				opoTray[i].GetComponent<MascotDisplay>().ReciveDmg(dmg, false, fromWho, true);
				yield return new WaitForSeconds(1);
				yield return StartCoroutine(DeathB4AtkHelper(opoTray[i].GetComponent<MascotDisplay>(), true));
			}
		}
		effectOn1 = false;
	}

	public void Pull(int fromSlot, int toSlot,MascotDisplay mascot) 
	{
		StartCoroutine(PullHelper(fromSlot, toSlot, mascot));
	}

	public void Push(MascotDisplay mascot, int time) 
	{
		StartCoroutine(PushHelper(mascot, time));
	}

	IEnumerator PushHelper(MascotDisplay mascot, int time) 
	{
		int realTime = GetRealTime(mascot, time);
		if (mascot.opo)
		{
			if(!playerTray[1].activeSelf)
				yield break;				
			for (int i = 0; i < realTime; i++)
			{
				SwitchSlot(playerTray[i].GetComponent<MascotDisplay>(), playerTray[i + 1].GetComponent<MascotDisplay>(), false);
				yield return new WaitForSeconds(0.5f);
			}
		}
		else 
		{
			if (!opoTray[1].activeSelf)
				yield break;		
			for (int i = 0; i < realTime; i++)
			{
				SwitchSlot(opoTray[i].GetComponent<MascotDisplay>(), opoTray[i + 1].GetComponent<MascotDisplay>(), true);
				yield return new WaitForSeconds(0.5f);
			}
		}
			
	}

	private int GetRealTime(MascotDisplay mascot, int time) 
	{
		int result = -1;
		for (int i = 0; i <= time; i++) 
		{
			if (mascot.opo ? playerTray[i].activeSelf : opoTray[i].activeSelf) 
			{
				result++;
			}
		}
		return result;
	}

	IEnumerator PullHelper(int fromSlot, int toSlot, MascotDisplay mascot)
	{
		if (mascot.opo)
		{
			if (playerTray[fromSlot].activeSelf && playerTray[toSlot].activeSelf) 
			{
				playerTray[fromSlot].GetComponent<MascotDisplay>().PlayerPull(fromSlot, toSlot);
				playerTray[toSlot].GetComponent<MascotDisplay>().OpoPull(toSlot, fromSlot);
				yield return new WaitForSeconds(1.8f);	
				playerTray[fromSlot].GetComponent<MascotDisplay>().PlayerPull(0, 0);
				playerTray[toSlot].GetComponent<MascotDisplay>().OpoPull(0, 0);
				SwitchSlot(playerTray[fromSlot].GetComponent<MascotDisplay>(), playerBattleSlot.GetComponent<MascotDisplay>(), false);
			}
		}
		else
		{
			if (opoTray[fromSlot].activeSelf && opoTray[toSlot].activeSelf) 
			{
				opoTray[fromSlot].GetComponent<MascotDisplay>().OpoPull(fromSlot, toSlot);
				opoTray[toSlot].GetComponent<MascotDisplay>().PlayerPull(toSlot, fromSlot);
				yield return new WaitForSeconds(1.8f);	
				opoTray[fromSlot].GetComponent<MascotDisplay>().OpoPull(0, 0);
				opoTray[toSlot].GetComponent<MascotDisplay>().PlayerPull(0, 0);
				SwitchSlot(opoTray[fromSlot].GetComponent<MascotDisplay>(), opoBattleSlot.GetComponent<MascotDisplay>(), true);
			}	
		}
	}

	public bool ProtectAlly(MascotDisplay mascot) 
	{
		foreach (GameObject slot in mascot.opo ? opoTray : playerTray)
		{
			MascotDisplay slotMascot = slot.GetComponent<MascotDisplay>();
			if (slot.activeSelf && slotMascot != mascot && slotMascot.IsOverided("ProtectAlly") && slotMascot.ProtectAlly(mascot))
			{
				return true;
			}
		}
		return false;
	}

	public void ShieldAlly(int dmg, MascotDisplay fromWho, MascotDisplay whoHurt) 
	{
		StartCoroutine(ShieldAllyHelper(dmg, fromWho, whoHurt));
	}

	IEnumerator ShieldAllyHelper(int dmg, MascotDisplay fromWho, MascotDisplay whoHurt) 
	{
		effectOn = true;
		foreach (GameObject slot in whoHurt.opo ? opoTray : playerTray)
		{
			MascotDisplay slotMascot = slot.GetComponent<MascotDisplay>();
			if (slot.activeSelf && slotMascot != whoHurt && slotMascot.IsOverided("ProtectAlly") && slotMascot.ProtectAlly(whoHurt) && slotMascot.IsOverided("ShieldAlly"))
			{
				slotMascot.ShieldAlly(dmg, fromWho);
				yield return new WaitForSeconds(1.5f);
				if (slotMascot.death)
				{
					yield return StartCoroutine(DeathB4AtkHelper(slotMascot, slotMascot.opo));
				}
				effectOn = false;
				yield break;
			}
		}
		effectOn = false;
	}

	IEnumerator Buy(MascotDisplay mascot) 
	{
		if (mascot.IsOverided("Buy"))
		{
			mascot.Buy();
			yield return new WaitForSeconds(0.7f);
		}
	}

	public void AttackCall(MascotDisplay player, MascotDisplay opo, int playerDmgRecive, int opoDmgRecive)
	{
		StartCoroutine(SplashAtk(player, opo, playerDmgRecive, opoDmgRecive));
	}

	IEnumerator SplashAtk(MascotDisplay player, MascotDisplay opo, int playerDmgRecive, int opoDmgRecive) 
	{
		effectOn1 = true;
		yield return StartCoroutine(Attack(player, opo, playerDmgRecive, opoDmgRecive));
		yield return StartCoroutine(DeathB4AtkHelper(opo, opo.opo));
		effectOn1 = false;
	}

	IEnumerator Attack(MascotDisplay player, MascotDisplay opo, int playerDmgRecive, int opoDmgRecive)
	{
		yield return new WaitForSeconds(0.5f);
		player.ReciveDmg(playerDmgRecive, opo.death, opo);
		opo.ReciveDmg(opoDmgRecive, player.death, player);
		yield return new WaitForSeconds(2);
	}

	IEnumerator AnyFaint(MascotDisplay mascot) 
	{
		effectOn = true;
		yield return new WaitForSeconds(1.8f);
		for (int i = 0; i < 5; i++)
		{
			if (playerTray[i].activeSelf && !playerTray[i].GetComponent<MascotDisplay>().death 
				&& playerTray[i].GetComponent<MascotDisplay>().IsOverided("AnyFaint"))
			{
				playerTray[i].GetComponent<MascotDisplay>().AnyFaint(mascot);
			}
			if (battleMode && opoTray[i].activeSelf && !opoTray[i].GetComponent<MascotDisplay>().death
				&& opoTray[i].GetComponent<MascotDisplay>().IsOverided("AnyFaint"))
			{
				opoTray[i].GetComponent<MascotDisplay>().AnyFaint(mascot);
			}
			yield return new WaitForSeconds(0.5f);
		}
		effectOn = false;
	}
}
