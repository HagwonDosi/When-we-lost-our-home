using UnityEngine;
using System.Collections;

public class RestrictedAreaEnterTrigger : UITrigger
{
    private PlayerStatus mPStatus = null;

	// Use this for initialization
	void Start ()
    {
        mPStatus = GameDirector.Instance.Player.GetComponent<PlayerStatus>();
	}

    public override void Act()
    {
        mPStatus.mIsPlayerInDanger = true;
        SpeechBubbleDirector.Instance.ShowText(0, "Player", 10, 2f);
    }
}
