using UnityEngine;
using System.Collections;

public class StairTrigger : UITrigger
{
    public CollisionTrigger mCTrigger = null;
    public GameObject mPlayer = null;

    public override void Act()
    {
        Stair stair = null;
        try
        {
            stair = mCTrigger.ColliderObject.GetComponent<InteractionControl>().mSubject.GetComponent<Stair>();
        }
        catch
        {
            Debug.LogWarning(gameObject.name + ".StairTrigger.Act()" + "Failed to get stair");
        }

        if (stair != null && CheckTime())
        {
            stair.UseStair(mPlayer);
            setTime();
        }
    }
}
