using UnityEngine;
using System.Collections;

public class Enemy_Target : MonoBehaviour {

    public GameObject player = null;
    public GameObject enemy = null;

    public float enemy_speed = 0.4f;

    public bool target = false;
    public bool Gun = false;
    public bool attack = false;
    private float player_x, enemy_x;

	// Use this for initialization
	void Start () {
        StartCoroutine(UpdateSpeed());
        enemy_x = enemy.transform.localPosition.x;

	}

    IEnumerator UpdateSpeed()
    {
        while (true)
        {
            player_x = player.transform.localPosition.x;

            if (target == true)
            {
                if (enemy_x > (player_x + 0.8f))
                {
                    if (Gun == true)
                    {
                        enemy.transform.localPosition = new Vector3(enemy_x += 2.0f, enemy.transform.localPosition.y, enemy.transform.localPosition.z);//총에 맞으면 뒤로 밀림
                    }
                    if (enemy_x - 0.1f < (player_x + 0.8f))
                    {
                        attack = true;
                        if (attack == true)
                        {
                            //공격에니메이션 적용
                        }
                    }
                    else attack = false;
                    enemy.transform.localPosition = new Vector3(enemy_x -= enemy_speed * Time.deltaTime, enemy.transform.localPosition.y, enemy.transform.localPosition.z);//타겟안에 들어오면 플레이어쪽으로 감

                }
            }

            yield return null;
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
            Gun = true;
        }
    }

    void OnTriggerExit(Collider col)
    {

    }
}
