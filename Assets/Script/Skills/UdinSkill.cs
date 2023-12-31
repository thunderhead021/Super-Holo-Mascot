﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UdinSkill : BaseSkill
{
    public override void StartOfBattle()
    {
        GameObject lvl1 = RandomRoll(null, null);
        GameObject lvl2 = level >= 2 ? RandomRoll(lvl1, null) : null;
        _ = level == 3 ? RandomRoll(lvl2, lvl1) : null;
    }

    private GameObject RandomRoll(GameObject slot1, GameObject slot2) 
    {
        int rand = Random.Range(0, GetNumOfSlot(mascot.opo ? false : true));
        List<GameObject> tray = mascot.opo ? gameManager.playerTray : gameManager.opoTray;
		for (int i = 0; i < 5; i++) 
        {
            if (rand == 0 && tray[i].activeSelf && tray[i] != slot1 && tray[i] != slot2) 
            {
                tray[i].GetComponent<MascotDisplay>().ReciveDmg(1, mascot.death, mascot, true);
                return tray[i];
            }
            if (rand > 0)
                rand--;
        }
        return null;
    }
}
