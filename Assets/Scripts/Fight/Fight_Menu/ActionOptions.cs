using UnityEngine;
using UnityEngine.UI;

public class ActionOptions : MonoBehaviour
{
    public void SetAllTogglesOff()
    {
        GetComponent<ToggleGroup>().SetAllTogglesOff();
    }
}
