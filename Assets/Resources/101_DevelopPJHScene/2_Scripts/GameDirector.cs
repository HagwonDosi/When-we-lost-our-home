using UnityEngine;
using System.Collections;

public class GameDirector : MonoBehaviour
{
    private static GameDirector mInstance = null;

    public static GameDirector Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = GameObject.FindObjectOfType<GameDirector>();

            return mInstance;
        }
    }

    public UIRoot mRoot = null;
    public UIPanel mPanel = null;

	// Use this for initialization
	void Start ()
    {
	
	}
	
    public Vector2 getPanelSize()
    {
        Vector2 size = new Vector2();

        size.x = mPanel.width;
        size.y = mPanel.height;

        return size;
    }
}
