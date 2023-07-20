using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MascotDisplay
{
	public GameObject buff;
	public Image LvlText;
	public List<GameObject> distance;
	[HideInInspector]
	public bool aniFinished = false;
	public AudioClip hit;
	public AudioClip fly;

	public void Death()
	{
		if (GetComponent<AudioSource>().isPlaying) return;
		GetComponent<AudioSource>().clip = fly;
		GetComponent<AudioSource>().Play();
	}

	public void Attack()
	{
		if (GetComponent<AudioSource>().isPlaying) return;
		GetComponent<AudioSource>().clip = hit;
		GetComponent<AudioSource>().Play();
	}

	public void AniFinish()
	{
		aniFinished = true;
	}

	public void moveFromShop()
	{
		animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		animator.SetTrigger("moving");
	}

	public void FaintInShop()
	{
		StartCoroutine(FaintInShopHelper());
	}

	IEnumerator FaintInShopHelper()
	{
		animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		animator.SetBool("faintInShop", true);
		yield return new WaitForSeconds(gameManager.animationDic["FaintInShop"]);
		animator.SetBool("faintInShop", false);
		animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
		gameObject.SetActive(false);
		gameObject.transform.localPosition = new Vector3(0, 0, 0);
		ResetAlpha();
	}

	private void ResetAlpha()
	{
		ResetAlphaHelper(art);
		ResetAlphaHelper(atkGO.GetComponent<Image>());
		atk.color = Color.white;
		ResetAlphaHelper(hpGO.GetComponent<Image>());
		hp.color = Color.white;
		ResetAlphaHelper(levelIndicator.GetComponent<Image>());
		ResetAlphaHelper(BG);
		ResetAlphaHelper(levelPart1.GetComponent<Image>());
		foreach (GameObject part in levelPart2)
		{
			ResetAlphaHelper(part.GetComponent<Image>());
		}
		foreach (GameObject part in levelNum)
		{
			ResetAlphaHelper(part.GetComponent<Image>());
		}
		ResetAlphaHelper(LvlText);
		ResetAlphaHelper(effect.GetComponent<Image>());
	}

	private void ResetAlphaHelper(Image image)
	{
		Color c = image.color;
		c.a = 1f;
		image.color = c;
	}

	public void moveToBattle(bool fromOpo)
	{
		animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		DisableDes();
		animator.SetTrigger(fromOpo ? "opo2Battle" : "player2Battle");
		animator.Play(0, -1, 0);
	}

	IEnumerator Buff(int atkValue, int hpValue, MascotDisplay fromWho)
	{
		gameManager.effectOn = true;
		if (fromWho != null)
		{
			GameObject dis = getDistance(startSlot - fromWho.startSlot);
			if (dis != null)
			{
				dis.GetComponent<distanceAniCall>().SetUpData();
				dis.SetActive(true);
				while (!dis.GetComponent<distanceAniCall>().aniFinished)
					yield return new WaitForSeconds(0.1f);
			}
		}		
		DisableDes();
		buff.SetActive(true);
		yield return new WaitForSeconds(gameManager.IsBattle() ? gameManager.animationDic["Buff"] : 1.5f);
		ChangeStats(atkValue.ToString(), hpValue.ToString());
		buff.SetActive(false);
		gameManager.effectOn = false;
	}

	private GameObject getDistance(int thisDis)
	{
		foreach (GameObject num in distance)
		{
			if (num.name.Equals(thisDis.ToString()))
			{
				return num;
			}
		}
		return null;
	}



	//fix now
	public void atkAnimation(bool fromOpo)
	{
		StartCoroutine(animationCallWithDiffrent(fromOpo, "opoAtk", "playerAtk", false, 0.1f));
	}

	public void poyoyoAnimation(bool fromOpo)
	{
		StartCoroutine(animationCallWithDiffrent(fromOpo, "poyoyoOpo", "poyoyoPlayer", false, gameManager.animationDic["PoyoyoOpo"]));
	}

	public void deathAnimation(bool fromOpo)
	{
		if (gameObject.activeSelf)
			StartCoroutine(animationCallWithDiffrent(fromOpo, "opoLose", "playerLose", true, gameManager.animationDic["OpoLose"] + 0.7f * Time.timeScale));
	}

	public void deathSummonAnimation(bool fromOpo)
	{
		StartCoroutine(deathSummonHelper(fromOpo));

	}

	IEnumerator deathSummonHelper(bool fromOpo)
	{
		DisableDes();
		animator.SetTrigger(fromOpo ? "opoLose" : "playerLose");
		yield return new WaitForSeconds(gameManager.animationDic["OpoLose"] + 0.2f);
		animator.SetTrigger("reset");
		gameObject.transform.localPosition = new Vector3(0, -95f, 0);
		gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		gameObject.SetActive(false);
		parent.Death();
	}

	IEnumerator animationCallWithDiffrent(bool fromOpo, string opoTrigger, string playerTrigger, bool needReset, float time)
	{
		DisableDes();
		animator.SetTrigger(fromOpo ? opoTrigger : playerTrigger);
		animator.Play(0, -1, 0);
		yield return new WaitForSeconds(time /*+ (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.5f ? 1 : 0)*/);
		if (needReset)
		{
			gameObject.SetActive(false);
			gameObject.transform.localPosition = new Vector3(0, -95f, 0);
			gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}

	public void winAnimation(bool fromOpo)
	{
		StartCoroutine(animationCallWithDiffrent(fromOpo, "opoWin", "playerWin", false, gameManager.animationDic["PlayerWin"]));
	}

	public void OpoPull(int fromSlot, int toSlot)
	{
		animator.SetInteger("opoPullFromSlot", Mathf.Abs(fromSlot - toSlot));
	}

	public void PlayerPull(int fromSlot, int toSlot)
	{
		animator.SetInteger("playerPullFromSlot", Mathf.Abs(fromSlot - toSlot));
	}

}
