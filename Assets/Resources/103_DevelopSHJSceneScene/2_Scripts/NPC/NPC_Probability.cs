using UnityEngine;
using System.Collections;

public class NPC_Probability : MonoBehaviour //NPC의 랜덤확률
{

    private static NPC_Probability rand = null;

    public static NPC_Probability Randomes
    {
        get
        {
            if (rand == null)
                rand = GameObject.FindObjectOfType<NPC_Probability>();

            return rand;
        }
    }

    private int Rand_txt = 0;// 랜덤으로 대화를 하냐 거래를하냐 
    private int Rand_go = 0; // 랜덤으로 방문시기

    public int Rand_TxT
    {
        get
        {
            return Rand_txt;
        }
    }

    public int Rand_GO
    {
        get
        {
            return Rand_go;
        }
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(UpdateTime());
	
	}

    IEnumerator UpdateTime()
    {
        while (true)
        {
            Rand_txt = Random.Range(1, 10);
            Rand_go = Random.Range(2, 4);
            yield return null;
        }
    }
}
