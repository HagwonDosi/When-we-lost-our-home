using UnityEngine;
using System.Collections;

public class Enemy_State : MonoBehaviour {

    public float eHP = 400f;
    private float eAttack = -10f;
    public BoxCollider Box = null;
    public PlayerStatus player_status;

    private bool Player_Text_Chek;
    public InteractionTrigger mSprite = null;
    private bool enemy_target_chek = false;
    private Enemy_Target enemy_target;

	// Use this for initialization
	void Start () {
        StartCoroutine(UpdateStatus());
	}

    public float HP
    {
        get
        {
            return eHP;
        }
    }

    public void AddHP(float col)
    {
        eHP += col;

        if(eHP <0)
            eHP = 0;
    }

    public void Attack()
    {
        player_status.AddHP(eAttack);
    }

    IEnumerator UpdateStatus()
    {
        while(true)
        {
            //enemy_target_chek = enemy_target.Attack_chek;
            if(eHP <= 0)
            {
                Player_Text_Chek = true;
                Destroy(gameObject);
                mSprite.InteractionTriggerExit();
            }
            

            yield return null;
        }
    }

    public bool player_Text_Chek
    {
        get
        {
            return Player_Text_Chek;
        }
    }

    void Mon_Attack()
    {
        Box.enabled = true;
    }

    void Mon_Attack_End()
    {
        Box.enabled = false;
    }

    void OnTriggerEnter (Collider col)
    {
        if(col.transform.tag == "Player")
        {
            Debug.Log("Attack");
            Attack();
        }
    }
}
