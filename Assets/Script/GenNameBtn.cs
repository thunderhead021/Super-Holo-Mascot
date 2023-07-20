using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GenNameBtn : MonoBehaviour, IPointerEnterHandler
{
	public GameManager gameManager;
	public Text text;
	public AudioClip hover;
	public AudioClip click;
	public AudioSource source;

	public void CreateName() 
	{
		gameManager.CreateName(text.text);
		source.clip = click;
		source.Play();
	}

	public void CreateAdjective()
	{	
		gameManager.CreateAdjective(text.text);
		source.clip = click;
		source.Play();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		source.clip = hover;
		source.Play();
	}
}
