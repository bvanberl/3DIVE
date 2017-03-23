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
    public bool scansReadyFlag;
    public int counter = 1;
    public Text SelectedText;
    public Text ScanListText;

    // Use this for initialization
    void Start () {
        scansReadyFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (scansReadyFlag)
        {
            scansReadyFlag = false;
            KeywordManager keywordMgr = this.gameObject.GetComponent<KeywordManager>();
            keywordMgr.KeywordsAndResponses = new KeywordManager.KeywordAndResponse[scans.Length];
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
        NetworkController.getScan(SelectedText.text);
    }

    public void setSelected(int number)
    {
        SelectedText.text = scans[number - 1];
    }

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }
}
