using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectMenuInputHandler : MonoBehaviour {
    public Canvas ConnectMenu, ScanMenu;
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
        ConnectMenu.gameObject.SetActive(true);
    }

    // Take user to the scan selection menu, from the project menu.
    public void onSelectButtonPressed()
    {
        /* TODO: Get list of scans in selected project, and open next menu, initializing it with the proper list.
         * Activate some loading icon in the meantime
         * */
        this.gameObject.SetActive(false);
        ScanMenu.gameObject.SetActive(true);
    }
}
