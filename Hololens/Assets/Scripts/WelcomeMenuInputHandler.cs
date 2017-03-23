using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeMenuInputHandler : MonoBehaviour {
    public Canvas statusBar;
    public Canvas connectMenu;

	public void onStartButtonPressed()
    {
        statusBar.gameObject.SetActive(true);
        connectMenu.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
