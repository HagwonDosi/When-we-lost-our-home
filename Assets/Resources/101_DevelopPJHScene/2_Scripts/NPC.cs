using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public int mSpeechBubbleIndex = 0;

    private string mNPCName = "";
    

	// Use this for initialization
	void Start ()
    {
        mNPCName = new string(name.ToCharArray(), 0, name.Length - 7);
	}
	
    public void MakeConversation()
    {
        SpeechBubbleDirector.Instance.ShowText(mSpeechBubbleIndex, mNPCName, 0, 3f);
    }
}
