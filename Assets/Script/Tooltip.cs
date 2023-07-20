using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public string tooltip;
	public GameManager gameManager;
	public TooltipSType type;
	public Shop shop;

	private static LTDescr delay;
	public void OnPointerEnter(PointerEventData eventData)
	{
		delay = LeanTween.delayedCall(0.5f, () => {
			gameManager.description.SetActive(true);
			string description = "";
			
			switch (type) 
			{
				case TooltipSType.RefreshBtn:
					if (shop.GetAllMascotInFreezer() + shop.GetAllItemInFreezer() == 7)
					{
						description = "Can't refresh with everything frozen";
						gameManager.description.GetComponent<Description>().Tooltips(description);
					}
					else if (gameManager.coin < 1)
					{
						description = "Not enough SC";
						gameManager.description.GetComponent<Description>().Tooltips(description);
					}
					else 
					{
						description = tooltip;
						string cost = "cost:";
						cost += gameManager.freeRefreshTimes > 0 ? "free" : "1 SC";
						gameManager.description.GetComponent<Description>().Tooltips(description, cost);
					}			
					break;
				case TooltipSType.BattleBtn:
					description = gameManager.HaveMascot() ? tooltip : "Need at least 1 mascot to start the battle";
					gameManager.description.GetComponent<Description>().Tooltips(description);
					break;
				case TooltipSType.AnythingElse:
					gameManager.description.GetComponent<Description>().Tooltips(tooltip);
					break;
			}
		});
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		LeanTween.cancel(delay.uniqueId);
		if (gameManager.description != null) 
		{
			gameManager.description.SetActive(false);
			gameManager.description.GetComponent<Description>().Reset();
		}
	}

}

public enum TooltipSType 
{
	RefreshBtn,
	BattleBtn,
	AnythingElse
}
