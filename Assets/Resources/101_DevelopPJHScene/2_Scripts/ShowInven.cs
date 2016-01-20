using UnityEngine;
using System.Collections;

public class ShowInven : MonoBehaviour
{
    public InvenData mInven = null;

    public void OnPress()
    {
        mInven.DebugItems();
    }
}
