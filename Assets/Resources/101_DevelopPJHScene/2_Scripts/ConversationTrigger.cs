using UnityEngine;
using System.Collections;

public class ConversationTrigger : UITrigger
{
    private CollisionTrigger mCTrigger = null;

    void Start()
    {
        mCTrigger = GameDirector.Instance.Player.GetComponent<CollisionTrigger>();
    }

    public override void Act()
    {
        NPC npc = mCTrigger.ColliderObject.GetComponent<InteractionControl>().mSubject.GetComponent<NPC>();

        npc.MakeConversation();
    }
}
