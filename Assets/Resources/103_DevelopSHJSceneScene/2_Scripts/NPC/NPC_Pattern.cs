using UnityEngine;
using System.Collections;

public class NPC_Pattern : MonoBehaviour // NPC의 패턴
{


	// Use this for initialization
	void Start () {
        StartCoroutine(UpdateTime());
	
	}

    IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return null;
        }
    }
}
