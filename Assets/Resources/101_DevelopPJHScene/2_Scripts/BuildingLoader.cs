using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExternBuildingConEnt
{
    private ExternBuildingControl mEBCon = null;
    private Entrance mEntrance = null;

    public ExternBuildingControl ExtCon
    {
        get
        {
            return mEBCon;
        }
    }
    public Entrance Ent
    {
        get
        {
            return mEntrance;
        }
    }

    public ExternBuildingConEnt(ExternBuildingControl exCon, Entrance ent)
    {
        mEBCon = exCon;
        mEntrance = ent;
    }
}

public class BuildingLoader : MonoBehaviour
{
    #region Variables
    private Dictionary<string, ExternBuildingConEnt> mBuildingConInfo = new Dictionary<string, ExternBuildingConEnt>();
    private Dictionary<string, GameObject> mBuildingPreInfo = new Dictionary<string, GameObject>();
    [SerializeField]
    private GameObject mCurBuilding = null;
    [SerializeField]
    private ExternBuildingControl mCurExternBuilding = null;
    [SerializeField]
    private GameObject mExit = null;
    #endregion

    #region Capsule
    public GameObject CurBuiding
    {
        get
        {
            return mCurBuilding;
        }
    }
    public ExternBuildingControl CurExternBuildingControl
    {
        get
        {
            return mCurExternBuilding;
        }
    }

    #endregion

    // Use this for initialization
    void Start ()
    {
        var entrances = FindObjectsOfType<Entrance>();
        var exBCon = FindObjectsOfType<ExternBuildingControl>();

        Dictionary<string, Entrance> entranceDic = new Dictionary<string, Entrance>();
        Dictionary<string, ExternBuildingControl> exBConDic = new Dictionary<string, ExternBuildingControl>();

        for(int i = 0; i < entrances.Length; i++)
        {
            entranceDic.Add(entrances[i].mEntranceTo, entrances[i]);
        }
        for(int i = 0; i< exBCon.Length; i++)
        {
            exBConDic.Add(exBCon[i].BuildingName, exBCon[i]);
        }

        foreach(var iter in entranceDic)
        {
            ExternBuildingControl con = null;

            exBConDic.TryGetValue(iter.Key, out con);

            mBuildingConInfo.Add(iter.Key, new ExternBuildingConEnt(con, iter.Value));

            var prefab = Resources.Load("101_DevelopPJHScene/1_Prefabs/MapPrefabs/" + iter.Key) as GameObject;

            mBuildingPreInfo.Add(iter.Key, prefab);
        }
    }

    #region CustomFunctions
    public void LoadPrefabMap(string fMapName)
    {
        GameObject prefab = null;
        ExternBuildingConEnt conEnt = null;

        if(mBuildingConInfo.TryGetValue(fMapName, out conEnt))
        {
            if (mBuildingPreInfo.TryGetValue(fMapName, out prefab))
            {
                GameObject building = Instantiate(prefab, conEnt.ExtCon.mBuildingSpawnPlace.position, Quaternion.Euler(0, 0, 0)) as GameObject;

                building.GetComponent<BuildingControl>().mBuildingName = fMapName;
                mCurExternBuilding = conEnt.ExtCon;
                mCurBuilding = building;
                //mCurBuilding.GetComponent<BuildingControl>().SetPlayer();
            }
            else
            {
                Debug.LogWarning(gameObject.name + ".BuildingLoader.LoadPrefabMap() " + "Map doesn't exist");
            }
        }
        else
        {
            Debug.LogWarning(gameObject.name + ".BuildingLoader.LoadPrefabMap() " + "Map doesn't exist");
        }
    }

    /// <summary>
    /// 현재 빌딩의 들어간 부분에 출구를 만드는 함수
    /// </summary>
    /// <param name="fEnt">들어온 입구</param>
    public void AddExitInCurBuiding(Entrance fEnt)
    {
        GameObject exit = Instantiate(mExit) as GameObject;
        exit.transform.parent = mCurBuilding.transform;

        mCurBuilding.GetComponent<BuildingControl>().mExit = exit.transform;
        exit.transform.position = new Vector3(fEnt.transform.position.x, fEnt.transform.position.y, mCurBuilding.transform.position.z);
    }
    #endregion
}
