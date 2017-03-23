using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBarInputHandler : MonoBehaviour {
    public NetworkManager NetworkController;

    public void onDisconnectButtonPressed()
    {
        // disconnect NetworkController and update UI accordingly 
        if (NetworkController == null)
            NetworkController = GameObject.Find("NetworkController").GetComponent<NetworkManager>();
        NetworkController.disconnect();
    }
	
}
