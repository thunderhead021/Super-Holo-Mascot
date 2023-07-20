using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Book : MonoBehaviour
{
	[SerializeField]
	private GameObject book;
	[SerializeField]
	private GameObject close;
	[SerializeField]
	private GameManager gameManager;
	[SerializeField]
	private Button[] buttons;

	[SerializeField]
	private GameObject leftPage;
	[SerializeField]
	private GameObject leftTopImage; //also for detail
	[SerializeField]
	private Text leftPageTopText;   //also for detail
	[SerializeField]
	private Text leftPageBottomText; //also for detail
	[SerializeField]
	private Scrollbar leftPageBottomSb;
	[SerializeField]
	private GameObject detailPage;
	[SerializeField]
	private GameObject detailPageImage;
	[SerializeField]
	private GameObject detailPageTierText;
	[SerializeField]
	private GameObject detailPageAtkText;
	[SerializeField]
	private GameObject detailPageHpText;
	[SerializeField]
	private GameObject detailPageAtk;
	[SerializeField]
	private GameObject detailPageHp;
	[SerializeField]
	private GameObject detailPageCreditText;
	[SerializeField]
	private GameObject leftMainPage;
	[SerializeField]
	private GameObject leftMainPageBtn;

	[SerializeField]
	private GameObject rightPage;
	[SerializeField]
	private GameObject rightTopImage;
	[SerializeField]
	private Text rightPageTopText;
	[SerializeField]
	private Text rightPageBottomText;
	[SerializeField]
	private GameObject listPage;
	[SerializeField]
	private GameObject listPageContent;
	[SerializeField]
	private GameObject rightMainPage;

	private bool itemChoose = false;
	[SerializeField]
	private Sprite Items;
	[SerializeField]
	private Sprite Mascots;
	[SerializeField]
	private Description description;
	[SerializeField]
	private GameObject ButtonInfo;
	private int curTier = 0;
	[SerializeField]
	private AudioSource _audioSource;
	[SerializeField]
	private Item[] Itemtoken;

	public void Play(AudioClip _audio)
	{
		if (_audioSource.isPlaying)
			_audioSource.Stop();
		_audioSource.clip = _audio;
		_audioSource.Play();
	}

	public void StopAudio() 
	{
		_audioSource.Stop();
	}

	public void ShowMascotDetail(Mascot mascot) 
	{
		leftPageBottomSb.value = 1;
		leftTopImage.GetComponent<Image>().sprite = mascot.playerArt;
		leftTopImage.SetActive(true);
		detailPageImage.GetComponent<Image>().sprite = mascot.playerArt;
		detailPageImage.SetActive(true);

		leftPageTopText.text = mascot.mascotName;

		detailPageTierText.GetComponent<Text>().text = mascot.tier.ToString();
		detailPageTierText.SetActive(true);

		switch (mascot.id)
		{
			case -5:
				detailPageAtkText.GetComponent<Text>().text = "1/2/3";
				detailPageHpText.GetComponent<Text>().text = "1/2/3";
				break;
			case -8:
				detailPageAtkText.GetComponent<Text>().text = "2/4/6";
				detailPageHpText.GetComponent<Text>().text = "2/4/6";
				break;
			case -12:
				detailPageAtkText.GetComponent<Text>().text = "4/8/12";
				detailPageHpText.GetComponent<Text>().text = "4/8/12";
				break;
			default:
				detailPageAtkText.GetComponent<Text>().text = mascot.atk.ToString();
				detailPageHpText.GetComponent<Text>().text = mascot.hp.ToString();
				break;
		}
		
		detailPageAtkText.SetActive(true);		
		detailPageHpText.SetActive(true);
		
		switch (mascot.id) 
		{
			case 30:
				leftPageBottomText.text = "(1)(2) Start of shop: Gain 1 Experience \r\n(3) Start of battle: Transform into grow up Chungami";
				break;
			case -1:
				leftPageBottomText.text = "(Obtain from Nekko)";
				break;
			case -2:
				leftPageBottomText.text = description.GetMascotDescrib(mascot) + "\r\n (Obtain from Chungami)";
				break;
			case -4:
				leftPageBottomText.text = description.GetMascotDescrib(mascot) + "\r\n" 
					+ "(the effect is only active at level 3)";
				break;
			case -5:
				leftPageBottomText.text = "(Obtain from Haaton)";
				break;
			case -8:
				leftPageBottomText.text = "(Obtain from Takodachi)";
				break;
			case -11:
				leftPageBottomText.text = "(Obtain from Kotori)";
				break;
			case -12:
				leftPageBottomText.text = "(Obtain from Jobz)";
				break;
			default:
				leftPageBottomText.text = description.GetMascotDescrib(mascot);
				break;
		}

		detailPageCreditText.GetComponent<RectTransform>().anchoredPosition = new Vector2(65.74997f, -156.73f);
		if (!mascot.credit.Equals(""))
		{
			detailPageCreditText.GetComponent<Hyperlinks>().link = mascot.credit;
			detailPageCreditText.SetActive(true);
		}
		else 
		{
			detailPageCreditText.SetActive(false);
		}
	}

	public void ShowItemDetail(Item item)
	{
		leftPageBottomSb.value = 1;
		leftTopImage.GetComponent<Image>().sprite = item.art;
		leftTopImage.SetActive(true);
		detailPageImage.GetComponent<Image>().sprite = item.art;
		detailPageImage.SetActive(true);

		leftPageTopText.text = item.itemName;

		detailPageTierText.GetComponent<Text>().text = item.tier.ToString();
		detailPageTierText.SetActive(true);

		if (item.id == -4)
			leftPageBottomText.text = "+1/2 Attack\r\n+1/2 Health";
		else
			leftPageBottomText.text = description.GetItemDescrib(item);

		detailPageCreditText.GetComponent<RectTransform>().anchoredPosition = new Vector2(65.74997f, -93.73f);
		if (!item.credit.Equals(""))
		{
			detailPageCreditText.GetComponent<Hyperlinks>().link = item.credit;
			detailPageCreditText.SetActive(true);
		}
		else
		{
			detailPageCreditText.SetActive(false);
		}
	}

	public void OpenBook()
	{
		foreach (Button button in buttons)
		{
			button.interactable = false;
		}
		gameManager.settingPause = true;
		book.SetActive(true);
	}

	public void CloseBook()
	{
		leftPage.SetActive(false);
		rightPage.SetActive(false);
		close.SetActive(false);
		gameManager.description.SetActive(false);
		GetComponent<Animator>().SetTrigger("Close");
	}

	public void TurnOffDes() 
	{
		gameManager.description.SetActive(false);
	}

	public void FinishOpenBook()
	{
		leftPageTopText.text = "Items list";
		rightPageTopText.text = "Mascots list";

		leftPageBottomText.text = "Show all available items";
		rightPageBottomText.text = "Show all available mascots";
		detailPageCreditText.SetActive(false);
		leftPage.SetActive(true);
		leftMainPageBtn.SetActive(true);
		rightPage.SetActive(true);
		close.SetActive(true);
	}

	public void FinishCloseBook()
	{
		foreach (Button button in buttons)
		{
			button.interactable = true;
		}
		gameManager.settingPause = false;
		rightMainPage.SetActive(true);
		leftMainPage.SetActive(true);
		leftTopImage.SetActive(true);
		RectTransform rectTransform = GetComponent<RectTransform>();
		rectTransform.anchoredPosition = Vector3.zero;
		rectTransform.localScale = new Vector3(1,1,1);
		detailPage.SetActive(false);
		listPage.SetActive(false);
		book.SetActive(false);
	}

	public void ReturnToHome()
	{
		curTier = 0;
		detailPageTierText.GetComponent<Text>().text = "";
		detailPageAtkText.GetComponent<Text>().text = "";
		detailPageHpText.GetComponent<Text>().text = "";
		detailPageImage.SetActive(false);
		detailPageCreditText.SetActive(false);

		leftPageTopText.text = "Items list";
		rightPageTopText.text = "Mascots list";

		leftPageBottomText.text = "Show all available items";
		rightPageBottomText.text = "Show all available mascots";

		leftTopImage.GetComponent<Image>().sprite = Items;
		rightTopImage.GetComponent<Image>().sprite = Mascots;

		rightMainPage.SetActive(true);
		leftMainPage.SetActive(true);
		leftMainPageBtn.SetActive(true);
		leftTopImage.SetActive(true);

		detailPage.SetActive(false);
		listPage.SetActive(false);

		rightPage.SetActive(true);
		leftPage.SetActive(true);
	}

	public void ReturnBtn() 
	{
		rightPage.SetActive(false);
		leftPage.SetActive(false);
		GetComponent<Animator>().SetTrigger("Return Home");
	}

	public void OpenItemList() 
	{
		itemChoose = true;
		GetComponent<Animator>().SetTrigger("Choosed");
	}

	public void OpenMascotList()
	{
		itemChoose = false;
		GetComponent<Animator>().SetTrigger("Choosed");
	}

	public void ShowList() 
	{
		FlipList();
		Populate(1);
	}

	public void FlipList()
	{
		rightMainPage.SetActive(false);
		leftMainPage.SetActive(false);
		leftMainPageBtn.SetActive(false);
		leftTopImage.SetActive(false);
		leftPageTopText.text = "";
		leftPageBottomText.text = "";
		if (itemChoose)
		{
			rightPageTopText.text = "Items list";
			detailPageAtk.SetActive(false);
			detailPageHp.SetActive(false);
			rightTopImage.GetComponent<Image>().sprite = Items;
		}
		else
		{
			rightPageTopText.text = "Mascots list";
			detailPageAtk.SetActive(true);
			detailPageHp.SetActive(true);
			rightTopImage.GetComponent<Image>().sprite = Mascots;
		}
		detailPage.SetActive(true);
		listPage.SetActive(true);
		leftPage.SetActive(true);
		rightPage.SetActive(true);
	}

	public void TierShowFinish() 
	{
		leftPage.SetActive(false);
		rightPage.SetActive(false);
	}

	public void SwitchTab(int tier)
	{
		if (tier != curTier) 
		{
			leftTopImage.SetActive(false);
			leftPageTopText.text = "";
			leftPageBottomText.text = "";
			detailPageTierText.GetComponent<Text>().text = "";
			detailPageAtkText.GetComponent<Text>().text = "";
			detailPageHpText.GetComponent<Text>().text = "";
			detailPageImage.SetActive(false);
			detailPageTierText.SetActive(false);
			detailPageAtkText.SetActive(false);
			detailPageHpText.SetActive(false);
			detailPageCreditText.SetActive(false);

			GetComponent<Animator>().SetTrigger("Flip");
			leftPage.SetActive(false);
			rightPage.SetActive(false);
			Populate(tier);
		}		
	}

	public void Populate(int tier) 
	{
		ClearListPageContentChildren();
		if (itemChoose)
		{
			foreach (var x in getItemList(tier))
			{
				GameObject newObj = Instantiate(ButtonInfo, listPageContent.transform);
				newObj.GetComponent<ButtonInfo>().item = x;
				newObj.GetComponent<ButtonInfo>().ChangeImage();
			}
		}
		else
		{
			foreach (var x in getMascotList(tier))
			{
				GameObject newObj = Instantiate(ButtonInfo, listPageContent.transform);
				newObj.GetComponent<ButtonInfo>().mascot = x;
				newObj.GetComponent<ButtonInfo>().ChangeImage();
			}
		}
		curTier = tier;
	}

	private void ClearListPageContentChildren()
	{
		var children = listPageContent.transform.Cast<Transform>().ToArray();

		foreach (var child in children)
		{
			Destroy(child.gameObject);
		}
	}

	private List<Item> getItemList(int tier) 
	{
		switch (tier) 
		{
			case 1:
				List<Item> list1 = new List<Item>(gameManager.itemTier1)
				{
					Itemtoken[3]
				};
				return list1;
			case 2:
				return gameManager.itemTier2;
			case 3:
				return gameManager.itemTier3;
			case 4:
				return gameManager.itemTier4;
			case 5:
				List<Item> list2 = new List<Item>(gameManager.itemTier5)
				{
					Itemtoken[0],
					Itemtoken[1],
					Itemtoken[2]
				};
				return list2;
		}
		return null;
	}

	private List<Mascot> getMascotList(int tier)
	{
		List<Mascot> list = new List<Mascot>();	
		switch (tier)
		{
			case 1:
				list = new List<Mascot>(gameManager.mascotTier1);
				list.Add(gameManager.tokenList[0]);
				list.Add(gameManager.tokenList[1]);
				break;
			case 2:
				list = new List<Mascot>(gameManager.mascotTier2);
				list.Add(gameManager.tokenList[12]);
				break;
			case 3:
				list = new List<Mascot>(gameManager.mascotTier3);
				list.Add(gameManager.tokenList[4]);
				break;
			case 4:
				list = new List<Mascot>(gameManager.mascotTier4);
				list.Add(gameManager.tokenList[11]);
				list.Add(gameManager.tokenList[7]);
				break;
			case 5:
				list = new List<Mascot>(gameManager.mascotTier5);
				list.Add(gameManager.tokenList[8]);
				break;
		}
		return list;
	}
}
