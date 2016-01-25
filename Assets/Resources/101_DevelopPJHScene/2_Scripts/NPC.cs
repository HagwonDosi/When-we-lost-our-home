using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public SpeechBubbleControl mSpeechBubble = null;

    private string mNPCName = "";
    

	// Use this for initialization
	void Start ()
    {
        mNPCName = new string(name.ToCharArray(), 0, name.Length - 7);
	}

    void OnDestroy()
    {
        Destroy(mSpeechBubble.gameObject);
    }
	
    public void MakeConversation()
    {
        SpeechBubbleDirector.Instance.ShowText(mSpeechBubble, mNPCName, 0, 3f);
    }
}
