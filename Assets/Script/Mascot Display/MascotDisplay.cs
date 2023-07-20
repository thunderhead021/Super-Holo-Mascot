using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using Random = UnityEngine.Random;
using System.Linq;

public partial class MascotDisplay 
{
	public Mascot mascot = null;
	public Image art;
	public TextMeshProUGUI atk;
	public GameObject atkGO;
	public TextMeshProUGUI hp;
	public GameObject hpGO;
	public bool InShop = false;
	public GameManager gameManager;
	public List<GameObject> tray;
	private List<GameObject> curTray = new List<GameObject>();
	private bool drag = false;
	private Vector3 curPos;
	private GameObject targetSlot;
	public GameObject levelIndicator;
	[HideInInspector]
	public int level = 1;
	public List<GameObject> levelNum;
	public GameObject levelPart1;
	public List<GameObject> levelPart2;
	public Image BG;
	public List<Sprite> bg;
	public GameObject effect;
	public Animator animator = null;
	[HideInInspector]
	public bool death = false;
	[HideInInspector]
	public bool effectActivated = false;
	[HideInInspector]
	public bool itemEffectActivated = false;
	public int startSlot;
	private string[] mascotInfo = new string[6];  //mascotId_atk_hp_lvl_exp_effectId
	public bool opo = false;
	[HideInInspector]
	public int timeUseSkill;
	[HideInInspector]
	public int buyCost;
	[HideInInspector]
	public int sellCost = 1;
	[HideInInspector]
	public string BandGStatHelper = "";
	public BattleSlot parent;
	// Start is called before the first frame update
	void Awake()
	{
		CreateMascot(opo);
	}

	public void CreateMascot(bool fromOpo, bool fromSummon = false)
	{
		if (mascot != null)
		{
			art.sprite = fromOpo ? mascot.opoArt : mascot.playerArt;
			art.enabled = true;
			mascotInfo[0] = mascot.id.ToString();
			timeUseSkill = mascot.timeUseSkill;
			buyCost = mascot.cost;
			ChangeStats((mascot.atk + (fromSummon ? 0 : (2 * gameManager.GetshopPermaBuff()))).ToString(), 
				(mascot.hp + (fromSummon ? 0 : (1 * gameManager.GetshopPermaBuff()))).ToString());
			atkGO.SetActive(true);
			hpGO.SetActive(true);
			levelIndicator.SetActive(InShop ? false : true);
			SetLvl(1, 0);
			mascotInfo[5] = "n";
			effect.SetActive(false);
			death = false;
			effectActivated = false;
			opo = fromOpo;
			if (parent != null) 
			{
				parent.Alive();
			}
		}
	}

	public void ResetMascot() 
	{
		gameObject.SetActive(false);
		SetLvl(1, 0);
		mascotInfo[5] = "n";
		effect.SetActive(false);
	}

	public void GenerateMascot(string info, bool fromOpo, int startSlot) 
	{
		if (info.Equals(""))
		{
			gameObject.SetActive(false);
			if (parent != null)
			{
				parent.Alive();
			}
			return;
		}		
		string[] infos = info.Split('_');
		if ((infos.Length <= 1)) 
		{
			gameObject.SetActive(false);
			return;
		}
		if (int.Parse(infos[0]) >= 0)
			mascot = gameManager.mascotList[int.Parse(infos[0])];
		else 
		{
			switch (int.Parse(infos[0])) 
			{
				case -1:
					mascot = gameManager.tokenList[0];
					break;
				case -5:
					mascot = gameManager.tokenList[1];
					break;
				case -6:
					mascot = gameManager.tokenList[2];
					break;
				case -7:
					mascot = gameManager.tokenList[3];
					break;
				case -8:
					mascot = gameManager.tokenList[4];
					break;
				case -9:
					mascot = gameManager.tokenList[5];
					break;
				case -10:
					mascot = gameManager.tokenList[6];
					break;
				case -11:
					mascot = gameManager.tokenList[7];
					break;
				case -12:
					mascot = gameManager.tokenList[8];
					break;
				case -13:
					mascot = gameManager.tokenList[9];
					break;
				case -14:
					mascot = gameManager.tokenList[10];
					break;
			}
		}
		this.startSlot = startSlot;
		CreateMascot(fromOpo, true);
		ChangeStats(infos[1], infos[2]);
		level = int.Parse(infos[3]);
		int exp = int.Parse(infos[4]);
		SetLvl(level, exp);
		if (!infos[5].Equals("n") && int.TryParse(infos[5], out _))
		{
			if (int.Parse(infos[5]) >= 0)
			{
				Item item = gameManager.itemList[int.Parse(infos[5])];
				AddEffect(item.effect, item.id);
			}
			else 
			{
				switch (int.Parse(infos[5])) 
				{
					case -1:
						AddEffect(gameManager.itemtokenList[0], -1);
						break;
					case -2:						
						AddEffect(gameManager.itemtokenList[1], -2);
						break;
				}
			}
		}
	}

	public void SetLvlNumber(int num) 
	{
		foreach (GameObject tmp in levelNum) 
		{
			tmp.SetActive(false);
		}
		levelNum[num - 1 <= 0 ? 0 : num - 1].SetActive(true);
		BG.sprite = bg[num - 1 <= 0 ? 0 : num - 1];
	}

	public void SetLvl(int lvl, int exp) 
	{
		mascotInfo[3] = lvl.ToString();
		mascotInfo[4] = exp.ToString();
		ResetLvl();
		if (lvl == 1) 
		{
			levelPart1.SetActive(exp == 0 ? false : true);
		}
		else if (lvl == 2) 
		{
			for (int i = 0; i < exp; i++)
			{
				levelPart2[i].SetActive(true);
			}
		}
		SetLvlNumber(lvl);
	}

	public void ResetLvl() 
	{
		levelPart1.SetActive(false);
		foreach (GameObject part in levelPart2)
		{
			part.SetActive(false);
		}
	}

	public string GetMascotInfo() 
	{
		string result = mascotInfo[0];
		for (int i = 1; i < 6; i++) 
		{
			result += "_" + mascotInfo[i];
		}
		return result;
	}

	public int GetAtk() 
	{
		return int.Parse(mascotInfo[1]);
	}

	public int GetHp()
	{
		return int.Parse(mascotInfo[2]);
	}

	public string GetEffect() 
	{
		return mascotInfo[5];
	}

	public void DeepCopy(MascotDisplay other, bool fromOpo) 
	{
		mascot = other.mascot;
		CreateMascot(fromOpo, true);
		ChangeStats(other.atk.text, other.hp.text);
		effect.SetActive(other.effect.activeSelf);
		if (effect.activeSelf)
		{
			AddEffect(other.effect.GetComponent<Image>().material, int.Parse(other.mascotInfo[5]));
		}
		level = other.level;
		SetLvl(int.Parse(other.mascotInfo[3]), int.Parse(other.mascotInfo[4]));
		startSlot = other.startSlot;
		timeUseSkill = other.timeUseSkill;
		effectActivated = other.effectActivated;
	}

	public void ChangeStats(string atk, string hp) 
	{
		this.atk.text = atk;
		this.hp.text = hp;
		mascotInfo[1] = atk;
		mascotInfo[2] = hp;
	}

	public void AddEffect(Material material, int itemId, MascotDisplay fromWho = null) 
	{
		StartCoroutine(AddEffectHelper(material, itemId, fromWho));
	}

	IEnumerator AddEffectHelper(Material material, int itemId, MascotDisplay fromWho) 
	{
		if (fromWho != null)
		{
			GameObject dis = getDistance(startSlot - fromWho.startSlot);
			if (dis != null)
			{
				dis.GetComponent<distanceAniCall>().SetUpData(true);
				dis.SetActive(true);
				while (!dis.GetComponent<distanceAniCall>().aniFinished)
					yield return new WaitForSeconds(0.1f);
			}
		}
		effect.SetActive(true);
		effect.GetComponent<Image>().material = material;
		mascotInfo[5] = itemId.ToString();
	}

	public void RemoveEffect() 
	{
		effect.SetActive(false);
		effect.GetComponent<Image>().material = null;
		mascotInfo[5] = "n";
	}

	public void StatsBuff(int atkAdd, int hpAdd, MascotDisplay fromWho = null)
	{
		int atkValue = int.Parse(atk.text);
		atkValue += atkAdd;

		int hpValue = int.Parse(hp.text);
		hpValue += hpAdd;
		if (hpValue <= 0)
			StartCoroutine(DeathInShopPhase());
		else 
		{
			StartCoroutine(Buff(atkValue, hpValue, fromWho));
		}
	}

	IEnumerator DeathInShopPhase() 
	{	
		Faint();
		yield return new WaitForSeconds(1.5f);
		gameObject.SetActive(false);
		DisableDes();
	}


	public void ReciveDmg(int dmg, bool opoDeath, MascotDisplay fromWho, bool fromSkill = false) 
	{
		StartCoroutine(ReciveDmgHelper(dmg, opoDeath, fromWho, fromSkill));
	}

	IEnumerator ReciveDmgHelper(int dmg, bool opoDeath, MascotDisplay fromWho, bool fromSkill)
	{
		if (!opoDeath)
		{
			if (fromSkill) 
			{
				GameObject dis = getDistance(startSlot - fromWho.startSlot);
				if (dis != null) 
				{
					dis.GetComponent<distanceAniCall>().SetUpData(true);
					dis.SetActive(true);
					while (!dis.GetComponent<distanceAniCall>().aniFinished)
						yield return new WaitForSeconds(0.1f);
				}
			}
			if (effect.activeSelf && mascotInfo[5].Equals("-2")) 
			{
				effect.SetActive(false);
				effect.GetComponent<Image>().material = null;
				mascotInfo[5] = "n";
				if (gameManager.IsBattle() && !opo) 
				{
					gameManager.GetAllSlot()[startSlot] = GetMascotInfo();
					gameManager.CreateNewInfo();
				}
				yield break;
			}
			yield return new WaitForSeconds(0.7f);
			int hpValue = int.Parse(hp.text);
			int damage = dmg;
			if (fromWho.GetEffect().Equals("18")) 
			{
				damage = Random.Range(0, 100) > 50 ? (damage * 2) : damage;
			}
			int newHp = hpValue - damage + ReceiveDmg(damage);
			if (newHp <= 0 && gameManager.ProtectAlly(this))
			{
				gameManager.ShieldAlly(damage - ReceiveDmg(damage), fromWho, this);
			}
			else 
			{
				ChangeStats(atk.text, newHp.ToString());
				if (newHp <= 0)
				{
					death = true;
					if (fromWho.IsOverided("Knockout"))
					{
						fromWho.Knockout(Math.Abs(newHp));
					}
				}
			}	
		}
	}
}
