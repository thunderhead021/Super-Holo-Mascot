using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoronesukiSkill : BaseSkill
{
	public override int ReceiveDmg(int dmg)
	{
		return (int)(dmg * 0.2 * level) <= 0 ? 1 : (int)(dmg * 0.2 * level);
	}
}
