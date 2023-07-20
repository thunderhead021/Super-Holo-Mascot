using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager
{
	[HideInInspector]
	public bool FinishSendData = false;
    public void PlayGame() 
    {
		inGame = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Return2MainMenu(bool sendData)
	{
		StartCoroutine(Return2MainMenuHelper(sendData));
	}

	IEnumerator Return2MainMenuHelper(bool sendData) 
	{
		inGame = false;
		if (sendData)
		{
			SendRoundData();
			while (!FinishSendData)
				yield return new WaitForSeconds(0.1f);
			FinishSendData = false;
		}
		playerName = "";
		SceneManager.LoadScene(0);
		Time.timeScale = 1;
	}

	public void sendData() 
	{
		SendRoundData();
	}

	public void QuitGame() 
	{
		Debug.Log("QUIT!!!!");
		Application.Quit();
	}
}
