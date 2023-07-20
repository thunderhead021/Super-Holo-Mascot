using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnigiryaaSkill : BaseSkill
{
	public override void ReceiveItem()
	{
		mascot.StatsBuff(1 * level, 1 * level);
	}
}
