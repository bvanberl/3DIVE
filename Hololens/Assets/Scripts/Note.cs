using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Note : MonoBehaviour {

    public string content;

    // Use this for initialization
    void Awake () {

    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
        transform.localEulerAngles += new Vector3(0.0f, 180.0f, 0.0f);
	}
}
