using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendData : MonoBehaviour
{
	public GameManager GameManager;
	private int curFeedbackType = 0;

	private string AIDataURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSddRSDmJqf6Mlln1HkJWuyQXfEPv6wZL3B9RxR4H2dHe_Ibbg/formResponse";
	private string AIDataEntry = "entry.1891441655";
	

	private string BugReportURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdU7YnDvzogJ20-8TogoBWYgdeAP62dat9TQcIh51o1WPkKgg/formResponse";
	private string BugReportDesEntry = "entry.929279188";
	private string BugReportActPreEntry = "entry.1299036458";
	private string BugReportExpResEntry = "entry.1514383448";
	private string BugReportActResEntry = "entry.861727076";
	[SerializeField]
	private InputField BugReportDes;
	[SerializeField]
	private InputField BugReportActPre;
	[SerializeField]
	private InputField BugReportExpRes;
	[SerializeField]
	private InputField BugReportActRes;
	[SerializeField]
	private GameObject BugReportBtn;


	private string FeedbackURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSesFmlBCZe_oG1BwsHxjg5O4Bn4xOdhcRccQafIf_jD3VUjTg/formResponse";
	private string FeedbackTitleEntry = "entry.1797152387";
	private string FeedbackEntry = "entry.181843888";
	[SerializeField]
	private InputField FeedbackTitle;
	[SerializeField]
	private InputField feedback;
	[SerializeField]
	private GameObject feedbackSendBtn;

	[SerializeField]
	private Image bg;
	[SerializeField]
	private Animator animator;

	public void SendFBData() 
	{
		StartCoroutine(checkInternetConnection((isConnected) => {
			if (isConnected)
			{
				StartCoroutine(PostFBData());
			}
			else 
			{
				FeedbackTitle.Select();
				FeedbackTitle.text = "";
				feedback.Select();
				feedback.text = "";
				animator.SetTrigger("Internet");
			}
		}));
	}

	IEnumerator PostFBData()
	{
		WWWForm form = new WWWForm();
		form.AddField(FeedbackTitleEntry, FeedbackTitle.text);
		form.AddField(FeedbackEntry, feedback.text);
		UnityWebRequest www = UnityWebRequest.Post(FeedbackURL, form);
		yield return www.SendWebRequest();
		FeedbackTitle.Select();
		FeedbackTitle.text = "";
		feedback.Select();
		feedback.text = "";
		animator.SetTrigger("Submit");
	}

	public void SendBRData()
	{
		StartCoroutine(checkInternetConnection((isConnected) => {
			if (isConnected)
			{
				StartCoroutine(PostBRData());
			}
			else
			{
				BugReportDes.Select();
				BugReportDes.text = "";
				BugReportActPre.Select();
				BugReportActPre.text = "";
				BugReportExpRes.Select();
				BugReportExpRes.text = "";
				BugReportActRes.Select();
				BugReportActRes.text = "";
				animator.SetTrigger("Internet");
			}
		}));
	}

	IEnumerator PostBRData()
	{
		WWWForm form = new WWWForm();
		form.AddField(BugReportDesEntry, BugReportDes.text);
		form.AddField(BugReportActPreEntry, BugReportActPre.text);
		form.AddField(BugReportExpResEntry, BugReportExpRes.text);
		form.AddField(BugReportActResEntry, BugReportActRes.text);
		UnityWebRequest www = UnityWebRequest.Post(BugReportURL, form);
		yield return www.SendWebRequest();
		BugReportDes.Select();
		BugReportDes.text = "";
		BugReportActPre.Select();
		BugReportActPre.text = "";
		BugReportExpRes.Select();
		BugReportExpRes.text = "";
		BugReportActRes.Select();
		BugReportActRes.text = "";
		animator.SetTrigger("Submit");
	}

	public void SendAIData(string data, bool disableGO = true) 
	{
		StartCoroutine(checkInternetConnection((isConnected) => {
			if (isConnected)
			{
				StartCoroutine(PostAIData(data, disableGO));
			}
		}));
	}

	IEnumerator checkInternetConnection(Action<bool> action)
	{
#pragma warning disable CS0618 // Type or member is obsolete
		WWW www = new WWW("http://google.com");
#pragma warning restore CS0618 // Type or member is obsolete
		yield return www;
		if (www.error != null)
		{
			action(false);
		}
		else
		{
			action(true);
		}
	}

	IEnumerator PostAIData(string data, bool disableGO) 
	{
		WWWForm form = new WWWForm();
		form.AddField(AIDataEntry, data);

		UnityWebRequest www = UnityWebRequest.Post(AIDataURL, form);
		yield return www.SendWebRequest();
		if(disableGO)
			gameObject.SetActive(false);
		GameManager.FinishSendData = true;
	}

	public void DropdownValueChanged(Dropdown change)
	{
		if (change.value == 0)
		{
			animator.SetTrigger("FB");
		}
		else
		{
			animator.SetTrigger("BR");
		}
		curFeedbackType = change.value;
	}

	public void CloseForm() 
	{
		gameObject.SetActive(false);
		bg.fillAmount = 0.35f;
	}

	public void CheckRequireFieldFB(string str) 
	{
		if (str.Equals("") || feedback.text.Equals("") || FeedbackTitle.text.Equals(""))
		{
			feedbackSendBtn.SetActive(false);
		}
		else if(!feedback.text.Equals("") && !FeedbackTitle.text.Equals(""))
		{
			feedbackSendBtn.SetActive(true);
		}
	}

	public void CheckRequireFieldBR(string str)
	{
		if (str.Equals("") || BugReportDes.text.Equals("") || BugReportActPre.text.Equals("") || BugReportExpRes.text.Equals("") 
			|| BugReportActRes.text.Equals(""))
		{
			BugReportBtn.SetActive(false);
		}
		else if (!BugReportDes.text.Equals("") && !BugReportActPre.text.Equals("") && !BugReportExpRes.text.Equals("") && !BugReportActRes.text.Equals(""))
		{
			BugReportBtn.SetActive(true);
		}
	}
}
