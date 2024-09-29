using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureGroup : MonoBehaviour
{
    public List<GameObject> gesturesToEnable;

    public List<GameObject> gesturesToDisable;

    public void ActivateGestureGroup()
    {
        foreach (GameObject gesture in gesturesToEnable)
        {
            if (gesture != null)
                gesture.SetActive(true);
        }

        foreach (GameObject gesture in gesturesToDisable)
        {
            if (gesture != null)
                gesture.SetActive(false);
        }
    }
}
