using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHack : MonoBehaviour
{

    // Update is called once per frame
    void Start()
    {
        SetAspectRatio();
    }

    public void SetAspectRatio()
    {
        int x = Mathf.RoundToInt(Screen.currentResolution.height / 3.0f * 2.0f);
        Screen.SetResolution(x, Screen.currentResolution.height, false);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            Screen.SetResolution(400, 600, false);
        }
        if (Input.GetKey(KeyCode.F2))
        {
            SetAspectRatio();
        }
    }
}
