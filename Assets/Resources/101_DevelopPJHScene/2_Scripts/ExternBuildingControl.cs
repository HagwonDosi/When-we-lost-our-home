using UnityEngine;
using System.Collections;

public class ExternBuildingControl : MonoBehaviour
{
    [SerializeField]
    private string mBuildingName = "";

    public Transform mBuildingSpawnPlace = null;

    public string BuildingName
    {
        get
        {
            return mBuildingName;
        }
    }
}
