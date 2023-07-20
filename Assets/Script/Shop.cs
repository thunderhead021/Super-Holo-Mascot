using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> MascotShop;
    [SerializeField]
    private List<GameObject> ItemShop;
    public GameManager gameManager;
    public static int[] Freezer = { -1,-1,-1,-1,-1,-1,-1};
    public int[] slotWReduceCost = { -1, -1};
    private bool start = true;
    private List<Mascot> curMascotList;
    private List<Item> curItemList;
	// Start is called before the first frame update
	void Start()
    {
        StartCoroutine(StartHelper());
	}

    IEnumerator StartHelper() 
    {
        yield return new WaitForSeconds(0.5f);
        curMascotList = gameManager.GetCurMascotList();
        curItemList = gameManager.GetCurItemList();
		Refresh();
        start = false;
	}

    public void Restart() 
    {
        for (int i = 0; i < Freezer.Length; i++) 
        {
            Freezer[i] = -1;
        }
    }

    public void Refresh() 
    {
        if ((GetAllMascotInFreezer() + GetAllItemInFreezer() < 7 && gameManager.coin >= 1) || start) 
        {
			MascotShopRefresh();
			ItemRefresh();
            if (gameManager.freeRefreshTimes == 0)
            {
                if(!start)
                    gameManager.AddCoin(-1);
			}
            else
                gameManager.freeRefreshTimes -= 1;

            if(!start)
				gameManager.RefreshAudio();
		}    
    }

    private void MascotShopRefresh() 
    {
        for (int i = 0; i < 5; i++) 
        {
            if (Freezer[i] >= 0)
            {
                Mascot mascot = gameManager.mascotList[Freezer[i]];
                MascotShop[i].GetComponent<MascotDisplay>().mascot = mascot;
                MascotShop[i].GetComponent<MascotDisplay>().InShop = true;
				MascotShop[i].GetComponent<MascotDisplay>().freeze.SetActive(true);
				MascotShop[i].GetComponent<MascotDisplay>().CreateMascot(false);
                if (slotWReduceCost[0] == 1)
                {
                    MascotShop[i].GetComponent<MascotDisplay>().buyCost = slotWReduceCost[1];
                    slotWReduceCost[0] = 0;
				}
                else 
                {
                    slotWReduceCost[0] -= 1;
				}
                MascotShop[i].SetActive(true);
            }
            else 
            {
				int rand = Random.Range(0, curMascotList.Count);
				Mascot mascot = curMascotList[rand];
				MascotShop[i].GetComponent<MascotDisplay>().mascot = mascot;
				MascotShop[i].GetComponent<MascotDisplay>().InShop = true;
				MascotShop[i].GetComponent<MascotDisplay>().CreateMascot(false);
				MascotShop[i].SetActive(true);
			}
        }
    }

    private void ItemRefresh() 
    {
        for (int i = 0; i < 2; i++) 
        {
            if (Freezer[i + 5] >= 0)
            {
                Item item = gameManager.itemList[Freezer[i + 5]];
                ItemShop[i].GetComponent<ItemDisplay>().item = item;
                ItemShop[i].GetComponent<ItemDisplay>().CreateItem();
				ItemShop[i].GetComponent<ItemDisplay>().freeze.SetActive(true);
				ItemShop[i].SetActive(true);
            }
            else 
            {
				int rand = Random.Range(0, curItemList.Count);
				Item item = curItemList[rand];
				ItemShop[i].GetComponent<ItemDisplay>().item = item;
				ItemShop[i].GetComponent<ItemDisplay>().CreateItem();
                ItemShop[i].transform.localPosition = new Vector2(0, -5);
				ItemShop[i].SetActive(true);
			}
		}
    }

    public void ReplaceItemsShop(Item item) 
    {
		for (int i = 0; i < 2; i++)
		{
			ItemShop[i].GetComponent<ItemDisplay>().item = item;
			ItemShop[i].GetComponent<ItemDisplay>().CreateItem();
			ItemShop[i].GetComponent<ItemDisplay>().freeze.SetActive(false);
			ItemShop[i].transform.localPosition = new Vector2(0, -5);
			ItemShop[i].SetActive(true);
		}
	}

    public void ReplaceItem(int pos, Item item) 
    {
		ItemShop[pos].GetComponent<ItemDisplay>().item = item;
		ItemShop[pos].GetComponent<ItemDisplay>().CreateItem();
		ItemShop[pos].transform.localPosition = new Vector2(0, -5);
        ItemShop[pos].GetComponent<ItemDisplay>().freeze.SetActive(false);
		ItemShop[pos].SetActive(true);
	}

    public void Add2Freezer(int id, int slot) 
    {
        Freezer[slot] = id;
    }

    public int GetAllMascotInFreezer() 
    {
        int result = 0;
        for (int i = 0; i < 5; i++) 
        {
            if (Freezer[i] >= 0)
                result++;
        }
        return result;
    }

	public int GetAllItemInFreezer()
	{
		int result = 0;
		for (int i = 0; i < 2; i++)
		{
			if (Freezer[i+5] >= 0)
				result++;
		}
		return result;
	}

    public void BuffAll() 
    {
        foreach (GameObject slot in MascotShop) 
        {
            if (slot.activeSelf)
                slot.GetComponent<MascotDisplay>().StatsBuff(2, 1);
        }
    }
}
