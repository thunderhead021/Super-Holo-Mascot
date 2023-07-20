using UnityEngine;

[CreateAssetMenu(fileName = "New Mascot", menuName = "Mascot")]
public class Mascot : ScriptableObject
{
	public int id;
	public string mascotName;
	public int tier;
	public string description = ":Active: :Faint: :End of Battle: :Hurt enemy: :Before ATK: :Ally summoned: :Start of battle: :Start of shop: " +
		":Receive damage: :Receive item: :Sell: :Ally leveled up: :Knockout: :Buy:";
	public Sprite playerArt;
	public Sprite opoArt;
	public int hp;
	public int atk;
	public int timeUseSkill;
	public int cost = 3;
	public BaseSkill skill;
	public string credit;
}
