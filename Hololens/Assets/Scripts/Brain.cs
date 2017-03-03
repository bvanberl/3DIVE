using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    public List<GameObject> notes;

	// Use this for initialization
	void Start () {
        notes = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Add a list of notes to the scene for the current brain scan.
    public void initNotes(string[] messages, Vector3[] positions)
    {
        for(int i = 0; i < messages.Length; i++)
        {
            GameObject newNote = Instantiate(Resources.Load("Prefabs/NoteParent")) as GameObject;
            notes.Add(newNote);
            notes[i].GetComponentInChildren<TextMesh>().text = messages[i];
            notes[i].transform.position = positions[i];
            //notes[i].transform.parent = this.gameObject.transform;
        }
    }

    // Adds new note to brain 
    public void addNote(GameObject newNote)
    {
        notes.Add(newNote);
    }
}
