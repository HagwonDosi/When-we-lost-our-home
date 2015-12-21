using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConversationFileControl : MonoBehaviour
{
    public string mFileName = "";
    public ShowTextSlowly mText = null;

    private List<string> mListOfConvs = new List<string>();

	// Use this for initialization
	void Start ()
    {
        string dir = System.Environment.CurrentDirectory + "\\Assets\\Resources\\DataFiles\\" + mFileName;
        StreamReader reader = new StreamReader(dir);

        while(reader.Peek() != -1)
        {
            mListOfConvs.Add(reader.ReadLine());
        }
	}
	
}
