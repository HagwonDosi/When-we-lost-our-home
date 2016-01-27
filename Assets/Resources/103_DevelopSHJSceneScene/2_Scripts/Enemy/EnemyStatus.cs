using UnityEngine;
using System.Collections;

public class EnemyStatus : Status
{
    public float mMaxHealth = 400f;
    
    public InteractionTrigger mITrigger = null;

	// Use this for initialization
	void Start ()
    {
        AddStatus("Health", new StatMinMax(mMaxHealth, 0f, mMaxHealth));
	}

    public float HP
    {
        get
        {
            return GetStatus("Health");
        }
    }

    public void AddHP(float fVal)
    {
        AddValStatus("Health", fVal);
    }
}
