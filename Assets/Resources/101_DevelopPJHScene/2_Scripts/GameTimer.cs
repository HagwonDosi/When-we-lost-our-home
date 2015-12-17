using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public float mSecPerDay = 60f;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
    IEnumerator UpdateTime()
    {
        while(true)
        {


            yield return null;
        }
    }
}
