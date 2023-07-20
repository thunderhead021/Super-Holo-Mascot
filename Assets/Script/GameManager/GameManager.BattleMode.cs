using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager
{
	[HideInInspector]
	public GameObject playerBattleSlot;
	[HideInInspector]
	public GameObject opoBattleSlot;
	private static int whoWon; //-1: opo 1: player 0: tie


	private void Battle() 
	{
		StartCoroutine(BattlePhase());
	}

	private bool MarchComplete() 
	{
		for (int i = 0; i < 4; i++) 
		{
			if ((playerTray[i].activeSelf && !playerTray[i].GetComponent<MascotDisplay>().aniFinished) || 
				(opoTray[i].activeSelf && !opoTray[i].GetComponent<MascotDisplay>().aniFinished))
				return false;
		}
		return true;
	}

	private void RevertAllAni() 
	{
		for (int i = 0; i < 4; i++)
		{
			if (playerTray[i].activeSelf)
				playerTray[i].GetComponent<MascotDisplay>().aniFinished = false;
			if (opoTray[i].activeSelf)
				opoTray[i].GetComponent<MascotDisplay>().aniFinished = false;
		}
	}

	IEnumerator BattlePhase() 
	{
		while(!MarchComplete())
			yield return new WaitForSeconds(0.2f);
		RevertAllAni();
		//Before Battle
		//active all start of battle passive from both side
		yield return StartCoroutine(StartOfBattle());
		while (effectOn)
			yield return new WaitForSeconds(0.5f);
		yield return MoveUp();
		//begin battle loop
		while (!BattleEnd())
		{
			//active before battle passive from both battle slots
			//active start battle animation from both battle slots
			MascotDisplay player = GetAtker(false);
			playerBattleSlot = player.gameObject;
			MascotDisplay opo = GetAtker(true);
			opoBattleSlot = opo.gameObject;
			yield return StartCoroutine(BeforeAtk(player, opo));
			yield return MoveUp();
			if (!player.death && !opo.death) 
			{
				player.atkAnimation(false);
				opo.atkAnimation(true);
				while(!player.aniFinished && !opo.aniFinished)
					yield return new WaitForSeconds(0.1f);
				player.aniFinished = false;
				opo.aniFinished = false;
				//player.Attack();
				//During Battle
				//find the winner => if no winner go again
				yield return StartCoroutine(Attack(player, opo, int.Parse(opo.atk.text), int.Parse(player.atk.text)));
				while (effectOn)
					yield return new WaitForSeconds(0.1f);
				//After Battle
				//play the correct animation
				yield return StartCoroutine(Death(player, false));
				yield return StartCoroutine(Death(opo, true));
				while (effectOn)
					yield return new WaitForSeconds(0.1f);
				while (effectOn1)
					yield return new WaitForSeconds(0.1f);
			}
			yield return MoveUp();
			//active skills from both side
			//move slots		
			//go again
		}
		Time.timeScale = 3;
		if (whoWon == -1)
		{
			heart -= HeartLoseNum();
			transtion.GetComponent<Animator>().SetInteger("lose", HeartLoseNum());
			GetComponent<AudioSource>().clip = loseaudio;
			GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(animationDic["Lose 3"] * 3);
		}
		else if (whoWon == 1) 
		{
			win += 1;
			yield return StartCoroutine(EndOfBatlle());
			transtion.GetComponent<Animator>().SetInteger("win", win);
			GetComponent<AudioSource>().clip = winaudio;
			GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(animationDic["Win r"] * 3);
		}
		else 
		{
			transtion.GetComponent<Animator>().SetTrigger("draw");
			GetComponent<AudioSource>().clip = drawaudio;
			GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(animationDic["Draw"] * 3);
		}
		battleMode = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	private MascotDisplay GetAtker(bool fromOpo) 
	{
		foreach (GameObject slot in fromOpo ? opoTray : playerTray) 
		{
			if (slot.activeSelf)
				return slot.GetComponent<MascotDisplay>();
		}
		return null;
	}

	IEnumerator MoveUp() 
	{
		yield return new WaitForSeconds(1);
		if (BattleEnd())
			yield break;
		yield return StartCoroutine(MoveUpHelper());
	}

	IEnumerator MoveUpHelper() 
	{
		for (int i = 0; i < 5; i++) 
		{
			GameObject player_slot = playerTray[i];
			GameObject opo_slot = opoTray[i]; 
			if (!player_slot.activeSelf || !opo_slot.activeSelf)
			{
				if(!player_slot.activeSelf)
					player_slot.GetComponent<MascotDisplay>().parent.Death();
				if (!opo_slot.activeSelf)
					opo_slot.GetComponent<MascotDisplay>().parent.Death();
				yield return new WaitForSeconds(0.6f);
			}
		}
	}

	private int HeartLoseNum() 
	{
		if (round <= 2)
			return 1;
		if (round <= 4)
			return 2;
		return 3;
	}

	private bool BattleEnd() 
	{
		bool noPLayer = true;
		bool noOpo = true;
		foreach (GameObject slot in playerTray)
		{
			if ((slot.activeSelf))
			{
				noPLayer = false;
				break;
			}

		}
		foreach (GameObject slot in opoTray)
		{
			if ((slot.activeSelf))
			{
				noOpo = false;
				break;
			}

		}
		if ((noPLayer == true && noOpo == false))
		{
			whoWon = -1;
			return true;
		}
		else if (noPLayer == false && noOpo == true)
		{
			whoWon = 1;
			return true;
		}
		else if (noPLayer == true && noOpo == true) 
		{
			whoWon = 0;
			return true;
		}
		else	
			return false;
	}
}
