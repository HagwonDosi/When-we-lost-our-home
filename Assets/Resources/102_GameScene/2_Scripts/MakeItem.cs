using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeItem : MonoBehaviour
{

    private List<string> items = new List<string>();

    public GameObject[] Item = null;
    public GameObject[] NPC = null;

    public GameObject Pop_Trade = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pop_trade()
    {
        Pop_Trade.transform.localPosition = new Vector3(0, 0, 0);
        //GameObject PoP_Trade = (GameObject)Instantiate(Pop_Trade, transform.localPosition,transform.localRotation);
    }

}