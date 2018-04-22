using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public float size;

    private float t;

	// Use this for initialization
	void Start () {
        size = 0.72f;
        t = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float sized = GetComponent<Camera>().orthographicSize;
		if (size != sized)
        {
            Debug.Log("pommier2"+size);
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(sized, size, t);
            t = t + Time.deltaTime*0.05f;
        }
	}
}
