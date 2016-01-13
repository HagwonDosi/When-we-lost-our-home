using UnityEngine;
using System.Collections;


public class Player_Text : MonoBehaviour {

    public TextMesh player_Text = null;
    public GameObject player = null;
    public PlayerStatus Hp;
    public float fTime = 2.0f;
    public float fChek = 0.0f;
    public bool color_Chek = false;
    public Enemy_State Enemy;
    public ConvTestTrigger con;

    Vector3 Pos_;
    int count = 0;
    
    public float player_Hp;
    private string Player_Texteur;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        Pos_ = player.transform.localPosition;
        player_Text.transform.localPosition = new Vector3(Pos_.x, Pos_.y + 0.72f, transform.localPosition.z);
        player_Hp = Hp.HP;
        if (count == 0)
        {
            color_Chek = Enemy.player_Text_Chek;
        }
        if (color_Chek == true)
        {
            count = 1;
            if (player_Hp >= 80)
            {
                Player_Texteur = "별거 아니구만";
            }
            else if (player_Hp > 30 && player_Hp <= 50)
            {
                Player_Texteur = "조금 힘들었어";
            }
            else if (player_Hp <= 30)
            {
                Player_Texteur = "하마터면 죽을 뻔 했어";
            }

            player_Text.text = Player_Texteur;
            fChek += Time.deltaTime;
            float _fAmount = Mathf.Lerp(1.0f, 0.0f, fChek / fTime);
            player_Text.color = new Color(player_Text.color.r, player_Text.color.g, player_Text.color.b, _fAmount);
            if (fChek >= fTime)
            {
                fChek = 0.0f;
                color_Chek = false;
            }
        }


    }

}
