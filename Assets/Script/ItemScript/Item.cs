using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int tier;
    public Sprite art;
    public string description;
    public int cost = 3;
    public BuffType type;
	public string credit;
	[HideInInspector]
    public int atkBuff;
	[HideInInspector]
    public int hpBuff;
    [HideInInspector]
    public Material effect;
    [HideInInspector]
    public EffectType effectType;
    [HideInInspector]
    public PassiveType passiveType;
    [HideInInspector]
    public TriggerType triggerType;
    [HideInInspector]
    public ItemTriggerBase triggerSkill;
}
public enum PassiveType
{
    StartOfBattle,
    ShopOnly,
    BeforeAtk,
    AllTime,
	Knockout
}

public enum TriggerType
{
	Single,
	Multiple
}

public enum EffectType 
{
    Death,
    Passive
}

public enum BuffType 
{
    StatBuff,
    AddEffect,
    TriggerImmidiately
}
