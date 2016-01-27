using UnityEngine;
using System.Collections;

public class Destory : MonoBehaviour {

    public GameObject Destory_Object = null;
    public Popinven mPop = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Destoryed()
    {
        UIDirector.Instance.SetEnabledUILayer(0, true);
        mPop.StopAllCoroutines();
        Destory_Object.transform.localPosition = new Vector3(10000, 10000, 10000);
    }
}
