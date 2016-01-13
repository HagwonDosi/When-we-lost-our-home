using UnityEngine;
using System.Collections;

public class Enemy_Target : MonoBehaviour
{
    public GameObject player = null;
    public GameObject enemy = null;
    private Animator _animator;

    float enemy_speed;

    public float Gun_Bullet = 80;
    public bool target = false;
    public bool Gun = false;
    public Enemy_State eState;
    public bool attack = false;
    
    private float player_x, enemy_x;

	// Use this for initialization
	void Start () {
        StartCoroutine(UpdateSpeed());
        enemy_x = enemy.transform.localPosition.x;
        _animator = gameObject.GetComponent<Animator>();

	}

    IEnumerator UpdateSpeed()
    {
        while (true)
        {
            player_x = player.transform.localPosition.x;

            if (target == true)
            {
                enemy_speed = 0.4f;
                if (enemy_x > (player_x + 0.8f))
                {
                    _animator.SetBool("M_Walk", true);
                    _animator.SetBool("Monster_Attack_chek", false);

                    enemy.transform.localPosition = new Vector3(enemy_x -= enemy_speed * Time.deltaTime, enemy.transform.localPosition.y, enemy.transform.localPosition.z);//타겟안에 들어오면 플레이어쪽으로 감
                    if (Gun == true)
                    {
                        enemy.transform.localPosition = new Vector3(enemy_x += 0.05f, enemy.transform.localPosition.y, enemy.transform.localPosition.z);//총에 맞으면 뒤로 밀림
                        Gun = false;
                    }
                   
                    
                }
                if (enemy_x - 0.1f < (player_x + 0.8f))
                {
                    enemy_speed = 0;
                    attack = true;
                    if (attack == true)
                    {
                        _animator.SetBool("M_Walk", false);
                        _animator.SetBool("Monster_Attack_chek", true);
                        _animator.speed = 0.5f; // 애니메이션 스피드 변경
                        if (Gun == true)
                        {
                            enemy.transform.localPosition = new Vector3(enemy_x += 0.05f, enemy.transform.localPosition.y, enemy.transform.localPosition.z);//총에 맞으면 뒤로 밀림
                            Gun = false;
                        }
                        //공격에니메이션 적용
                    }
                }
                
                
                if (enemy_x < (player_x - 0.8f))
                {
                    _animator.SetBool("M_Walk", true);
                    _animator.SetBool("Monster_Attack_chek", false);

                    enemy.transform.localPosition = new Vector3(enemy_x += enemy_speed * Time.deltaTime, enemy.transform.localPosition.y, enemy.transform.localPosition.z);//타겟안에 들어오면 플레이어쪽으로 감

                    if (Gun == true)
                    {
                        enemy.transform.localPosition = new Vector3(enemy_x -= 0.05f, enemy.transform.localPosition.y, enemy.transform.localPosition.z);//총에 맞으면 뒤로 밀림
                        Gun = false;
                    }
                    if (enemy_x + 0.1f > (player_x - 0.8f))
                    {
                        attack = true;
                        if (attack == true)
                        {
                            //공격에니메이션 적용
                            _animator.SetBool("M_Walk", false);
                            _animator.SetBool("Monster_Attack_chek", true);
                            _animator.speed = 0.5f;
                            if (Gun == true)
                            {
                                enemy.transform.localPosition = new Vector3(enemy_x += 0.05f, enemy.transform.localPosition.y, enemy.transform.localPosition.z);//총에 맞으면 뒤로 밀림
                                Gun = false;
                            }
                        }
                    }
                   
                }
            }

            yield return null;
        }
    }

    void Attack_End()
    {
        _animator.speed = 1.0f;
        attack = false;
    }

    public bool Attack_chek
    {
        get
        {
            return attack;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Target")
        {
            target = true;
        }
        if(col.gameObject.tag == "Gun")
        {
            eState.AddHP(-Gun_Bullet);
            Gun = true;
            Destroy(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {

    }
}
