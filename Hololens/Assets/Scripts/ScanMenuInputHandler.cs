using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanMenuInputHandler : MonoBehaviour {
    public Canvas ProjectMenu;
    public Canvas BrainMenu;
    public NetworkManager NetworkController;
    public Dropdown ScanDropdown;
    public string[] scans;
    public bool scansReadyFlag;

    // Use this for initialization
    void Start () {
        scansReadyFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (scansReadyFlag)
        {
            scansReadyFlag = false;
            foreach (string s in scans)
            {
                ScanDropdown.options.Add(new Dropdown.OptionData() { text = s });
            }
        }
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
        if (NetworkController == null)
            NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkManager>();
        NetworkController.getScan(ScanDropdown.options[ScanDropdown.value].text);
        
    }

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }
}
