using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConnectMenuInputHandler : MonoBehaviour {
    public Text IPText;
    public int index = 0;
    public int[] IPIndeces = null;
    public bool connectionEstablished;
    public Canvas ProjectMenu;
    public NetworkManager NetworkController;

	// Use this for initialization
	void Start () {
        IPIndeces = new int[12]{ 0, 2, 4, 8, 10, 12, 16, 18, 20, 24, 26, 28 };
        connectionEstablished = false;
        /*
        KeywordManager keywordMgr = this.gameObject.GetComponent<KeywordManager>();
        NumberToWordsConverter numConv = new NumberToWordsConverter();
        keywordMgr.KeywordsAndResponses = new KeywordManager.KeywordAndResponse[10];
        int counter = 1;
        for(int i = 0; i < 10; i++)
        {
            keywordMgr.KeywordsAndResponses[counter - 1].Keyword = numConv.convertToString(i);
            UnityEvent evt = new UnityEvent();
            string arg = i.ToString();
            evt.AddListener(delegate { onDigitButtonPressed(arg); });
            keywordMgr.KeywordsAndResponses[counter - 1].Response = evt;
            ++counter;
        }
        keywordMgr.refreshKeywords();*/
    }
	
	// Update is called once per frame
	void Update () {
		if (connectionEstablished)
        {
            connectionEstablished = false;
            this.gameObject.SetActive(false);
            ProjectMenu.gameObject.SetActive(true);
        }
	}

    public void onDigitButtonPressed(string btnText)
    {
        if(btnText == "Delete" && IPText.text.Length > 0) // Remove last entered character
        {
            IPText.text = IPText.text.Substring(0, IPText.text.Length - 1);
        }
        else
        {
            IPText.text += btnText;
        }
    }

    public void onConnectButtonPressed()
    {
        /* TODO: Get list of projects, and open next menu, initializing it with the proper list.
         * Activate some loading icon in the meantime
         * */
        if (NetworkController == null)
            NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkManager>();
        //NetworkController.connect(IPText.text);
        string ipStr = IPText.text;
        NetworkController.connect(IPText.text);
    }

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }
}
