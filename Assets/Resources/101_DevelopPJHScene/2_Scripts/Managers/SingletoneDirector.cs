using UnityEngine;
using System.Collections;

public class Singletone<T> : MonoBehaviour where T : Singletone<T>
{
    private static T mInstance = null;
    
    public static T Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = FindObjectOfType<T>();
            }
            if(mInstance == null)
            {
                Debug.LogWarning(typeof(T).ToString() + " does not exist on hierarchy");
            }

            return mInstance;
        }
    }
}
