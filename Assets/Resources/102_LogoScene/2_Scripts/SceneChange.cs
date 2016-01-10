using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour {

    public Animator Anim;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}   

    void NextScene()
    {
        Application.LoadLevel(3);
    }
}
