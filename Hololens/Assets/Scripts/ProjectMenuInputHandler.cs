using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProjectMenuInputHandler : MonoBehaviour {
    public Canvas ConnectMenu, ScanMenu;
    public Dropdown ProjectDropdown;
    public NetworkManager NetworkController;
    public string[] projects;
    string selectedProject;
    public bool projectsReadyFlag;
    public int counter = 1;
    public Text SelectedText;
    public Text ProjectListText;

    // Use this for initialization
    void Start () {
        projects = null;
        projectsReadyFlag = false;
    }

    // Update is called once per frame
    void Update () {
		if (projectsReadyFlag)
        {
            ProjectListText.text = "";
            projectsReadyFlag = false;
            KeywordManager keywordMgr = this.gameObject.GetComponent<KeywordManager>();
            keywordMgr.KeywordsAndResponses = new KeywordManager.KeywordAndResponse[projects.Length];
            NumberToWordsConverter numConv = new NumberToWordsConverter();
            foreach (string s in projects)
            {
                //ProjectDropdown.options.Add(new Dropdown.OptionData() { text = s });
                ProjectListText.text += (counter + ") " + s + "\n");
                keywordMgr.KeywordsAndResponses[counter - 1].Keyword = numConv.convertToString(counter);
                UnityEvent evt = new UnityEvent();
                int arg = counter;
                evt.AddListener(delegate{ setSelected(arg); });
                keywordMgr.KeywordsAndResponses[counter - 1].Response = evt;
                ++counter;
            }
            keywordMgr.refreshKeywords();
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
        if (SelectedText.text != "Dictate project number to select a scan.")
        {
            this.gameObject.SetActive(false);
            ScanMenu.gameObject.SetActive(true);
            if (NetworkController == null)
                NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkManager>();

            //NetworkController.getScans(ProjectDropdown.options[ProjectDropdown.value].text);
            NetworkController.getScans(SelectedText.text);
        }
    }

    public void setSelected(int number)
    {
        SelectedText.text = projects[number - 1];
    }

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }

}
