using UnityEngine;
using System.Collections;

public class ColliderEvent : MonoBehaviour
{
    public Collider mCollider = null;

    public void SetColliderOn()
    {
        mCollider.enabled = true;
    }

    public void SetColliderOff()
    {
        mCollider.enabled = false;
    }
}
