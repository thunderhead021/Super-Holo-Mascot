using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class ItemDisplay
{
    public Item item;
	public Image art;
	public List<GameObject> allSlots;
	public GameManager gameManager;
	[HideInInspector]
	public int buyCost;
	private List<GameObject> curSlots = new List<GameObject>();
	private Vector3 curPos;
	private GameObject targetSlot;
	// Start is called before the first frame update
	void Start()
    {
		CreateItem();

	}

	public void CreateItem()
	{
		art.sprite = item.art;
		buyCost = item.cost;
	}

	public void MultipleTrigger() 
	{
		item.triggerSkill.gameManager = gameManager;
		item.triggerSkill.item = this;
		item.triggerSkill.Multiple();
	}

	public void SingleTrigger(MascotDisplay mascot)
	{
		item.triggerSkill.gameManager = gameManager;
		item.triggerSkill.item = this;
		item.triggerSkill.Single(mascot);
	}
}
