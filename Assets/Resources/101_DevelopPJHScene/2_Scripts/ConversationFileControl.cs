using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConversationFileControl : MonoBehaviour
{
    public string mFileName = "";
    public List<SpeechBubbleControl> mSpeechBubbles = new List<SpeechBubbleControl>();

    public List<string> mListOfConvs = new List<string>();

	// Use this for initialization
	void Start ()
    {
        string dir = System.Environment.CurrentDirectory + "\\Assets\\Resources\\DataFiles\\" + mFileName;
        StreamReader reader = new StreamReader(dir);

        //파일에서 줄단위로 내용을 얻어와 List에 저장
        while(reader.Peek() != -1)
        {
            mListOfConvs.Add(reader.ReadLine());
        }
	}
	
    /*
     * List 인덱스를 인자로 전달해서 해당 인덱스의 메세지를 말풍선에 출력
    */
    public bool ShowTextByIndex(int fIndex, int fSIndex, float fDuration)
    {
        if(fIndex >= 0 && fIndex < mListOfConvs.Count
            && fSIndex >= 0 && fSIndex < mSpeechBubbles.Count)
        {
            return mSpeechBubbles[fSIndex].ShowText(mListOfConvs[fIndex], fDuration);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " ConversationFileControl.ShowTextByIndex " + "Out of Index");
            return false;
        }
    }
}
