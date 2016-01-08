using UnityEngine;
using System.Collections;

public class Bullet : UITrigger {

    public float Speed = 10;
    float Pos_x = 0;
    public bool Update_Bullet = false;

    private bool FacingRight = true;
    private int Dir = 1;

	// Use this for initialization
	void Start () {
        Pos_x = transform.localPosition.x;
	
	}

    private void Move_Bullet()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime * Dir);
    }
	
	// Update is called once per frame
	void Update () {
        Move_Bullet();
	
	}

    public void Creat_Bullet(bool _FacingRight)
    {
        FacingRight = _FacingRight;
        Debug.Log("FacingRight : " + FacingRight);
            if(!FacingRight)
                Dir = -1;
            else Dir = 1;

        Vector3 Scale = transform.localScale;

        Scale.z *= Dir;
        transform.localScale = Scale;

        Update_Bullet = true;
    }

}
