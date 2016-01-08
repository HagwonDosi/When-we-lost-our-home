using UnityEngine;
using System.Collections;

public class Player_Gun : MonoBehaviour {

    public GameObject Gun = null;
    public GameObject Gun_Bullet = null;
    public GameObject Gun_pool = null;



	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Gun_Fire()
    {

        bool Facin = true;
        if (transform.localEulerAngles.y < 180.0f)
            Facin = true;
        else if (transform.localEulerAngles.y > 180.0f) Facin = false;



        GameObject gun_bullet = (GameObject)Instantiate(Gun_Bullet, Gun.transform.position, Quaternion.identity);

        gun_bullet.transform.parent = Gun.transform;
        gun_bullet.transform.localPosition = Vector3.zero;
        gun_bullet.transform.parent = Gun_pool.transform;
        gun_bullet.GetComponent<Bullet>().Creat_Bullet(Facin);


    }
    
}
