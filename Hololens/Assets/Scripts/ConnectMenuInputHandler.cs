﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectMenuInputHandler : MonoBehaviour {
    public Text IPText;
    public int index = 0;
    public int[] IPIndeces = null;
    public Canvas ProjectMenu;

	// Use this for initialization
	void Start () {
        IPIndeces = new int[12]{ 0, 2, 4, 8, 10, 12, 16, 18, 20, 24, 26, 28 };
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onDigitButtonPressed(string btnText)
    {
        char[] arr = IPText.text.ToCharArray();
        if (btnText == "Delete")
        {
            if (index > 0)
            {
                --index;
                arr[IPIndeces[index]] = '_';
            }
            else
            {
                return;
            }
        }
        else if(index < IPIndeces.Length)
        {
            arr[IPIndeces[index]] = btnText[0];
            ++index;
        }
        IPText.text = new string(arr);
    }

    public void onConnectButtonPressed()
    {
        /* TODO: Get list of projects, and open next menu, initializing it with the proper list.
         * Activate some loading icon in the meantime
         * */
        this.gameObject.SetActive(false);
        ProjectMenu.gameObject.SetActive(true);
    }

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }
}
