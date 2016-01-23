using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConversationFileControl : MonoBehaviour
{
    [SerializeField]
    private string mFileName = "";
    [SerializeField]
    private List<string> mListOfConvs = new List<string>();

    public string FileName
    {
        get
        {
            return mFileName;
        }
    }

    public List<string> ConvsList
    {
        get
        {
            return mListOfConvs;
        }
    }

	// Use this for initialization
	void Start ()
    {
        string dir = System.Environment.CurrentDirectory + "\\Assets\\Resources\\DataFiles\\" + mFileName;
        StreamReader reader = FileIODirector.ReadFile(dir);

        //파일에서 줄단위로 내용을 얻어와 List에 저장
        while(reader.Peek() != -1)
        {
            mListOfConvs.Add(reader.ReadLine());
        }
	}
	
}
