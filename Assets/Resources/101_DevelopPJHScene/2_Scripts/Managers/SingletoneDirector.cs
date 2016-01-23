using UnityEngine;
using System.Collections;

public class SingletoneDirector<T> : MonoBehaviour where T : SingletoneDirector<T>
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
                Debug.LogWarning(typeof(T).ToString() + " is not exist on hierarchy");
            }

            return mInstance;
        }
    }
}
