using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
	public TextMeshProUGUI objectName;

	public TextMeshProUGUI objectDescription;

	public TextMeshProUGUI item;

	public TextMeshProUGUI itemDescription;

	public Image tier;

	public List<Sprite> tierList;

	public int charWrapLimit;

	public GameManager gameManager;

	public LayoutElement layoutElement;

	public GameObject cost;

	public Text costText;

	RectTransform rectTransform;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	private void Update()
	{
		Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(pos.x, pos.y, 0);
		float pivotX = (rectTransform.localPosition.x + 960) / Screen.width > 1 ? 1 : (rectTransform.localPosition.x + 960) / Screen.width;
		float pivotY = (rectTransform.localPosition.y + 960) / (Screen.height * 6) > 1 ? 1 : (rectTransform.localPosition.y + 960) / (Screen.height * 6);

		rectTransform.pivot = new Vector2(pivotX, pivotY);
		
	}

	public void Tooltips(string Text) 
	{
		objectName.gameObject.SetActive(false);
		objectDescription.text = Text;
		ChangeLayout();
	}

	public void Tooltips(string Text, string Cost)
	{
		objectName.gameObject.SetActive(false);
		objectDescription.text = Text;
		tier.gameObject.SetActive(false);
		cost.SetActive(true);
		costText.text = Cost;
		ChangeLayout();
	}

	public string GetMascotDescrib(Mascot mascot) 
	{
		return mascot.description.Equals("") ? "" : mascot.description.Replace(" @@@ ", "\n");
	}

	public string GetItemDescrib(Item item)
	{
		string des = item.description.Equals("") ? "" : item.description;
		string result = "";
		switch (item.type)
		{
			case BuffType.TriggerImmidiately:
				result = des;
				break;
			case BuffType.StatBuff:
				result = item.hpBuff != 0 ? ((item.hpBuff > 0 ? "+" : " ") + item.hpBuff + " " + "health") : item.atkBuff != 0 ? ((item.atkBuff > 0 ? "+" : " ") + item.atkBuff + " " + "attack") : "";
				if (item.hpBuff != 0 && item.atkBuff != 0)
				{
					result = (item.atkBuff > 0 ? "+" : " ") + item.atkBuff + " " + "attack" + "\r\n" + (item.hpBuff > 0 ? "+" : " ") + item.hpBuff + " " + "health";
				}
				break;
			case BuffType.AddEffect:
				switch (item.effectType)
				{
					case EffectType.Death:
						result = "Add new effect" + "\r\n" + "Faint: " + des;
						break;
					case EffectType.Passive:
						result = "Add new effect" + "\r\n";
						switch (item.passiveType)
						{
							case PassiveType.StartOfBattle:
								result += "Start of battle: " + des;
								break;
							case PassiveType.BeforeAtk:
							case PassiveType.ShopOnly:
								result += des;
								break;
							case PassiveType.Knockout:
							case PassiveType.AllTime:
								result = des.Replace(" @@@ ", "\n");
								break;
						}
						break;
				}
				break;
		}
		return result;
	}

	public void MascotSetText(Mascot mascot, string effectId, MascotDisplay mascotDisplay) 
	{
		objectName.text = mascot.mascotName;
		if (!mascot.description.Equals(""))
		{
			string des = GetMascotDescrib(mascot);
			switch (mascot.id)
			{
				case 23:
					des += "\n\nNumber of friendly members: " + gameManager.GetNumOfSSMember(mascotDisplay.InShop ? false : mascotDisplay.opo);
					break;
			}
			objectDescription.text = des;
			objectDescription.gameObject.SetActive(true);
		}
		else
			objectDescription.gameObject.SetActive(false);

		cost.SetActive(true);
		if (mascotDisplay.InShop)
			costText.text = "Cost:" + mascotDisplay.buyCost.ToString() + " SC";
		else
			costText.text = "Sell:" + mascotDisplay.sellCost.ToString() + " SC";
		if (!effectId.Equals("n")) 
		{
			itemDescription.gameObject.SetActive(true);
			item.gameObject.SetActive(true);
			if (int.Parse(effectId) >= 0)
				itemDescription.text = gameManager.itemList[int.Parse(effectId)].itemName.Replace(" @@@ ", "\n");
			else
			{
				switch (int.Parse(effectId))
				{
					case -1:
						itemDescription.text = "take 3 additional damage";
						break;
					case -2:
						itemDescription.text = "Block damage once";
						break;
				}
			}
		}
		tier.sprite = tierList[mascotDisplay.mascot.tier - 1];
		tier.gameObject.SetActive(true);
		ChangeLayout();
	}

	public void ItemSetText(Item item)
	{
		objectName.text = item.itemName;
		cost.SetActive(true);
		costText.text = "Cost:" + item.cost + " SC";
		tier.sprite = tierList[item.tier - 1];
		tier.gameObject.SetActive(true);
		objectDescription.gameObject.SetActive(true);
		objectDescription.text = GetItemDescrib(item);
		ChangeLayout();
	}
	
	public void Reset()
	{
		objectName.gameObject.SetActive(true);
		itemDescription.gameObject.SetActive(false);
		item.gameObject.SetActive(false);
		cost.SetActive(false);
	}

	private void ChangeLayout() 
	{
		int nameLength = objectName.text.Length;
		int desLength = objectDescription.text.Length;
		int itemLenght = itemDescription.text.Length;

		layoutElement.enabled = (nameLength > charWrapLimit || desLength > charWrapLimit || itemLenght > charWrapLimit);
	}
}
