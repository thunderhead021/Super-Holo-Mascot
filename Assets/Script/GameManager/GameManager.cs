using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    public List<Mascot> mascotList;
    public List<Mascot> mascotTier1;
    public List<Mascot> mascotTier2;
    public List<Mascot> mascotTier3;
    public List<Mascot> mascotTier4;
    public List<Mascot> mascotTier5;

	public List<Item> itemList;
	public List<Item> itemTier1;
    public List<Item> itemTier2;
    public List<Item> itemTier3;
    public List<Item> itemTier4;
    public List<Item> itemTier5;

	public List<Mascot> tokenList;
	public List<Material> itemtokenList;
	public List<GameObject> playerTray;
    public List<GameObject> opoTray;
    public GameObject transtion;
    public Text roundNumber;
    public List<GameObject> victorySquad;
    public GameObject description;
    public GameObject descriptionCamvas;
    public static string player = "////";
    public static int shopPermaBuff = 0;
    private static bool battleMode = false;
	private static bool inGame = false;
	public static int round = 0;
    public GameObject[] allHearts;
    public GameObject[] allWins;
    private static int heart = 10;
    private static int win = 0;
    private static string[] allSlot;
	[HideInInspector]
    public int coin = 10;
    [HideInInspector]
    public int freeRefreshTimes = 0;
    public Shop shop;
    public Coin coinShow;
    public GameObject endGameBtns;
    public GameObject tier;
    public Animator canBattle;
    private static List<string> aiData = new List<string>();
	public List<AnimationClip> animations;
    [HideInInspector]
	public Dictionary<string, float> animationDic = new Dictionary<string, float>();
	public AudioClip buy;
	public AudioClip sell;
	public AudioClip refresh;
	public AudioClip winaudio;
	public AudioClip freezeaudio;
	public AudioClip loseaudio;
	public AudioClip drawaudio;
	public AudioClip tieraudio;
	public PlayMusic march;
	public PlayMusic bgm;
    public GameObject canvas1;
	public GameObject canvas2;
    public GameObject btnsOnCanvas2;
    public SettingMenu setting;
    public GameObject fastBtn;
    public GameObject slowBtn;
    public GameObject PauseScreen;
    public SendData Form;
	private static string playerName = "";
    private static bool firstOpen = true;
    [HideInInspector]
    public bool pause = false;
	[HideInInspector]
	public bool settingPause = false;
	public void BuyAudio() 
    {
		GetComponent<AudioSource>().clip = buy;
		GetComponent<AudioSource>().Play();
	}

	public void SellAudio()
	{
		GetComponent<AudioSource>().clip = sell;
		GetComponent<AudioSource>().Play();
	}

	public void FreezeAudio()
	{
		GetComponent<AudioSource>().clip = freezeaudio;
		GetComponent<AudioSource>().Play();
	}

	public void RefreshAudio()
	{
		GetComponent<AudioSource>().clip = refresh;
		GetComponent<AudioSource>().Play();
	}

	IEnumerator LerpFunction(bool getName)
	{
		float time = 0;
		btnsOnCanvas2.SetActive(getName ? true : false);
        Vector3 onScreen = getName ? canvas1.transform.localPosition : canvas2.transform.localPosition;
        Vector3 offScreen = getName ? new Vector3(canvas2.transform.localPosition.x, -canvas2.transform.localPosition.y, canvas2.transform.localPosition.z):
            new Vector3(canvas1.transform.localPosition.x, -canvas1.transform.localPosition.y, canvas1.transform.localPosition.z);
		while (time < 1.5f)
		{
			canvas1.transform.localPosition = Vector2.Lerp(canvas1.transform.localPosition, getName ? offScreen : onScreen, time / 1.5f);
			canvas2.transform.localPosition = Vector2.Lerp(canvas2.transform.localPosition, getName ? onScreen : offScreen, time / 1.5f);
			time += Time.deltaTime;
			yield return null;
		}
		canvas1.transform.localPosition = getName ? offScreen : onScreen;
		canvas2.transform.localPosition = getName ? onScreen : offScreen;
	}

	public void AddBuff() 
    {
        shopPermaBuff += 1;

	}

    public int GetshopPermaBuff() 
    {
        return shopPermaBuff;

	}

	public List<Mascot> GetCurMascotList() 
    {
        List<Mascot> list = mascotTier1.ToList();
        if (round >= 3) 
            list = list.Concat(mascotTier2).ToList();
        if(round >= 5)
			list = list.Concat(mascotTier3).ToList();
		if (round >= 7)
			list = list.Concat(mascotTier4).ToList();
		if (round >= 9)
			list = list.Concat(mascotTier5).ToList();
		return list;
    }

	public List<Item> GetCurItemList()
	{
		List<Item> list = itemTier1.ToList();
		if (round >= 3)
			list = list.Concat(itemTier2).ToList();
		if (round >= 5)
			list = list.Concat(itemTier3).ToList();
		if (round >= 7)
			list = list.Concat(itemTier4).ToList();
		if (round >= 9)
			list = list.Concat(itemTier5).ToList();
		return list;
	}

	public int GetNumOfSSMember(bool fromOpo) 
    {
        int result = 0;
        foreach (GameObject slot in fromOpo ? opoTray : playerTray) 
        {
            if (slot.activeSelf && slot.GetComponent<MascotDisplay>().GetEffect().Equals("7")) 
            {
                result++;
			}
        }
        return result;
    }

    public bool IsBattle() 
    {
        return battleMode;
    }

    public string[] GetAllSlot() 
    {
        return allSlot;
    }

    public void CreateNewInfo() 
    {
        string result = "";
        for(int i = 0; i < 4; i++) 
        {
            result += allSlot[i] + "/";
        }
        result += allSlot[4];
        player = result;
    }

	public string CreateNewInfo(string[] infos)
	{
		string result = infos[0];
		for (int i = 1; i < 6; i++)
		{
			result += "_" + infos[i];
		}
		return result;
	}

	IEnumerator checkInternetConnection(Action<bool> action)
	{
#pragma warning disable CS0618 // Type or member is obsolete
		WWW www = new WWW("http://google.com");
#pragma warning restore CS0618 // Type or member is obsolete
		yield return www;
		if (www.error != null)
		{
			action(false);
		}
		else
		{
			action(true);
		}
	}

    public void CheckInternet() 
    {
		StartCoroutine(checkInternetConnection((isConnected) => {
			setting.isConnected = isConnected;
		}));
	}

    private void Start()
	{
        CheckInternet();
        if (firstOpen)
        {
            string aidataPath = Path.Combine(Application.streamingAssetsPath, "AIdata.txt");
			FileStream file = new FileStream(aidataPath, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(file);
			aiData = new List<string>(sr.ReadToEnd().Split('\n').ToList());
			sr.Close();
			file.Close();	
			setting.ReturnSetting();
            firstOpen = false;
        }
        if (playerName.Equals("") && inGame)
        {
			player = "////";
			heart = 10;
			win = 0;
			round = 0;
			whoWon = 0;
			shopPermaBuff = 0;
			battleMode = false;
			shop.Restart();
			GenText(selectAdjective, adjective);
            GenText(selectFanName, fanName);
        }
        else
        {
            foreach (Text text in showPlayerName)
            {
                text.text = playerName;
            }
            if (showOpoName != null)
                GenAName();
        }
        foreach (AnimationClip clip in animations)
        {
            animationDic.Add(clip.name, clip.length * Time.timeScale);
        }
        Debug.Log(player);
        StartCoroutine(WaitToEnaleObject());
        if (playerTray.Count > 0)
        {
            allSlot = player.Split('/');
            for (int i = 0; i < 5; i++)
            {
                playerTray[i].GetComponent<MascotDisplay>().GenerateMascot(allSlot[i], false, i);
                if (battleMode && playerTray[i].activeSelf)
                    playerTray[i].GetComponent<MascotDisplay>().moveToBattle(false);
            }
        }
        if (opoTray.Count > 0)
        {
            string[] opoLineUp = GetAnOpoForRound(round);		
			for (int i = 0; i < 5; i++)
            {
				Debug.Log(opoLineUp[i]);
				opoTray[i].GetComponent<MascotDisplay>().GenerateMascot(opoLineUp[i], true, 0 - i - 1);
				if (battleMode && opoTray[i].activeSelf)
                    opoTray[i].GetComponent<MascotDisplay>().moveToBattle(true);
            }
        }
        if (battleMode)
        {
            Time.timeScale = 3;
            Battle();
        }
        else if (inGame)
        {
            Time.timeScale = 1.5f;
            if (win == 8)
            {
                StartCoroutine(Victory());
            }
            else if (heart <= 0)
            {
                StartCoroutine(Loser());
            }
            else
            {
                for (int i = 0; i < heart; i++)
                {
                    allHearts[i].SetActive(true);
                }
                for (int i = 0; i < win; i++)
                {
                    allWins[i].SetActive(true);
                }
                coin = 10;
                StartOfShop();
                coinShow.ChangeAmount(coin);
                round += 1;
                roundNumber.text = "Round " + round;
                StartCoroutine(Unlock());
            }
        }
    }

    public void AddCoin(int amount) 
    {
		coin += amount;
        if (coin < 0)
            coin = 0;
		coinShow.ChangeAmount(coin);
	}

    IEnumerator WaitToEnaleObject()
    {
        //if(inGame)
            yield return new WaitForSeconds(animationDic["Start"]);
        descriptionCamvas.SetActive(true);
    }

    IEnumerator Unlock() 
    {
		for (int i = 0; i < 5; i++)
		{
			tier.transform.GetChild(i).gameObject.SetActive(false);
		}
		tier.transform.GetChild(0).gameObject.SetActive(true);
		if (round >= 3)
			tier.transform.GetChild(1).gameObject.SetActive(true);
		if (round >= 5)
			tier.transform.GetChild(2).gameObject.SetActive(true);
		if (round >= 7)
			tier.transform.GetChild(3).gameObject.SetActive(true);
		if (round >= 9)
			tier.transform.GetChild(4).gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
		effectOn = true;
        if (round == 1 || round == 3 || round == 5 || round == 7 || round == 9) 
        {
			GetComponent<AudioSource>().clip = tieraudio;
			GetComponent<AudioSource>().Play();
			transtion.GetComponent<Animator>().SetInteger("round", round);
			yield return new WaitForSeconds(animationDic["Tier 5"]);
		}       
		effectOn = false;
	}

	IEnumerator Victory() 
    {
        yield return new WaitForSeconds(animationDic["Start"]/2);
        effectOn = true;
        for(int i = 0; i < 5; i++) 
        {
            if (playerTray[i].activeSelf)
                victorySquad[i].GetComponent<Image>().sprite = playerTray[i].GetComponent<Image>().sprite;
            else
                victorySquad[i].SetActive(false);

		}
        transtion.GetComponent<Animator>().SetTrigger("victory");
		yield return new WaitForSeconds(animationDic["Victory"] * 2);
		endGameBtns.SetActive(true);
	}

    IEnumerator Loser()
    {
        yield return new WaitForSeconds(animationDic["Start"]/2);
		effectOn = true;
		transtion.GetComponent<Animator>().SetTrigger("loser");
		yield return new WaitForSeconds(animationDic["Loser"] * 2);
		endGameBtns.SetActive(true);
	}

    private void SendRoundData() 
    {
		string output = playerName + "\n";
		foreach (string tmp in data)
		{
			output += tmp + "\n";

		}
		Debug.Log(output);
		Form.GetComponent<RectTransform>().localPosition = new Vector3(-2458, Form.GetComponent<RectTransform>().localPosition.y, Form.GetComponent<RectTransform>().localPosition.z);
		Form.gameObject.SetActive(true);
		Form.SendAIData(output);
	}

    public void OpenForm() 
    {
		string output = playerName + "\n";
		foreach (string tmp in data)
		{
			output += tmp + "\n";

		}
		Form.GetComponent<RectTransform>().localPosition = new Vector3(0, Form.GetComponent<RectTransform>().localPosition.y, Form.GetComponent<RectTransform>().localPosition.z);
        Form.gameObject.SetActive(true);
		Form.SendAIData(output, false);
	}

    public void NewGame() 
    {
        SendRoundData();
		player = "////";
        playerName = "";
		heart = 10;
		win = 0;
		round = 0;
        whoWon = 0;
		shopPermaBuff = 0;
		battleMode = false;
        shop.Restart();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    //click in the shop
    public void AddToPlayer(Mascot mascot, GameObject shopSlot) 
    {
        foreach (GameObject slot in playerTray) 
        {
            if (!slot.activeSelf) 
            {
                slot.SetActive(true);
                slot.GetComponent<MascotDisplay>().mascot = mascot;
                slot.GetComponent<MascotDisplay>().CreateMascot(false);
				AddCoin(-shopSlot.GetComponent<MascotDisplay>().buyCost);
				shopSlot.SetActive(false);
				StartCoroutine(Add2PlayerHelper(slot.GetComponent<MascotDisplay>()));
                break;
            }
        }
    }

    //drag it to the slot
    public void AddToPlayer(Mascot mascot, GameObject shopSlot, GameObject slot)
    {
        if (shopSlot != null)
        {
            GameObject slotMascot = slot.transform.GetChild(1).gameObject;
            slotMascot.SetActive(true);
            slotMascot.GetComponent<MascotDisplay>().mascot = mascot;
            slotMascot.GetComponent<MascotDisplay>().CreateMascot(false);
            AddCoin(-shopSlot.GetComponent<MascotDisplay>().buyCost);
            shopSlot.SetActive(false);
            StartCoroutine(Add2PlayerHelper(slotMascot.GetComponent<MascotDisplay>()));
		}
        else 
        {
			slot.SetActive(true);
			slot.GetComponent<MascotDisplay>().mascot = mascot;
			slot.GetComponent<MascotDisplay>().CreateMascot(false);
			AllySummonedActivate(slot.GetComponent<MascotDisplay>());
		}
       
    }

    IEnumerator Add2PlayerHelper(MascotDisplay ally) 
    {
		yield return StartCoroutine(Buy(ally));
		yield return StartCoroutine(AllySummoned(ally));
        canBattle.SetBool("CanBattle", true);
	}

    public void AllySummonedActivate(MascotDisplay ally) 
    {
        StartCoroutine(AllySummoned(ally));
    }

    public void SwitchSlot(MascotDisplay mascot, MascotDisplay slotMascot) 
    {
        MascotDisplay tmp = Instantiate(mascot);
        tmp.DeepCopy(mascot, false);
        if (slotMascot.gameObject.activeSelf) 
        {
            if (slotMascot.mascot.id == mascot.mascot.id && slotMascot.level < 3) 
            {
                for (int i = 0; i < mascot.level; i++) 
                {
                    AddExp(slotMascot);
                }
                Destroy(tmp.gameObject);
                mascot.gameObject.SetActive(false);
                return;
            }
            mascot.DeepCopy(slotMascot, false);
        }
		else 
        {
            slotMascot.gameObject.SetActive(true);
            mascot.gameObject.SetActive(false);
        }
        slotMascot.DeepCopy(tmp, false);
        slotMascot.transform.localPosition = new Vector2(0, 0);

		Destroy(tmp.gameObject);
    }

    public void SwitchSlot(MascotDisplay mascot, MascotDisplay slotMascot, bool fromOpo)
    {
        MascotDisplay tmp = Instantiate(mascot);
        tmp.DeepCopy(mascot, fromOpo);
        if (slotMascot.gameObject.activeSelf) 
        {
            mascot.DeepCopy(slotMascot, fromOpo);          
        }
        else
        {
            slotMascot.gameObject.SetActive(true);
            mascot.gameObject.SetActive(false);
        }
        slotMascot.DeepCopy(tmp, fromOpo);
        Destroy(tmp.gameObject);
    }

    public void LevelUp(GameObject shopSlot, GameObject slot) 
    {
		AddCoin(-shopSlot.GetComponent<MascotDisplay>().buyCost);
		shopSlot.SetActive(false);
        MascotDisplay mascot = slot.transform.GetChild(1).GetComponent<MascotDisplay>();
        StartCoroutine(LevelUpHelper(mascot));
    }

    IEnumerator LevelUpHelper(MascotDisplay mascot)
    {
		yield return StartCoroutine(Buy(mascot));
		AddExp(mascot);
	}

	public void AddExp(MascotDisplay mascot,int time = 1)
	{	
		mascot.StatsBuff(time, time);
        for (int i = 0; i < time; i++) 
        {
            AddExpHelper(mascot);
		}		
	}

    private void AddExpHelper(MascotDisplay mascot) 
    {
		if (!LvlUpCheck(mascot))
		{
			mascot.level += 1;
			mascot.SetLvl(mascot.level, 0);
			StartCoroutine(AllyLvlUp(mascot));
            mascot.ResetLvl();
		}
	}

    private bool LvlUpCheck(MascotDisplay mascot)
    {
		int expLvl = 0;
        if (mascot.level == 1 && !mascot.levelPart1.activeSelf)
        {
            mascot.SetLvl(mascot.level, 1);
            return true;
        }
        else if (mascot.level == 1 && mascot.levelPart1.activeSelf) 
        {
            return false;
        }
		else if (mascot.level == 2)
		{
			foreach (GameObject exp in mascot.levelPart2)
			{
				expLvl++;
				if (!exp.activeSelf)
				{
					mascot.SetLvl(mascot.level, expLvl);
					return true;
				}
			}
		}
        return false;
	}

	public void ChargeForward()
	{
        if (HaveMascot()) 
        {
			if (playerName.Equals(""))
			{
				chargedForward = true;
				StartCoroutine(LerpFunction(true));
				return;
			}
            march = GameObject.Find("March").GetComponent<PlayMusic>();
			march.Play();
			descriptionCamvas.SetActive(false);
            StartCoroutine(StartBattle());
        }       
    }

    public void SelectName() 
    {
		if (playerName.Equals(""))
		{
			StartCoroutine(LerpFunction(true));
		}
	}

    public bool HaveMascot() 
    {
        foreach (GameObject slot in playerTray) 
        {
            if(slot.activeSelf)
                return true;
        }
        return false;
    }

    IEnumerator StartBattle()
    {
        PushUp();
        yield return new WaitForSeconds(0.5f);
        player = "";
        for (int i = 0; i < 5; i++) 
        {
            if (playerTray[i].gameObject.activeSelf)
            {
                playerTray[i].GetComponent<MascotDisplay>().moveFromShop();
                player += playerTray[i].GetComponent<MascotDisplay>().GetMascotInfo();
                yield return new WaitForSeconds(0.1f);
            }
            if(i < 4)
                player += "/";
        }
		Add2Data(player);
		transtion.GetComponent<Animator>().SetTrigger("start");
	    yield return new WaitForSeconds(animationDic["Transtion"]);
        battleMode = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
	}

    public void TurnSlow() 
    {
		Time.timeScale = 1.5f;
		fastBtn.SetActive(false);
		slowBtn.SetActive(true);
	}

    public void TurnFast() 
    {
		Time.timeScale = 3;
        fastBtn.SetActive(true);
        slowBtn.SetActive(false);
	}

    public void Pause() 
    {
        if (!pause)
        {
			Time.timeScale = 0;
            pause = true;
			PauseScreen.SetActive(true);          
		}
        else 
        {
			Time.timeScale = fastBtn.activeSelf ? 3 : 1.5f;
			pause = false;
			PauseScreen.SetActive(false);
			StartCoroutine(CheckForDeath());
		}       
    }

    private void PushUp() 
    {
        for (int i = 4; i > 1; i--) 
        {
            if (playerTray[i].activeSelf && !playerTray[i - 1].activeSelf) 
            {
                SwitchSlot(playerTray[i].GetComponent<MascotDisplay>(), playerTray[i - 1].GetComponent<MascotDisplay>(), false);
                PushUp();
                return;
            }
        }
    }
}
