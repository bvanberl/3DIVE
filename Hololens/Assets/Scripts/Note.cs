using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Note : MonoBehaviour {

    public string content;
    public DictationRecognizer dictRcg;
    // Using an empty string specifies the default microphone. 
    private static string deviceName = string.Empty;
    private int samplingRate = 44100;
    private const int messageLength = 10;
    public AudioSource audioSource;
    public MicrophoneManager microphoneManager;

    // Use this for initialization
    void Awake () {
        // Turn the microphone on, which returns the recorded audio.
        audioSource.clip = microphoneManager.StartRecording();

        /*
        PhraseRecognitionSystem.Shutdown();
        dictRcg = new DictationRecognizer();
        dictRcg.DictationResult += DictationRecognizer_DictationResult;
        dictRcg.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dictRcg.Start();
        AudioClip recorded = Microphone.Start(deviceName, false, messageLength, samplingRate);*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        this.GetComponent<TextMesh>().text = text;
        dictRcg.Stop();
        PhraseRecognitionSystem.Restart();
    }


    private void DictationRecognizer_DictationHypothesis(string text)
    {
        this.GetComponent<TextMesh>().text = text;
    }
}
