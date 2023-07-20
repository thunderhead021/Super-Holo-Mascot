using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
	[HideInInspector]
    public int level;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public MascotDisplay mascot;
    public Mascot Token;


    public virtual void Buy() { }

    public virtual void Knockout(int excessDmg) { }

	public virtual bool ProtectAlly(MascotDisplay whoHurt) 
    {
        return false;
    }

    public virtual void ShieldAlly(int dmg, MascotDisplay fromWho) { }

    public virtual int StatBuffTime() { return 1;}

    public virtual void AllySummoned(MascotDisplay ally) { }

    public virtual void StartOfBattle() 
    {
    }

    public virtual void StartOfShop() { }

    public virtual void EndOfBattle()
    {
    }

    public virtual void BeforeAtk(){ }

    public virtual void Sell() { }

    public virtual void ReceiveItem() { }

    public virtual void DealDmg()
    {
    }

    public virtual int ReceiveDmg(int dmg) { return 0; }

    public virtual void AnyFaint(MascotDisplay mascot) { }

    public virtual void Faint()
    {
    }

	public virtual void FaintSummon()
	{
	}

	public virtual void Active()
    {
        Debug.Log("acitve skill");
    }

    public virtual void AllyLvlUp(MascotDisplay mascot) {}

    public virtual void LvlUp() { }

    protected int GetActiveMascotNum(bool fromOpo)
    {
        int max = 0;
		foreach (GameObject slot in fromOpo ? gameManager.opoTray : gameManager.playerTray)
		{
			if (slot.activeSelf && !slot.GetComponent<MascotDisplay>().death)
				max++;
		}
		return max;
	}

    protected int GetSlotPos()
    {
		List<GameObject> tray = mascot.opo ? gameManager.opoTray : gameManager.playerTray;
		for (int i = 0; i < 5; i++)
        {		
			if (((tray[i].activeSelf) || (!tray[i].activeSelf && tray[i].GetComponent<MascotDisplay>().death)) && tray[i] == mascot.gameObject)
            {
				return i;
            }
        }
        return -1;
    }

    protected int GetNumOfSlot(bool fromOpo) 
    {
        int result = 0;
		for (int i = 0; i < 5; i++)
		{
			if (fromOpo ? gameManager.opoTray[i].activeSelf : gameManager.playerTray[i].activeSelf)
			{
                result++;
			}
		}
		return result;
    }

    protected void BuffSlot(int slot, List<GameObject> tray, string[] allSlot, int atkBuffBase, int hpBuffBase, MascotDisplay fromWho = null)
    {
        if (tray[slot].activeSelf) 
        {
            tray[slot].GetComponent<MascotDisplay>().StatsBuff(atkBuffBase * level, hpBuffBase * level, fromWho);
            if (!tray[slot].GetComponent<MascotDisplay>().opo && gameManager.IsBattle() && tray[slot].GetComponent<MascotDisplay>().mascot.id >= 0)
            {
                string info = allSlot[tray[slot].GetComponent<MascotDisplay>().startSlot];
                string[] infos = info.Split('_'); //get infos[1] and infos[2]
                infos[1] = (int.Parse(infos[1]) + atkBuffBase * level).ToString();
                infos[2] = (int.Parse(infos[2]) + hpBuffBase * level).ToString();
                allSlot[tray[slot].GetComponent<MascotDisplay>().startSlot] = gameManager.CreateNewInfo(infos);
            }
        }
		gameManager.CreateNewInfo();
	}

	protected void AddEffectSlot(int slot, List<GameObject> tray, string[] allSlot, Material effect, int effectId)
	{
		if (tray[slot].activeSelf)
		{
			tray[slot].GetComponent<MascotDisplay>().AddEffect(effect, effectId, mascot);
			if (!tray[slot].GetComponent<MascotDisplay>().opo && gameManager.IsBattle() && tray[slot].GetComponent<MascotDisplay>().mascot.id >= 0)
			{
				string info = allSlot[tray[slot].GetComponent<MascotDisplay>().startSlot];
				string[] infos = info.Split('_'); //get infos[1] and infos[2]
				infos[5] = effectId.ToString();
				allSlot[tray[slot].GetComponent<MascotDisplay>().startSlot] = gameManager.CreateNewInfo(infos);
			}
		}
		gameManager.CreateNewInfo();
	}
}
