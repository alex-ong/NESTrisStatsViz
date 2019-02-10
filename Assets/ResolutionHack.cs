using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHack : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.F1))
        {
            Screen.SetResolution(400, 300, false);
        }
	}
}
