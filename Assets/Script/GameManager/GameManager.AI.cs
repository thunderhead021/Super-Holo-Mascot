using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
	private static List<string> data = new List<string>();
	private string[] GetAnOpoForRound(int round)
	{
		if (aiData.Count < round)
		{
			return aiData[aiData.Count - 1].Replace(" @@@ ", "\n").Split('\n')[0].Split('/');
		}
		string[] allVar = aiData[round - 1].Replace(" @@@ ", "\n").Split('\n');
		return allVar[allVar.Length == 1 ? 0 : Random.Range(0, allVar.Length)].Split('/');
	}

	//remove this
	private void Add2Data(string rawdata) 
	{
		data.Add(rawdata);
	}
}
