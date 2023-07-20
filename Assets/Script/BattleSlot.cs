using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSlot : MonoBehaviour
{
    public GameObject shadow;
    private bool started = false;
    private Vector2 targetPos;
    private Vector2 currentPos;
	private float lerpDuration = 0.5f;
	// Start is called before the first frame update
	void Start()
    {
        StartCoroutine(GetPos());
	}

    // Update is called once per frame
    void Update()
    {       
		targetPos = shadow.GetComponent<RectTransform>().anchoredPosition;
		if (started && currentPos != targetPos) 
        {
			StartCoroutine(LerpFunction());
		}
    }

	IEnumerator LerpFunction()
	{
		float time = 0;
		while (time < lerpDuration)
		{
			GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(currentPos, targetPos, time / lerpDuration);
			time += Time.deltaTime;
			yield return null;
		}
		currentPos = targetPos;
		GetComponent<RectTransform>().anchoredPosition = currentPos;
	}

	IEnumerator GetPos() 
    {
		yield return new WaitForSeconds(2);
        targetPos = shadow.GetComponent<RectTransform>().anchoredPosition;
		currentPos = GetComponent<RectTransform>().anchoredPosition;
        started = true;
	}

    public void Death() 
    {
        gameObject.SetActive(false);
        shadow.SetActive(false);
    }

	public void Alive()
	{
		gameObject.SetActive(true);
		shadow.SetActive(true);
	}
}
