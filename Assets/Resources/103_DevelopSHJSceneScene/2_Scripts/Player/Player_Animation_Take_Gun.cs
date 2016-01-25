using UnityEngine;
using System.Collections;

public class Player_Animation_Take_Gun : MonoBehaviour
{

    public Animator Player = null;
    public GameObject Gun = null;
    public GameObject Move_Parnt = null;
    public GameObject Right_Shoulder = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Take_Gun()
    {
        Gun.transform.parent = Move_Parnt.transform;
        Gun.transform.localPosition = new Vector3(-0.2232447f, -0.1490969f, 0.002845751f);
        Gun.transform.localEulerAngles = new Vector3(43.39853f, -90.05676f, -90.08264f);


    }

    void End_Gun()
    {
        Gun.transform.parent = Right_Shoulder.transform;
        Gun.transform.localPosition = new Vector3(0.04803576f, 0.3793242f, 0.2160557f);
        Gun.transform.localEulerAngles = new Vector3(-24.42001f, -1.788439e-12f, -180f);

    }

    void EndAttack()
    {
        Player.SetBool("Player_Attack", false);
    }
}
