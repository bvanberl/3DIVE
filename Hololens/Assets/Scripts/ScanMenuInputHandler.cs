using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanMenuInputHandler : MonoBehaviour {
    public Canvas ProjectMenu;
    public Canvas BrainMenu;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Take user back to the connect menu, from the project selection menu.
    public void onBackButtonPressed()
    {
        this.gameObject.SetActive(false);
        ProjectMenu.gameObject.SetActive(true);
    }

    // Take user to the scan selection menu, from the project menu.
    public void onSelectButtonPressed()
    {
        this.gameObject.SetActive(false);
        BrainMenu.gameObject.SetActive(true);
    }

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }
}
