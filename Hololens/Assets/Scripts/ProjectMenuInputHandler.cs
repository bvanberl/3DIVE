using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectMenuInputHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Take user back to the connect menu, from the project selection menu.
    public void onBackButtonPressed()
    {
        GameObject.FindGameObjectWithTag("ConnectMenuTag").SetActive(true);
        GameObject.FindGameObjectWithTag("ConnectMenuTag").GetComponent<Canvas>().enabled = true;
        GameObject.FindGameObjectWithTag("ProjectMenuTag").SetActive(false);
        GameObject.FindGameObjectWithTag("ProjectMenuTag").GetComponent<Canvas>().enabled = false;
    }

    // Take user to the scan selection menu, from the project menu.
    public void onSelectButtonPressed()
    {
        GameObject.FindGameObjectWithTag("ScanMenuTag").SetActive(true);
        GameObject.FindGameObjectWithTag("ScanMenuTag").GetComponent<Canvas>().enabled = true;
        GameObject.FindGameObjectWithTag("ProjectMenuTag").SetActive(false);
        GameObject.FindGameObjectWithTag("ProjectMenuTag").GetComponent<Canvas>().enabled = false;
    }
}
