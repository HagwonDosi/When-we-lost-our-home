using UnityEngine;
using System.Collections;

public class DebugTrigger : UITrigger
{
    public string mLogStr = "";

    public override void Act()
    {
        Debug.Log("Debug Trigger" + mLogStr);
    }
}
