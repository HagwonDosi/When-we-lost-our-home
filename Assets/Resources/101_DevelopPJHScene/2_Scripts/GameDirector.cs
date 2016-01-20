using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour
{
    #region Variables
    private static GameDirector mInstance = null;

    public UIRoot mRoot = null;
    public UIPanel mPanel = null;
    public GameObject mPlayer = null;
    #endregion

    #region get/setter
    public static GameDirector Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = GameObject.FindObjectOfType<GameDirector>();

            return mInstance;
        }
    }
    #endregion

    #region VirtualFuntions
    // Use this for initialization
    void Start ()
    {
	
	}
    #endregion

    #region CustomFunctions
    public Vector2 getPanelSize()
    {
        Vector2 size = new Vector2();

        size.x = mPanel.width;
        size.y = mPanel.height;

        return size;
    }

    
    static public T CustomGetComponent<T>(GameObject obj) where T : Component
    {
        T com = obj.GetComponent<T>();

        if(com == null)
        {
            Debug.Log(typeof(T).ToString() + "was not found in " + obj.name);
            com = obj.AddComponent<T>();
        }

        return com;
    }
    #endregion
}
