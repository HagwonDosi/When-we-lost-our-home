using UnityEngine;
using System.Collections;

public class PressUseSelectedItem : MonoBehaviour
{
    public void OnPress()
    {
        ItemDirector.Instance.SelectedItemUse();
    }
}
