using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameManager
{
    public List<string> fanName;
	public List<string> adjective;
	public Text[] selectAdjective;
	public Text[] selectFanName;
    public Text[] showPlayerName;
	public Text showOpoName;
	public GameObject confirmBtn;
	public AudioClip clickaudio;
	private string playerAdjective = "";
    private string playerFanName = "";
    private bool chargedForward = false;

    private void GenText(Text[] selectArray, List<string> textArray) 
    {
        List<string> selectText = new List<string>();
        foreach (Text text in selectArray) 
        {
            string newText = textArray[Random.Range(0, textArray.Count)];
            while(selectText.Contains(newText))
				newText = textArray[Random.Range(0, textArray.Count)];
            selectText.Add(newText);
            text.text = newText;
		}
    }

    private void ShowName() 
    {
        foreach (Text text in showPlayerName) 
        {
            text.text = "The " + playerAdjective + " " + playerFanName;
        }
        if (!playerAdjective.Equals("") && !playerFanName.Equals(""))
            confirmBtn.SetActive(true);

	}

    public void CreateName(string name) 
    {
        playerFanName = name;
        ShowName();
	}

	public void CreateAdjective(string adj)
	{
		playerAdjective = adj;
		ShowName();
	}

    public void SetPlayerName() 
    {
        playerName = "The " + playerAdjective + " " + playerFanName;
		StartCoroutine(LerpFunction(false));
        if (chargedForward)
        {
            march.Play();
            descriptionCamvas.SetActive(false);
            StartCoroutine(StartBattle());
        }
        else 
        {
			GetComponent<AudioSource>().clip = clickaudio;
			GetComponent<AudioSource>().Play();

		}
	}

    private void GenAName() 
    {
		showOpoName.text =  "The " + adjective[Random.Range(0, adjective.Count)] + " " + fanName[Random.Range(0, fanName.Count)];
	}
}