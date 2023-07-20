using UnityEngine;

public class Hyperlinks : MonoBehaviour
{
    public string link;

	public void OpenUrl()
	{
		Application.OpenURL(link);
	}
}
