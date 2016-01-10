using UnityEngine;
using System.Collections;

public class EntranceTrigger : UITrigger
{
    public CollisionTrigger mCTrigger = null;
    public MapLoader mLoader = null;
    public SmooothCamera mCamera = null;

    public override void Act()
    {
        Entrance ent = null;
        try
        {
            ent = mCTrigger.ColliderObject.GetComponent<Entrance>();
        }
        catch
        {
            Debug.LogWarning(gameObject.name + ".EntranceTrigger.Act() " + "error get ent");
            return;
        }

        mLoader.LoadPrefabMap(ent.mEntranceTo);
        mCamera.GoImmediately();
    }
}
