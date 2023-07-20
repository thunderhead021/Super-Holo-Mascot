using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [HideInInspector]
    public Mascot mascot;
	[HideInInspector]
	public Item item;
	[SerializeField]
	private AudioSource _audioSource;
	[SerializeField]
	private AudioClip _audio;
	public void ChangeImage() 
    {
        if(mascot != null)
            image.sprite = mascot.opoArt;
        else if(item != null)
			image.sprite = item.art;
	}

    public void ShowInfo() 
    {
		Book book = GameObject.Find("Book").GetComponent<Book>();
		if (mascot != null)
			book.ShowMascotDetail(mascot);
		else if (item != null)
			book.ShowItemDetail(item);
		if (_audioSource.isPlaying) return;
		_audioSource.clip = _audio;
		_audioSource.Play();
	}
}
