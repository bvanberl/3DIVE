using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScanMenuInputHandler : MonoBehaviour {
    public Canvas ProjectMenu;
    public Canvas BrainMenu;
    public NetworkManager NetworkController;
    public Dropdown ScanDropdown;
    public string[] scans;
    public string selectedScan;
    public bool scansReadyFlag;
    public int counter = 1;
    public Text SelectedText;
    public Text ScanListText;
    public Text ProjectText;

    // Use this for initialization
    void Start () {
        scansReadyFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (scansReadyFlag)
        {
            scansReadyFlag = false;
            ScanListText.text = "";
            KeywordManager keywordMgr = this.gameObject.GetComponent<KeywordManager>();
            keywordMgr.KeywordsAndResponses = new KeywordManager.KeywordAndResponse[scans.Length + 1];
            NumberToWordsConverter numConv = new NumberToWordsConverter();
            foreach (string s in scans)
            {
                //ScanDropdown.options.Add(new Dropdown.OptionData() { text = s });
                ScanListText.text += (counter + ") " + s + "\n");
                keywordMgr.KeywordsAndResponses[counter - 1].Keyword = numConv.convertToString(counter);
                UnityEvent evt = new UnityEvent();
                int arg = counter;
                evt.AddListener(delegate { setSelected(arg); });
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
        ProjectMenu.gameObject.SetActive(true);
    }

    // Take user to the scan selection menu, from the project menu.
    public void onSelectButtonPressed()
    {
        this.gameObject.SetActive(false);
        BrainMenu.gameObject.SetActive(true);
        if (NetworkController == null)
            NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkManager>();
        //NetworkController.getScan(ScanDropdown.options[ScanDropdown.value].text);
        NetworkController.getScan(selectedScan);
    }

    public void setSelected(int number)
    {
        selectedScan = scans[number - 1];
        SelectedText.text = "Selected: " + selectedScan;
    }

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }
}
