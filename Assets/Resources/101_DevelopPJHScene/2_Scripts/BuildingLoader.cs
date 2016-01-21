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
    public List<PrefabInfo> mPrefabList;

    private Dictionary<string, ExternBuildingConEnt> mBuildingConInfo = new Dictionary<string, ExternBuildingConEnt>();
    private Dictionary<string, GameObject> mBuildingPreInfo = new Dictionary<string, GameObject>();
    [SerializeField]
    private GameObject mCurBuilding = null;

    #region get/setter
    public GameObject CurBuiding
    {
        get
        {
            return mCurBuilding;
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
        }

        foreach (var iter in mPrefabList)
        {
            mBuildingPreInfo.Add(iter.mPrefabName, iter.mObject);
        }
    }

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
}
