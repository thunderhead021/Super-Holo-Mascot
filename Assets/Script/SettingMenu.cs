using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Video;
using YoutubePlayer;
using Button = UnityEngine.UI.Button;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer master;
    public Dropdown resList;
    private GameObject bgm;
    public GameObject errorText;
    private Resolution[] resolutions;
    public GameObject settingMenu;
    public Button[] btn;
	public GameManager gameManager;
    public Slider slider;
    public Toggle toggle;
    public InputField inputField;
	[SerializeField]
	private GameObject tips;
	[HideInInspector]
	public bool isConnected = false;
    private bool fullscr = false;
    private string bgmLink = "";
    private float timeScale = 0;
	private float volume = 0;

	private void Awake()
    {
		bgm = GameObject.Find("BGM");
		ReturnSetting();
		timeScale = Time.timeScale;
        Time.timeScale = 0;
        

		errorText.SetActive(false);
	}

    public void SetAudio(float level) 
    {
		master.SetFloat("volume", level);
        volume = level;
	}

    public void SetFullscreen(bool isFull) 
    {
        Screen.fullScreen = isFull;
        fullscr = isFull;
	}

    public void SetRes(int index) 
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void CheckLink(string link) 
    {
		Regex r = new Regex(@"^(http(s)??\:\/\/)?(www\.)?((youtube\.com\/watch\?v=)|(youtu.be\/))([a-zA-Z0-9\-_])+");
		if (!r.IsMatch(link))
		{
            bgmLink = "";
			errorText.SetActive(true);
		}
        else 
        {
            bgmLink = link;
			errorText.SetActive(false);
		}
		if (link.Equals(""))
			errorText.SetActive(false);
	}

    public void SetBGM() 
    {
		gameManager.CheckInternet();
		if (!isConnected)
		{
			bgm.GetComponent<VideoPlayer>().Stop();
			bgm.GetComponent<PlayMusic>().Play();
		}
		else
		{
			if (bgmLink.Equals(""))
			{
				bgm.GetComponent<VideoPlayer>().Stop();
				bgm.GetComponent<PlayMusic>().Play();
			}
			else
			{
				bgm.GetComponent<VideoPlayer>().PlayYoutubeVideoAsync(bgmLink);
				bgm.GetComponent<PlayMusic>().Stop();
			}
		}	
	}

    public void ReturnSetting() 
    {
		bgm = GameObject.Find("BGM");
		Screen.fullScreen = PlayerPrefs.GetInt("Fullscreen") == 1;
        toggle.isOn = Screen.fullScreen;

		Screen.SetResolution(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"), Screen.fullScreen);
		resolutions = Screen.resolutions;
		resList.ClearOptions();
		List<string> resOpt = new List<string>();
		int curRes = 0;
		int i = 0;
		foreach (Resolution r in resolutions)
		{
			resOpt.Add(r.width + "x" + r.height);
			if (r.width == (PlayerPrefs.GetInt("Width") != 0 ? PlayerPrefs.GetInt("Width") : Screen.currentResolution.width) && r.height ==
				(PlayerPrefs.GetInt("Height") != 0 ? PlayerPrefs.GetInt("Height") : Screen.currentResolution.height))
				curRes = i;
			i++;
		}
		resList.AddOptions(resOpt);
		resList.value = curRes;
		resList.RefreshShownValue();

		master.SetFloat("volume", PlayerPrefs.GetFloat("Volume"));
        slider.value = PlayerPrefs.GetFloat("Volume");

		bgmLink = PlayerPrefs.GetString("Link");
		inputField.text = bgmLink;

		SetBGM();
	}

	public void ReturnToGame() 
    {
        ReturnSetting();

		Time.timeScale = timeScale;
        settingMenu.SetActive(false);
		gameManager.settingPause = false;
		foreach (Button button in btn)
		{
			button.interactable = true;
		}
	}

	public void SaveSetting()
	{
		PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("Fullscreen", fullscr ? 1 : 0);
        PlayerPrefs.SetInt("Width", Screen.width);
		PlayerPrefs.SetInt("Height", Screen.height);
        PlayerPrefs.SetString("Link", bgmLink);
		SetBGM();
		Time.timeScale = timeScale;
		settingMenu.SetActive(false);
		gameManager.settingPause = false;
		foreach (Button button in btn)
		{
			button.interactable = true;
		}
	}

	public void ShowSetting() 
    {
		settingMenu.SetActive(true);
		if (PlayerPrefs.GetInt("FirstTimeSetting") == 1) 
		{
			tips.SetActive(true);
		}
		gameManager.settingPause = true;
        foreach (Button button in btn) 
        {
			button.interactable = false;
		}	
	}

	public void CloseTip() 
	{
		tips.SetActive(false);
		PlayerPrefs.SetInt("FirstTimeSetting", 0);
	}
}