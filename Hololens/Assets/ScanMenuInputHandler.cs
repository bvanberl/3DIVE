using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanMenuInputHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Take user back to the connect menu, from the project selection menu.
    public void onBackButtonPressed()
    {
        GameObject.FindGameObjectWithTag("ProjectMenuTag").SetActive(true);
        GameObject.FindGameObjectWithTag("ProjectMenuTag").GetComponent<Canvas>().enabled = true;
        GameObject.FindGameObjectWithTag("ScanMenuTag").SetActive(false);
        GameObject.FindGameObjectWithTag("ScanMenuTag").GetComponent<Canvas>().enabled = false;
    }

    // Take user to the scan selection menu, from the project menu.
    public void onSelectButtonPressed()
    {
        GameObject.FindGameObjectWithTag("ScanMenuTag").SetActive(false);
        GameObject.FindGameObjectWithTag("ScanMenuTag").GetComponent<Canvas>().enabled = false;
        GameObject.FindGameObjectWithTag("BrainTag").SetActive(true);
    }
}
