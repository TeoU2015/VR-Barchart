using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physical : MonoBehaviour {

    public QuestionTrigger qt;
    private string filename;
    public StudyTracker Tracker;

    // Use this for initialization
    void Start () {

        filename = Tracker.filename;
        qt.setQuestionTrigger(filename);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
