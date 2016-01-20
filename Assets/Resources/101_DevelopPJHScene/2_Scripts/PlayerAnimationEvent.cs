using UnityEngine;
using System.Collections;

public class PlayerAnimationEvent : MonoBehaviour
{
    void GameOver()
    {
        Application.LoadLevel(4);
    }
}
