using UnityEngine;
using System.Collections;

public class NPC_Move : MonoBehaviour // NPC의 움직임
{
    public GameObject NPC = null;
    public float Speed = 10;
    float pos_x = 0;
    public bool Chek = false;
    

    private NPC_Probability rand = null;

    private GameTimer time = null;
    

    // Use this for initialization
    void Start()
    {
        rand = NPC_Probability.Randomes;
        time = GameTimer.Instance;

        StartCoroutine(UpdateStatus());
    }

    int Rand_go; 

    IEnumerator UpdateStatus()
    {
        Rand_go = rand.Rand_GO;
        int SDay = time.Day;
        float STime = time.Hour;
        while (true)
        {
            if (time.getTimeGap(SDay, STime) >= 1.0f)
            {
                Debug.Log("SDay : ");
                Debug.Log(time.Day);
                SDay = time.Day;
                STime = time.Hour;
            }
            pos_x = transform.localPosition.x;

            if (Rand_go <= SDay && Chek == false)
            {
                Debug.Log("Rand_go : ");
                Debug.Log(Rand_go);
                transform.localPosition = new Vector3(pos_x += Speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
            }
            yield return null;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Chek = true;
        Rand_go += rand.Rand_GO;
        Debug.Log("----------------------------------");
        Debug.Log(Rand_go);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }
}
