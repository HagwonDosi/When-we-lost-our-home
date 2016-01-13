using UnityEngine;
using System.Collections;

public class Playe_Animation : MonoBehaviour {

    public Animator player = null;
    public bool Target = false;
    public Player_Gun player_Chek = null;

    public Enemy_State enemy;

    public float FireTime = 0.3f;
    private float ChekingTime = 0.0f;

	// Use this for initialization
	void Start () {
        ChekingTime = FireTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Target == true)
        {
            player.SetBool("Player_Monster", true);
        }
        if (enemy.player_Text_Chek == true)
        {
            Target = false; 
            player.SetBool("Player_Monster", false);
        }
	
        /*if(transform.Find("mon@stand").GetComponent<Enemy_State>().player_status == true)
        {
            player.SetBool("Player_Monster", false);
        }*/

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Enemy")
        {
            Target = true;
            
        }
        
    }

    public void Attack_Animation()
    {
        if (Time.time - ChekingTime > FireTime)
        {
            player.SetBool("Player_Gun", true);
            ChekingTime = Time.time;
        }
    }
    public void Attack_Animation_End()
    {
        player.SetBool("Player_Gun", false);
    }
}
