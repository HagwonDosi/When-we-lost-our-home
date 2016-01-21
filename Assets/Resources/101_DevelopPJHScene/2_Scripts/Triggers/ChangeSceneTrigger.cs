using UnityEngine;
using System.Collections;

public class ChangeSceneTrigger : UITrigger
{
    public int mSceneTo = 0;

    public override void Act()
    {
        Application.LoadLevel(mSceneTo);
    }
}
