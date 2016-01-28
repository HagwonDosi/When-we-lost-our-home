using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public SpeechBubbleControl mSpeechBubble = null;

    private string mNPCName = "";
    private int mCurConvIndex = 0;
    private int mCurPlayerIndex = 0;
    [SerializeField]
    private int mConvStartIndex = 0;
    [SerializeField]
    private int mPlayerConvStartIndex = 0;
    [SerializeField]
    private int mConvEndIndex = 0;
    [SerializeField]
    private int mPlayerConvEndIndex = 0;
    
    public int PlayerConvIndex
    {
        get
        {
            return mCurPlayerIndex;
        }
    }

	// Use this for initialization
	void Start ()
    {
        mCurConvIndex = mConvStartIndex;
        mCurPlayerIndex = mPlayerConvStartIndex;
        mNPCName = new string(name.ToCharArray(), 0, name.Length - 7);
	}

    void OnDestroy()
    {
        Destroy(mSpeechBubble.gameObject);
    }
	
    public void MakeConversation()
    {
        if (mCurConvIndex > mConvEndIndex)
        {
            mCurConvIndex = mConvStartIndex;
        }
        if(mCurPlayerIndex > mPlayerConvEndIndex)
        {
            mCurPlayerIndex = mPlayerConvStartIndex;
        }

        SpeechBubbleDirector.Instance.ShowText(0, "Player", mCurPlayerIndex, 1);

        string conv = SpeechBubbleDirector.Instance.GetConversationString("Player", mCurPlayerIndex);
        conv = conv.Replace('\n', '@');
        float sec = 0.15f * conv.Length;
        Debug.Log("conv " + conv + " sec " + sec);
        SpeechBubbleDirector.Instance.ShowText(1.2f + sec, mSpeechBubble, mNPCName, mCurConvIndex, 1);

        mCurConvIndex++;
        mCurPlayerIndex++;
    }
}
