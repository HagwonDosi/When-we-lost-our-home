using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObjectStringFloat
{
    public string mName = "";
    public GameObject mObject = null;
    public float mFVal = 0f;
}

/// <summary>
/// NPC와 관련된 것을 관장하는 Director
/// </summary>
public class NPCDirector : SingletoneDirector<NPCDirector>
{
    #region Variables
    [SerializeField]
    private List<ObjectStringFloat> mObjectList = new List<ObjectStringFloat>();
    private Dictionary<string, ObjectStringFloat> mObjectDic = new Dictionary<string, ObjectStringFloat>();
    [SerializeField]
    private GameObject mSpeechBubblePre = null;

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
        ObjectStringFloat ori = null;
        GameObject npc = null;
        if(mObjectDic.TryGetValue(fName, out ori))
        {
            npc = Instantiate(ori.mObject) as GameObject;
            npc.transform.parent = fBCon.transform;

            npc.transform.localPosition = new Vector2(fXPos, fBCon.GetYByFloor(fFloor));
        }

        GameObject bubble = Instantiate(mSpeechBubblePre) as GameObject;
        bubble.transform.parent = UIDirector.Instance.GetUIAnchor(UIAnchor.Side.Center).transform;

        Vector3 screen = SmooothCamera.Instance.Camera3D.WorldToScreenPoint(npc.transform.position);
        screen.y += ori.mFVal;
        bubble.transform.localPosition = screen;

        return npc;
    }
    #endregion
}
