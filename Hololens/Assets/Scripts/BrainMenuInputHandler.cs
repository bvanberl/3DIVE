using HoloToolkit.Unity.InputModule;
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
    public AudioSource audioSource;
    public MicrophoneManager microphoneManager;
    public bool isRecording = false;
    public Text recordBtnText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onCreateNoteButtonPressed()
    {
        if(!isRecording)
        {
           // TransformManager.Instance.keywordRecognizer.Stop();
            recordBtnText.text = "Stop Recording";
            GameObject newNote = Instantiate(Resources.Load("Prefabs/NoteParent")) as GameObject;
            notes.Add(newNote);
            microphoneManager.DictationDisplay = newNote.GetComponentInChildren<TextMesh>();
            isRecording = true;
            audioSource.clip = microphoneManager.StartRecording();
        }
        else
        {
            recordBtnText.text = "Create Note";
            isRecording = false;
            // Turn off the microphone.
            microphoneManager.StopRecording();
            // Restart the PhraseRecognitionSystem and KeywordRecognizer
            microphoneManager.StartCoroutine("RestartSpeechSystem", GetComponent<KeywordManager>());
            //TransformManager.Instance.keywordRecognizer.Stop();
        }

        
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

    public void onCloseButtonPressed()
    {
        Application.Quit();
    }

}
