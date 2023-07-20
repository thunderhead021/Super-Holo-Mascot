using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public List<Sprite> sc;
    public TextMeshProUGUI number;
    public Image art;

    public void ChangeAmount(int coin) 
    {
        number.text = coin.ToString();
		gameObject.SetActive(true);
		switch (coin) 
        {
            case 0:
				gameObject.SetActive(false);
                break;
            case 1:
                art.sprite = sc[0];
                break;
			case 2:
				art.sprite = sc[1];
				break;
			case 3:
				art.sprite = sc[2];
				break;
			case 4:
				art.sprite = sc[3];
				break;
			case 5:
				art.sprite = sc[4];
				break;
			case 6:
				art.sprite = sc[5];
				break;
			default:
				art.sprite = sc[6];
				break;
		}
    }
}
