public partial class MascotDisplay
{
    private void SetSkillInfo() 
    {
        mascot.skill.level = level;
        mascot.skill.gameManager = gameManager;
        mascot.skill.mascot = this;
    }

    public void StartOfBattle() 
    {
        SetSkillInfo();
        mascot.skill.StartOfBattle();
       
        if (!mascotInfo[5].Equals("n")) 
        {
            switch (int.Parse(mascotInfo[5])) 
            {
                case 2:
                    gameObject.GetComponent<AZKi>().StartOfBattle();
                    break;
            }
        }
    }

    private bool HaveItemWithMethod(string methodName) 
    {
        if (!mascotInfo[5].Equals("n"))
        {
            switch (methodName) 
            {
                case "StartOfBattle":
                    switch (int.Parse(mascotInfo[5]))
                    {
                        case 2:
                            return true;
                    }
                    break;
                case "StartOfShop":
					switch (int.Parse(mascotInfo[5]))
					{
						case 0:
						case 6:
						case 17:
							return true;
					}
					break;
				case "FaintSummon":
					switch (int.Parse(mascotInfo[5]))
					{
						case 14:
							return true;
						case 16:
							return true;
					}
					break;
				case "Knockout":
					switch (int.Parse(mascotInfo[5]))
					{
						case 13:
							return true;
					}
					break;
				case "ReceiveDmg":
					switch (int.Parse(mascotInfo[5]))
					{
						case 10:
						case 11:
							return true;
					}
					break;
			}
               
        }
        return false;
    }

    public bool IsOverided(string methodName) 
    {
        return mascot.skill.GetType().GetMethod(methodName).DeclaringType != typeof(BaseSkill) || HaveItemWithMethod(methodName);
    }

	public void AnyFaint(MascotDisplay other) 
	{
		SetSkillInfo();
		mascot.skill.AnyFaint(other);
	}

	public void Faint() 
    {
        SetSkillInfo();
        mascot.skill.Faint();
    }

	public void Knockout(int excessDmg)
	{
		SetSkillInfo();
		mascot.skill.Knockout(excessDmg);
		if (!mascotInfo[5].Equals("n"))
		{
			switch (int.Parse(mascotInfo[5]))
			{
				case 13:
					if (mascot.skill.GetType().GetMethod("Knockout").DeclaringType != typeof(Moona)) 
					{
						gameObject.GetComponent<Moona>().gameManager = gameManager;
						gameObject.GetComponent<Moona>().mascot = this;
						gameObject.GetComponent<Moona>().Knockout(excessDmg);
					}
					break;
			}
		}
	}
	
	public void FaintSummon()
	{
		SetSkillInfo();
		mascot.skill.FaintSummon();
		if (!mascotInfo[5].Equals("n"))
		{
			switch (int.Parse(mascotInfo[5]))
			{
				case 14:
					gameObject.GetComponent<AmeWatchSkill>().mascot = this;
					gameObject.GetComponent<AmeWatchSkill>().gameManager = gameManager;
					gameObject.GetComponent<AmeWatchSkill>().FaintSummon();
					break;
				case 16:
					gameObject.GetComponent<Iofi>().mascot = this;
					gameObject.GetComponent<Iofi>().gameManager = gameManager;
					gameObject.GetComponent<Iofi>().FaintSummon();
					break;
			}
		}
	}
	
	public void DealDmg()
    {
        SetSkillInfo();
        mascot.skill.DealDmg();
    }

    public void EndOfBattle()
    {
        SetSkillInfo();

        mascot.skill.EndOfBattle();
    }

    public void BeforeAtk() 
    {
        SetSkillInfo();

        mascot.skill.BeforeAtk();
    }

    public void AllySummoned(MascotDisplay ally)
    {
        SetSkillInfo();

        mascot.skill.AllySummoned(ally);
    }

    public void Sell()
    {
        SetSkillInfo();

        mascot.skill.Sell();
    }

	public void Buy()
	{
		SetSkillInfo();
		mascot.skill.Buy();
	}

	public void StartOfShop() 
    {
		SetSkillInfo();

		mascot.skill.StartOfShop();
		if (!mascotInfo[5].Equals("n"))
		{
			switch (int.Parse(mascotInfo[5]))
			{
                case 0:
                    gameObject.GetComponent<RoyalTeaSkill>().mascot = this;
					gameObject.GetComponent<RoyalTeaSkill>().StartOfShop();
					break;
				case 6:
					gameObject.GetComponent<DokurokunSkill>().gameManager = gameManager;
					gameObject.GetComponent<DokurokunSkill>().StartOfShop();
					break;
				case 17:
					StatsBuff(0, 1);
					break;
			}
		}
	}

    public void AllyLvlUp(MascotDisplay mascot) 
    {
		SetSkillInfo();
        this.mascot.skill.AllyLvlUp(mascot);
	}

	public void LvlUp()
	{
		SetSkillInfo();
		mascot.skill.LvlUp();		
	}

	public int StatBuffTime() 
    {
		SetSkillInfo();
        return mascot.skill.StatBuffTime();
	}

	public void ReceiveItem() 
    {
        SetSkillInfo();

        mascot.skill.ReceiveItem();
    }

    public bool ProtectAlly(MascotDisplay mascot) 
    {
		SetSkillInfo();
        return this.mascot.skill.ProtectAlly(mascot);
	}

	public void ShieldAlly(int dmg, MascotDisplay fromWho)
	{
		SetSkillInfo();
		mascot.skill.ShieldAlly(dmg, fromWho);
	}

    public int ReceiveDmg(int dmg) 
    {
		SetSkillInfo();
        int result = mascot.skill.ReceiveDmg(dmg);
		if (!mascotInfo[5].Equals("n"))
		{
			switch (int.Parse(mascotInfo[5]))
			{
				case -1:
					result -= 3;
					break;
				case 10:
                    result += 2;
					break;
                case 11:
					int value = int.Parse(hp.text) - dmg + result;
                    if (value <= 0) 
                    {
                        result += value + 1;
                        string[] allSlot = gameManager.GetAllSlot();
						string info = allSlot[startSlot];
						string[] newInfos = info.Split('_');
						newInfos[5] = "n";
						allSlot[startSlot] = gameManager.CreateNewInfo(newInfos);
                        gameManager.CreateNewInfo();
					}
					RemoveEffect();
					break;
			}
		}
        return result;
	}
}
