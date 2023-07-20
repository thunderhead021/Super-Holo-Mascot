public class KaelasforgeSkill : ItemTriggerBase
{
    public override void Multiple()
    {
        gameManager.shop.BuffAll();
		gameManager.AddBuff();
    }
}
