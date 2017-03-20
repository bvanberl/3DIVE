using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectMenuInputHandler : MonoBehaviour {
    public Canvas ConnectMenu, ScanMenu;
    public Dropdown ProjectDropdown;
    public NetworkManager NetworkController;
    public string[] projects;
    string selectedProject;
    public bool projectsReadyFlag;

	// Use this for initialization
	void Start () {
        projects = null;
        projectsReadyFlag = false;
    }

    // Update is called once per frame
    void Update () {
		if (projectsReadyFlag)
        {
            projectsReadyFlag = false;
            foreach (string s in projects)
            {
                ProjectDropdown.options.Add(new Dropdown.OptionData() { text = s });
            }
        }
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
        if (NetworkController == null)
            NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkManager>();
        
        NetworkController.getScans(ProjectDropdown.options[ProjectDropdown.value].text);
    }

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }
}
