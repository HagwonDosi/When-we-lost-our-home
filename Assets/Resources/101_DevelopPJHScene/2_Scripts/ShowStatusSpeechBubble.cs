using UnityEngine;
using System.Collections;

public class ShowStatusSpeechBubble : ShowPlayerStatus
{
    private Status mStatus = null;
    private string mKey = "";
	
	// Update is called once per frame
	void Update ()
    {
        mCurValue = mStatus.GetStatus(mKey);
	}
}
