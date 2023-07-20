using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class ItemDisplay : MonoBehaviour, IDragHandler, IPointerClickHandler
{
	private static LTDescr delay;
	public GameObject freeze;
	public int shopSlotnum;
	public Shop shop;
	public GameObject BigSelect;

	private bool isDrag = false;

	public void OnPointerEnter()
	{
		if (!gameManager.effectOn && !isDrag && !gameManager.settingPause) 
		{
			delay = LeanTween.delayedCall(0.5f, () => {
				gameManager.description.SetActive(true);
				gameManager.description.GetComponent<Description>().ItemSetText(item);
			});
		}	
	}

	public void OnPointerExit()
	{
		if(delay != null)
			LeanTween.cancel(delay.uniqueId);
		if (gameManager.description != null) 
		{
			gameManager.description.SetActive(false);
			gameManager.description.GetComponent<Description>().Reset();
		}	
	}

	public void OnBeginDrag()
	{
		if (gameManager.coin >= buyCost && !gameManager.settingPause) 
		{
			FindAllSlot();
			if (!gameManager.effectOn && curSlots.Count > 0)
			{
				isDrag = true;
				if (delay != null)
					LeanTween.cancel(delay.uniqueId);
				if (gameManager.description != null)
				{
					gameManager.description.SetActive(false);
					gameManager.description.GetComponent<Description>().Reset();
				}
				gameManager.description.SetActive(false);
				gameManager.description.GetComponent<Description>().Reset();
				curPos = transform.position;
			}
		}	
	}

	private void FindAllSlot() 
	{
		foreach (GameObject slot in allSlots) 
		{
			GameObject mascot = slot.transform.GetChild(1).gameObject;

			if (mascot.activeSelf) 
			{

				if (item.type == BuffType.TriggerImmidiately && item.triggerType == TriggerType.Multiple)
				{
					CopyAllSlot2CurSlot();
					BigSelect.SetActive(true);
					break;
				}
				curSlots.Add(slot);
				slot.transform.GetChild(2).gameObject.SetActive(true);
			}
		}
	}


	private void CopyAllSlot2CurSlot() 
	{
		foreach (GameObject slot in allSlots)
		{
			curSlots.Add(slot);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (curSlots.Count > 0) 
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
			transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
			GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			if (gameManager.description != null)
			{
				gameManager.description.SetActive(false);
				gameManager.description.GetComponent<Description>().Reset();
			}
			foreach (GameObject slot in curSlots)
			{
				if (item.type == BuffType.TriggerImmidiately && item.triggerType == TriggerType.Multiple)
				{
					BigSelect.GetComponent<Image>().color = new Color(100f / 255f, 100f / 255f, 100f / 255f);
				}
				else
				{
					UnHighlightAll();
				}

				if (RectTransformUtility.RectangleContainsScreenPoint(slot.GetComponent<RectTransform>(), mousePosition))
				{
					targetSlot = slot;
					if (item.type == BuffType.TriggerImmidiately && item.triggerType == TriggerType.Multiple)
					{
						BigSelect.GetComponent<Image>().color = Color.white;

					}
					else
					{
						HighlightSelect(slot, true);
					}
					break;
				}
				targetSlot = null;
			}
		}
	}

	private void UnHighlightAll() 
	{
		foreach (GameObject slot in curSlots)
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

	public void OnEndDrag()
	{
		if (curSlots.Count > 0) 
		{
			StartCoroutine(ResetPos());
			if (targetSlot != null)
			{
				freeze.SetActive(false);
				shop.Add2Freezer(-1, shopSlotnum);
				BuffMascot();
				gameManager.AddCoin(-buyCost);
				gameManager.BuyAudio();
			}
			if (item.type == BuffType.TriggerImmidiately && item.triggerType == TriggerType.Multiple)
			{
				BigSelect.SetActive(false);
			}
			else
			{
				foreach (GameObject slot in curSlots)
				{
					slot.transform.GetChild(2).gameObject.SetActive(false);
				}
			}
			curSlots.Clear();
			isDrag = false;
		}	
	}

	private void BuffMascot() 
	{
		StartCoroutine(BuffMascotHelper());
	}

	IEnumerator BuffMascotHelper() 
	{
		MascotDisplay mascot = targetSlot.transform.GetChild(1).GetComponent<MascotDisplay>();
		switch (item.type)
		{
			case BuffType.AddEffect:
				mascot.AddEffect(item.effect, item.id);
				break;
			case BuffType.StatBuff:
				mascot.StatsBuff(item.atkBuff * gameManager.StatBuffTime(), item.hpBuff * gameManager.StatBuffTime());
				break;
			case BuffType.TriggerImmidiately:
				switch (item.triggerType) 
				{
					case TriggerType.Single:
						SingleTrigger(mascot);
						break;
					case TriggerType.Multiple:
						MultipleTrigger();
						break;
				}
				break;
		}
		yield return new WaitForSeconds(0.5f);		
		mascot.ReceiveItem();
		gameObject.SetActive(false);
	}

	public void Buff(MascotDisplay mascot, int atk, int hp) 
	{
		StartCoroutine(BuffSlot(mascot, atk, hp));
	}

	IEnumerator BuffSlot(MascotDisplay mascot, int atk, int hp) 
	{
		mascot.StatsBuff(atk * gameManager.StatBuffTime(), hp * gameManager.StatBuffTime());
		yield return new WaitForSeconds(0.5f);
		mascot.ReceiveItem();
	}

	IEnumerator ResetPos() 
	{
		if (targetSlot == null) 
		{
			transform.localPosition = new Vector2(0, -5);
			GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
			yield break;
		}
		yield return new WaitForSeconds(0.5f);
		transform.localPosition = new Vector2(0, -5);
		GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			freeze.SetActive(freeze.activeSelf ? false : true);
			if (freeze.activeSelf)
				gameManager.FreezeAudio();
			shop.Add2Freezer(freeze.activeSelf ? item.id : -1, shopSlotnum);
		}
	}
}
