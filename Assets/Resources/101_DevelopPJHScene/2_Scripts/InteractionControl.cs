using UnityEngine;
using System.Collections;

public enum InteractionType
{
    Test,
    Door
}

/*
 * 그냥 InteractionType 하나 저장하는 것
*/ 
public class InteractionControl : MonoBehaviour
{

    public InteractionType mType;
}
