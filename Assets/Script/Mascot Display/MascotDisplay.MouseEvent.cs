using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class MascotDisplay : MonoBehaviour, IDragHandler, IPointerClickHandler
{
	private static LTDescr delay;

	public GameObject sellPrompt;
	public GameObject shop;
	public GameObject freeze;
	public int shopSlotnum = -1;

	private bool sell = false;

	public void OnPointerEnter() 
	{
		if (!gameManager.effectOn && !gameManager.settingPause)
		{
			delay = LeanTween.delayedCall(0.2f, () =>
			{
				gameManager.description.SetActive(true);
				gameManager.description.GetComponent<Description>().MascotSetText(mascot, mascotInfo[5], this);
			});
		}
		if (gameManager.pause) 
		{
			gameManager.description.SetActive(true);
			gameManager.description.GetComponent<Description>().MascotSetText(mascot, mascotInfo[5], this);
		}
	}

	public void OnPointerExit() 
	{
		DisableDes();
	}

	public void OnPointerClick(PointerEventData p)
	{
		if (InShop && !drag && !gameManager.effectOn && !gameManager.pause)
		{
			if (p.button == PointerEventData.InputButton.Left && gameManager.coin >= gameObject.GetComponent<MascotDisplay>().buyCost)
			{
				gameManager.AddToPlayer(mascot, gameObject);
				gameManager.BuyAudio();
				freeze.SetActive(false);
				shop.GetComponent<Shop>().Add2Freezer(-1, shopSlotnum);
			}
			else if (p.button == PointerEventData.InputButton.Right)
			{
				freeze.SetActive(freeze.activeSelf ? false : true);
				if (freeze.activeSelf)
					gameManager.FreezeAudio();
				shop.GetComponent<Shop>().Add2Freezer(freeze.activeSelf ? mascot.id : -1, shopSlotnum);
			}
			DisableDes();
		}

	}

	public void OnBeginDrag()
	{
		if (!gameManager.effectOn && ((InShop && gameManager.coin >= gameObject.GetComponent<MascotDisplay>().buyCost) || !InShop) && !gameManager.settingPause)
		{
			GetCurTray();
			if (curTray.Count > 0) 
			{
				DisableDes();
				curPos = transform.position;
				drag = true;
			}			
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (curTray.Count > 0)
		{
			DisableDes();
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
			transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
			GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			foreach (GameObject slot in curTray)
			{
				UnHighlightAll();
				if (RectTransformUtility.RectangleContainsScreenPoint(slot.GetComponent<RectTransform>(), mousePosition))
				{
					targetSlot = slot;
					HighlightSelect(slot, true);
					break;
				}
				targetSlot = null;
			}
			if (RectTransformUtility.RectangleContainsScreenPoint(sellPrompt.GetComponent<RectTransform>(), mousePosition))
			{
				sell = true;
			}
			else
			{
				sell = false;
			}
		}
	}

	public void OnEndDrag()
	{
		if (curTray.Count > 0)
		{
			DisableDes();
			drag = false;
			transform.localPosition = new Vector2(0, 0);
			Add2Tray();
			foreach (GameObject slot in curTray)
			{
				slot.transform.GetChild(2).gameObject.SetActive(false);
			}
			curTray.Clear();
			sellPrompt.SetActive(false);
			shop.SetActive(true);
		}
	}



	private void DisableDes()
	{
		if (delay != null) 
		{
			LeanTween.cancel(delay.uniqueId);
		}
		if (gameManager.description != null)
		{
			gameManager.description.SetActive(false);
			gameManager.description.GetComponent<Description>().Reset();
		}
	}

	private void GetCurTray()
	{
		if (InShop)
		{
			foreach (GameObject slot in tray)
			{
				GameObject mascot = slot.transform.GetChild(1).gameObject;
				if (!mascot.activeSelf || (mascot.GetComponent<MascotDisplay>().mascot.id == this.mascot.id && mascot.GetComponent<MascotDisplay>().level < 3))
				{
					slot.transform.GetChild(2).gameObject.SetActive(true);
					HighlightSelect(slot, false);
					curTray.Add(slot);
				}
			}
		}
		else 
		{
			sellPrompt.SetActive(true);
			shop.SetActive(false);
			foreach (GameObject slot in tray)
			{
				GameObject mascot = slot.transform.GetChild(1).gameObject;
				if (mascot != gameObject) 
				{
					slot.transform.GetChild(2).gameObject.SetActive(true);
					HighlightSelect(slot, false);
					curTray.Add(slot);
				}
			}

		}
	}

	private void UnHighlightAll()
	{
		foreach (GameObject slot in curTray)
		{
			HighlightSelect(slot, false);
		}
	}

	private void HighlightSelect(GameObject slot, bool isTarget)
	{
		GameObject arrow = slot.transform.GetChild(2).transform.GetChild(0).gameObject;
		GameObject frame = slot.transform.GetChild(2).transform.GetChild(1).gameObject;
		arrow.GetComponent<Image>().color = isTarget ? Color.white : new Color(100f / 255f, 100f / 255f, 100f / 255f);
		frame.GetComponent<Image>().color = isTarget ? Color.white : new Color(100f / 255f, 100f / 255f, 100f / 255f);
	}

	private void Add2Tray()
	{
		if (targetSlot != null)
		{
			if (InShop)
			{
				GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
				freeze.SetActive(false);
				gameManager.BuyAudio();
				shop.GetComponent<Shop>().Add2Freezer(-1, shopSlotnum);
				if (!targetSlot.transform.GetChild(1).gameObject.activeSelf)
					gameManager.AddToPlayer(mascot, gameObject, targetSlot);
				else if (targetSlot.transform.GetChild(1).gameObject.activeSelf)
					gameManager.LevelUp(gameObject, targetSlot);
			}
			else
			{
				gameManager.SwitchSlot(this, targetSlot.transform.GetChild(1).gameObject.GetComponent<MascotDisplay>());
			}	
		}
		else 
		{
			if (sell && !InShop)
			{
				gameManager.AddCoin(sellCost * level);
				gameManager.SellAudio();
				ResetMascot();
				freeze.SetActive(false);
				if (!gameManager.HaveMascot())
					gameManager.canBattle.SetBool("CanBattle", false);
				Sell();
			}
			if (InShop) 
			{
				GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
			}
		}
	}
}
