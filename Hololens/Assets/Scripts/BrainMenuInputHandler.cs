using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class BrainMenuInputHandler : MonoBehaviour {
    public Canvas ScanMenu;
    public GameObject BrainScanCube;
    public Text ProjectNameText, ScanNameText, PatientNameText, DateTimeText;
    public List<GameObject> notes;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onCreateNoteButtonPressed()
    {
        GameObject newNote = Instantiate(Resources.Load("Prefabs/NoteParent")) as GameObject;
        notes.Add(newNote);
    }

    public void onShowHologramButtonPressed()
    {
        BrainScanCube.SetActive(true);
    }


    public void onBackButtonPressed()
    {
        this.gameObject.SetActive(false);
        ScanMenu.gameObject.SetActive(true);
    }



}
