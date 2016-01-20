using UnityEngine;
using System.Collections;

public enum InteractionType
{
    Test,
    Door,
    Entrance,
    Stair,
    Exit,
    Enemy,
    Gun,
    Knife
}

/*
 * 그냥 InteractionType 하나 저장하는 것
*/ 
public class InteractionControl : MonoBehaviour
{
    public InteractionType mType;
    public GameObject mSubject = null;
}
