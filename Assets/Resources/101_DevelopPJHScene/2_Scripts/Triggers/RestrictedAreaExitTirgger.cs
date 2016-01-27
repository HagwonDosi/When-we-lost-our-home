using UnityEngine;
using System.Collections;

public class RestrictedAreaExitTirgger : UITrigger
{
    private PlayerStatus mPStatus = null;

    // Use this for initialization
    void Start()
    {
        mPStatus = GameDirector.Instance.Player.GetComponent<PlayerStatus>();
    }

    public override void Act()
    {
        mPStatus.mIsPlayerInDanger = false;
        SpeechBubbleDirector.Instance.ShowText(0, "Player", 11, 2f);
    }
}
