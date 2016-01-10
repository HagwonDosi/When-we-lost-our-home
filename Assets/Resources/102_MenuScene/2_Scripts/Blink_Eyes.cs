
using UnityEngine;
using System.Collections;

public class Blink_Eyes : MonoBehaviour
{
    public GameObject eyelid_Up;
    public GameObject eyelid_Down;

    float f_Delay = 3.0f;
    float time_1;

    bool check = false;

	// Use this for initialization
	void Start ()
    {
        time_1 = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (eyelid_Down.transform.localPosition.y >= -250)
        {
            check = true;
            Debug.Log("Open1");
        }
        if (eyelid_Down.transform.localPosition.y <= -500)
        {
            check = false;
            Debug.Log("Close1");
        }

        if (Time.time - time_1 >= f_Delay && check == false) 
        {
            eyelid_Down.transform.localPosition = new Vector3(eyelid_Down.transform.localPosition.x,
                                                              eyelid_Down.transform.localPosition.y + 30,
                                                              eyelid_Down.transform.localPosition.z);
            eyelid_Up.transform.localPosition = new Vector3(eyelid_Up.transform.localPosition.x,
                                                              eyelid_Up.transform.localPosition.y - 30,
                                                              eyelid_Up.transform.localPosition.z);
            Debug.Log("Close");
        }

        if(check == true)
        {
            eyelid_Down.transform.localPosition = new Vector3(eyelid_Down.transform.localPosition.x,
                                                              eyelid_Down.transform.localPosition.y - 30,
                                                              eyelid_Down.transform.localPosition.z);
            eyelid_Up.transform.localPosition = new Vector3(eyelid_Up.transform.localPosition.x,
                                                              eyelid_Up.transform.localPosition.y + 30,
                                                              eyelid_Up.transform.localPosition.z);
            Debug.Log("Open");
            time_1 = Time.time;
        }
	}
}
