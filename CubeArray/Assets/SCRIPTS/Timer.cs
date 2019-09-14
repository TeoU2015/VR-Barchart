using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public string timeFile = "TimeFile.csv";
    private float startTime;
    private string dataName;
    private float elapsedTime;
    private float stopTime;
    private int questionCount = 0;
    private StudyTracker tracker;

    // Use this for initializationd
    void Start () {
        tracker = gameObject.GetComponent<StudyTracker>();
        dataName = tracker.filename;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire3"))
        {
            StartTimer();
            questionCount++;
        }
        if (Input.GetButtonDown("Jump"))
        {
            StopTimer();
            logDataFull();
        }
    }

    void StopTimer(){
        stopTime = Time.time;
        elapsedTime = stopTime - startTime;
        Debug.Log(elapsedTime);
    }

    void StartTimer(){
        startTime = Time.time;
    }

    void logDataFull()
    {

        string header = "Participant ID,Chart Type,Data Name, Chart Number, Question number, Task number, Time\n";

        string format = "{0},{1},{2},{3},{4},{5},{6}\n";



        if (!System.IO.File.Exists(timeFile))

            System.IO.File.WriteAllText(timeFile, header);

        string output = string.Format(format, tracker.participantID, tracker.scene.name, dataName, tracker.chartNumber, questionCount,  (((tracker.chartNumber-1)*6)+questionCount) , elapsedTime);

        System.IO.File.AppendAllText(timeFile, output);
    }
}
