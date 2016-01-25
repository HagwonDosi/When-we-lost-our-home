using UnityEngine;
using System.Collections;

public class Flamethrower : MonoBehaviour
{
    public GameObject Fire;
    public GameObject Smoke;
    public GameObject c_light1;
    public GameObject c_light2;
    ParticleEmitter Emit;
    ParticleEmitter Firespeed;
    Light light1;
    Light light2;
    float ftime;
	// Use this for initialization
	void Start ()
    {
        ftime = Time.time;
        Emit = Smoke.GetComponent<ParticleEmitter>();
        Firespeed = Fire.GetComponent<ParticleEmitter>();
        light1 = c_light1.GetComponent<Light>();
        light2 = c_light2.GetComponent<Light>();

        Emit.emit = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Time.time - ftime > 0.5f)
        {
            Emit.emit = true;
        }
        if (Time.time - ftime <= 0.5)
            light1.intensity = Time.time - ftime;

        if (Time.time - ftime >= 0.5f && Time.time - ftime <= 1.5f)
            light2.intensity = Time.time - ftime - 0.5f;
	}
}