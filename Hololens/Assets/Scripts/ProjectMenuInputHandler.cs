using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProjectMenuInputHandler : MonoBehaviour {
    public Canvas ConnectMenu, ScanMenu, BrainMenu;
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
            keywordMgr.KeywordsAndResponses = new KeywordManager.KeywordAndResponse[projects.Length + 1];
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

            // Add voice command for 'continue'
            keywordMgr.KeywordsAndResponses[counter - 1].Keyword = "continue";
            UnityEvent contEvt = new UnityEvent();
            contEvt.AddListener(onSelectButtonPressed);
            keywordMgr.KeywordsAndResponses[counter - 1].Response = contEvt;
            counter = 1;

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

            // Get selected project
            NetworkController.getScans(selectedProject);
            ScanMenu.GetComponent<ScanMenuInputHandler>().ProjectText.text = selectedProject;
            BrainMenu.GetComponent<BrainMenuInputHandler>().projectNameStr = selectedProject;
        }
    }

    public void setSelected(int number)
    {
        selectedProject = projects[number - 1];
        SelectedText.text = "Selected: " + selectedProject;
    }

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }

}
