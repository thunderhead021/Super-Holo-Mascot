using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class distanceAniCall : MonoBehaviour
{
    [HideInInspector]
    public bool aniFinished = false;
    public AudioClip hit;
    public AudioSource source;
    private bool isHit = false;
    public void SetUpData(bool hit = false) 
    {
		aniFinished = false;
        isHit = hit;
	}

    public void AniFinish() 
    {
        if (isHit)
        {
			source.clip = hit;
            source.Play();
		}
        aniFinished = true;
		gameObject.GetComponent<Image>().fillAmount = 0;
		gameObject.SetActive(false);        
    }
}
