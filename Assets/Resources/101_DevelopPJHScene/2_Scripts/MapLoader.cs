using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct PrefabInfo
{
    public string mPrefabName;
    public GameObject mObject;
}

/// <summary>
/// 맵을 불러오기 위한 스크립트
/// </summary>
public class MapLoader : MonoBehaviour
{
    #region Variables
    public GameObject mFloorTemp = null;
    public GameObject mWallTemp = null;
    public GameObject mStairTemp = null;
    public GameObject mBuilding = null;
    public Transform mPlayerPos = null;
    public float mHeightOfFloor = 3f;
    public float mMagnification = 1f;
    public Vector3 mOffset = Vector3.zero;
    public string mFileName = "";
    public List<PrefabInfo> mPrefabList;
    public Transform mBuildingPos = null;
    public bool mLoadExternMap = false;

    private string mVersion = "1.2.0";
    private Dictionary<string, GameObject> mBuildingInfo = new Dictionary<string, GameObject>();
    [SerializeField]
    private GameObject mCurBuilding = null;
    #endregion

    #region get/setter
    public GameObject CurBuiding
    {
        get
        {
            return mCurBuilding;
        }
    }

    #endregion

    #region VirtualFunctions
    // Use this for initialization
    void Start ()
    {
        if(mLoadExternMap)
            LoadExternMap();

        foreach(var iter in mPrefabList)
        {
            mBuildingInfo.Add(iter.mPrefabName, iter.mObject);
        }
	}


    #endregion

    #region CustomFunction
    private float GetYByFloor(int fFloor)
    {
        return mOffset.y + ((fFloor - 1) * mHeightOfFloor);
    }

    private void MakeFloor(int fFloor, float fLeft, float fRight)
    {
        GameObject floor = Instantiate(mFloorTemp) as GameObject;
        floor.transform.parent = mBuilding.transform;

        floor.transform.localPosition = new Vector3(mOffset.x, GetYByFloor(fFloor), mOffset.z);
        float width = Mathf.Abs(fLeft - fRight);
        float fixedWidth = width * mMagnification;

        float fix = (fixedWidth - width) / 2f;

        floor.GetComponent<FloorControl>().setLimit(fLeft - fix, fRight + fix);
    }

    private void MakeWall(int fFloor, float fXPos)
    {
        GameObject wall = Instantiate(mWallTemp) as GameObject;
        wall.transform.parent = mBuilding.transform;

        wall.transform.localPosition = new Vector3(mOffset.x * mMagnification, GetYByFloor(fFloor + 1), mOffset.z);
    }

    private void MakeStair (int fFloor, float fXPos)
    {
        GameObject stair = Instantiate(mStairTemp) as GameObject;
        stair.transform.parent = mBuilding.transform;

        stair.transform.localPosition = new Vector3(mOffset.x * mMagnification, GetYByFloor(fFloor) - (mHeightOfFloor / 2), mOffset.z + 1f);
    }

    private void MakePlayerPos (float fXPos)
    {
        mPlayerPos.localPosition = new Vector3(fXPos * mMagnification, GetYByFloor(1), mOffset.z);
    }

    private void MakeModel(string fName, int fFloor, float xPos)
    {
        string path = "Models/" + fName;

        GameObject model = null;

        try
        {
            model = Resources.Load(path) as GameObject;
            Debug.Log("path " + path);
        }
        catch
        {
            Debug.LogWarning(gameObject.name + ".MapEditor.LoadBGModel() " + "could not find the resource " + fName);
            return;
        }

        if (model == null)
        {
            Debug.LogWarning(gameObject.name + ".MapEditor.LoadBGModel() " + "could not find the resource " + fName);
            return;
        }

        GameObject modelCopy = Instantiate(model) as GameObject;
        modelCopy.transform.parent = mBuilding.transform;

        modelCopy.transform.localScale = new Vector3(2.5f, 2.5f,2.5f);
        modelCopy.transform.localPosition = new Vector3(xPos, GetYByFloor(fFloor + 1), mOffset.z + 1f);
    }

    public void LoadExternMap()
    {
        string dir = System.Environment.CurrentDirectory + "\\Assets\\Resources\\DataFiles\\Map\\" + mFileName + ".wwmap";
        StreamReader reader = null;

        try
        {
            reader = new StreamReader(dir);
        }
        catch
        {
            Debug.LogWarning(gameObject.name + ".MapLoader.LoadMap() " + "File doesn't Exist at " + dir);
            return;
        }
        Debug.Log("File at " + dir + " Opened");

        if(!reader.ReadLine().Equals(mVersion))
        {
            Debug.LogWarning(gameObject.name + ".MapLoader.LoadMap() " + "Map Version invalid");
            return;
        }

        int floor = System.Convert.ToInt32(reader.ReadLine());

        for(int i = 0; i < floor; i++)
        {
            float left = (float)(System.Convert.ToDouble(reader.ReadLine()));
            float right = (float)(System.Convert.ToDouble(reader.ReadLine()));

            MakeFloor(i + 1, left, right);
        }

        int wallNum = System.Convert.ToInt32(reader.ReadLine());

        for(int i = 0; i < wallNum; i++)
        {
            int arrFloor = System.Convert.ToInt32(reader.ReadLine());
            float xPos = (float)System.Convert.ToDouble(reader.ReadLine());

            MakeWall(arrFloor, xPos);
        }

        int stairNum = System.Convert.ToInt32(reader.ReadLine());

        for(int i = 0; i < stairNum; i++)
        {
            int arrFloor = System.Convert.ToInt32(reader.ReadLine());
            float xPos = (float)System.Convert.ToDouble(reader.ReadLine());

            MakeStair(arrFloor, xPos);
        }

        if(System.Convert.ToBoolean(reader.ReadLine()))
        {
            float xPos = (float)System.Convert.ToDouble(reader.ReadLine());

            MakePlayerPos(xPos);
        }

        int modelNum = System.Convert.ToInt32(reader.ReadLine());

        for(int i = 0; i < modelNum; i++)
        {
            string name = reader.ReadLine();
            int arrFloor = System.Convert.ToInt32(reader.ReadLine());
            float xPos = (float)System.Convert.ToDouble(reader.ReadLine());

            MakeModel(name, arrFloor, xPos);
        }
    }

    public void LoadPrefabMap(string fMapName)
    {
        GameObject prefab = null;

        if(mBuildingInfo.TryGetValue(fMapName, out prefab))
        {
            GameObject building = Instantiate(prefab, mBuildingPos.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            building.GetComponent<BuildingControl>().mBuildingName = fMapName;
            mCurBuilding = building;
            //mCurBuilding.GetComponent<BuildingControl>().SetPlayer();
        }
        else
        {
            Debug.LogWarning(gameObject.name + ".MapLoader.LoadPrefabMap() " + "Map doesn't exist");
        }
    }

    #endregion
}
