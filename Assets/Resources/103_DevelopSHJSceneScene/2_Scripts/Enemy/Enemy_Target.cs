using UnityEngine;
using System.Collections;

public class Enemy_Target : MonoBehaviour {

    public GameObject player = null;
    public GameObject enemy = null;

    public float enemy_speed = 0.4f;

    public bool target = false;
    public bool attack = false;
    private float player_x, enemy_x;

	// Use this for initialization
	void Start () {
        StartCoroutine(UpdateSpeed());
        enemy_x = enemy.transform.localPosition.x;

	}

    IEnumerator UpdateSpeed()
    {
        while(true)
        {
            player_x = player.transform.localPosition.x;
            if (target == true)
            {
                if (enemy_x > (player_x + 0.8f))
                {
                    if (enemy_x - 0.1f < (player_x + 0.8f))
                    {
                        attack = true;
                    }
                    else attack = false;
                    enemy.transform.localPosition = new Vector3(enemy_x -= enemy_speed * Time.deltaTime, enemy.transform.localPosition.y, enemy.transform.localPosition.z);

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
    }

    void OnTriggerExit(Collider col)
    {

    }
}
