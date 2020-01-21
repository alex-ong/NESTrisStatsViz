using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnHeight : MonoBehaviour {
	private Material m;
	public float maxDist;
	public Camera cam;
	// Use this for initialization
	void Awake () {
		m = this.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		Color c = Color.white;
		c.a = Mathf.InverseLerp(maxDist,maxDist * 0.666f, Mathf.Abs(this.transform.position.y - cam.transform.position.y));
		m.SetColor("_Color", c);
	}
}
