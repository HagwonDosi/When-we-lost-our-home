using UnityEngine;
using System.Collections;

public class StatusAddValTrigger : UITrigger
{
    [SerializeField]
    private Status mStatus = null;
    [SerializeField]
    private string mKey = "";
    [SerializeField]
    private float mVal = 0f;

    public override void Act()
    {
        mStatus.AddValStatus(mKey, mVal);
    }
}
