using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectStringVec2
{
    public string mName = "";
    public GameObject mObject = null;
    public Vector2 mVec2 = Vector2.zero;
}

/// <summary>
/// NPC와 관련된 것을 관장하는 Director
/// </summary>
public class NPCDirector : Singletone<NPCDirector>
{
    #region Variables
    [SerializeField]
    private List<ObjectStringVec2> mObjectList = new List<ObjectStringVec2>();
    private Dictionary<string, ObjectStringVec2> mObjectDic = new Dictionary<string, ObjectStringVec2>();

    #endregion

    #region VirtualFunctions
    void Start()
    {
        foreach(var iter in mObjectList)
        {
            mObjectDic.Add(iter.mName, iter);
        }
    }

    #endregion

    #region CustomFunctions
    public void MapLoaded(string fBuildingName, BuildingControl fBCon)
    {
        Debug.Log("map " + fBuildingName + " npc load");
        string dir = System.Environment.CurrentDirectory + "/Assets/Resources/DataFiles/MapData/" + fBuildingName + ".npcinfo";

        //건물에 한 번도 들어간 적이 없을 때 경로를 변경
        if(!System.IO.File.Exists(dir))
        {
            dir = System.Environment.CurrentDirectory + "/Assets/Resources/DataFiles/Map/" + fBuildingName + ".npcinfo";
        }

        StreamReader reader = FileIODirector.ReadFile(dir);

        if(reader != null)
        {
            int npcNum = System.Convert.ToInt32(reader.ReadLine());

            for (int i = 0; i < npcNum; i++)
            {
                string npcName = reader.ReadLine();
                int floor = System.Convert.ToInt32(reader.ReadLine());
                float xPos = System.Convert.ToInt32(reader.ReadLine());

                MakeNPC(npcName, floor, xPos, fBCon);
            }
        }
    }

    private GameObject MakeNPC(string fName, int fFloor, float fXPos, BuildingControl fBCon)
    {
        ObjectStringVec2 ori = null;
        GameObject npc = null;
        if(mObjectDic.TryGetValue(fName, out ori))
        {
            npc = Instantiate(ori.mObject) as GameObject;
            npc.transform.parent = fBCon.transform;

            npc.transform.localPosition = new Vector2(fXPos, fBCon.GetYByFloor(fFloor));

            NPC con = npc.GetComponent<NPC>();

            con.mSpeechBubbleIndex = SpeechBubbleDirector.Instance.MakeSpeechBubble(npc.transform, ori.mVec2);
        }
        else
        {
            Debug.LogWarning(name + "NPCDirector.MakeNPC() " + "npc " + fName + " was not found");
        }


        return npc;
    }
    #endregion
}
