using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Diagnostics;
using System.Text;

public class BrainMenuInputHandler : MonoBehaviour {
    public Canvas ScanMenu;
    public GameObject BrainScanCube;
    public Text ProjectNameText, ScanNameText, PatientNameText, DateTimeText;
    public List<GameObject> notes;
    public AudioSource audioSource;
    public bool isRecording = false;
    public Text recordBtnText;
    public DictationRecognizer dictationRecognizer;
    public TextMesh DictationDisplay;
    public int lineCount = 1;
    public GameObject brain;
    private StringBuilder textSoFar = new StringBuilder();

    // Use this for initialization
    void Awake () {
        // 3.a: Create a new DictationRecognizer and assign it to dictationRecognizer variable.
        dictationRecognizer = new DictationRecognizer();

        // 3.a: Register for dictationRecognizer.DictationHypothesis and implement DictationHypothesis below
        // This event is fired while the user is talking. As the recognizer listens, it provides text of what it's heard so far.
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;

        // 3.a: Register for dictationRecognizer.DictationResult and implement DictationResult below
        // This event is fired after the user pauses, typically at the end of a sentence. The full recognized string is returned here.
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;

        // 3.a: Register for dictationRecognizer.DictationComplete and implement DictationComplete below
        // This event is fired when the recognizer stops, whether from Stop() being called, a timeout occurring, or some other error.
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;

        // 3.a: Register for dictationRecognizer.DictationError and implement DictationError below
        // This event is fired when an error occurs.
        dictationRecognizer.DictationError += DictationRecognizer_DictationError;
    }
	
	// Update is called once per frame
	void Update () {/*
        if (DictationDisplay.text.Length > 30 * lineCount)
        {
            DictationDisplay.text += "\n";
        }        */
	}

    public void onCreateNoteButtonPressed()
    {
        if(!isRecording)
        {
            recordBtnText.text = "Stop Recording";
            GameObject newNote = Instantiate(Resources.Load("Prefabs/NoteParent")) as GameObject;
            brain.GetComponent<Brain>().addNote(newNote);
            DictationDisplay = newNote.GetComponentInChildren<TextMesh>();
            isRecording = true;
            StartRecording();
        }
        else
        {
            recordBtnText.text = "Create Note";
            isRecording = false;
            StopRecording();
        }

        
    }

    public void onShowHologramButtonPressed()
    {
        BrainScanCube.SetActive(true);
    }


    public void onBackButtonPressed()
    {
        dictationRecognizer.Dispose();
        this.gameObject.SetActive(false);
        ScanMenu.gameObject.SetActive(true);
    }

    public void onCloseButtonPressed()
    {/*
        #if !UNITY_EDITOR
        System.Diagnostics.Process.GetCurrentProcess().Kill();
        #endif*/
    }

    public void StartRecording()
    {
        textSoFar = new StringBuilder();
        // 3.a Shutdown the PhraseRecognitionSystem. This controls the KeywordRecognizers
        PhraseRecognitionSystem.Shutdown();
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationError += DictationRecognizer_DictationError;
        // 3.a: Start dictationRecognizer
        dictationRecognizer.Start();
    }

    public void StopRecording()
    {
        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
        PhraseRecognitionSystem.Restart();
        TransformManager.Instance.keywordRecognizer.Start();
    }

    /// <summary>
    /// This event is fired while the user is talking. As the recognizer listens, it provides text of what it's heard so far.
    /// </summary>
    /// <param name="text">The currently hypothesized recognition.</param>
    private void DictationRecognizer_DictationHypothesis(string text)
    {
        // 3.a: Set DictationDisplay text to be textSoFar and new hypothesized text
        // We don't want to append to textSoFar yet, because the hypothesis may have changed on the next event
        DictationDisplay.text = textSoFar.ToString() + " " + text + "...";
    }

    /// <summary>
    /// This event is fired after the user pauses, typically at the end of a sentence. The full recognized string is returned here.
    /// </summary>
    /// <param name="text">The text that was heard by the recognizer.</param>
    /// <param name="confidence">A representation of how confident (rejected, low, medium, high) the recognizer is of this recognition.</param>
    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        // 3.a: Append textSoFar with latest text
        textSoFar.Append(text);

        // 3.a: Set DictationDisplay text to be textSoFar
        DictationDisplay.text = textSoFar.ToString();

        if(DictationDisplay.text.Length > lineCount*30)
        {
            lineCount++;
            DictationDisplay.text += "\n";
            textSoFar = new StringBuilder();
            textSoFar.Append(DictationDisplay.text);
        }
    }

    /// <summary>
    /// This event is fired when the recognizer stops, whether from Stop() being called, a timeout occurring, or some other error.
    /// Typically, this will simply return "Complete". In this case, we check to see if the recognizer timed out.
    /// </summary>
    /// <param name="cause">An enumerated reason for the session completing.</param>
    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        // If Timeout occurs, the user has been silent for too long.
        // With dictation, the default timeout after a recognition is 20 seconds.
        // The default timeout with initial silence is 5 seconds.
        if (cause == DictationCompletionCause.TimeoutExceeded)
        {
            //Microphone.End(deviceName);

            DictationDisplay.text = "Dictation has timed out. Please press the record button again.";
            SendMessage("ResetAfterTimeout");
        }
    }

    /// <summary>
    /// This event is fired when an error occurs.
    /// </summary>
    /// <param name="error">The string representation of the error reason.</param>
    /// <param name="hresult">The int representation of the hresult.</param>
    private void DictationRecognizer_DictationError(string error, int hresult)
    {
        // 3.a: Set DictationDisplay text to be the error string
        DictationDisplay.text = error + "\nHRESULT: " + hresult;
    }

    private IEnumerator RestartSpeechSystem(KeywordManager keywordToStart)
    {
        while (dictationRecognizer != null && dictationRecognizer.Status == SpeechSystemStatus.Running)
        {
            yield return null;
        }

        keywordToStart.StartKeywordRecognizer();
    }

}
