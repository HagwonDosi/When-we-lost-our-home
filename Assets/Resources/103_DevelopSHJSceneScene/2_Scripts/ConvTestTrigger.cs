using UnityEngine;
using System.Collections;

public class ConvTestTrigger : UITrigger
{
    public ConversationFileControl mConvCon = null;
    public int mMessegeIndex = 4;
    public float mDuration = 1;

    public override void Act()
    {
        mConvCon.ShowTextByIndex(mMessegeIndex, 0, mDuration);   
    }
}
