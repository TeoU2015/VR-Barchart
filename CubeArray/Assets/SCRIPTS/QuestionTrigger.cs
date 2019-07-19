using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger : MonoBehaviour {

    private SteamVR_TrackedController _controller;
    private PrimitiveType _currentPrimitiveType = PrimitiveType.Sphere;
    private string[] active_q;
    private GameObject Paper;
    private GameObject QuestionText;
    public int counter;
	public string filename;

    private void OnEnable()
    {
        _controller = GetComponent<SteamVR_TrackedController>();
        _controller.TriggerClicked += HandleTriggerClicked;
        _controller.TriggerUnclicked += HandleTriggerUnclicked;
    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        EnableQuestion(true);
    }

    private void HandleTriggerUnclicked(object sender, ClickedEventArgs e)
    {
        EnableQuestion(false);
    }

    /// <summary>
    /// Enables or disable the questionboard based on boolean
    /// </summary>
    /// <param name="state">Boolean for whether Question board is enabled</param>
    private void EnableQuestion(bool state)
    {
        //GameObject Paper = GameObject.Find("Paper");
        //GameObject Question = GameObject.Find("Question");
        Paper.GetComponent<MeshRenderer>().enabled = state;
        QuestionText.GetComponent<MeshRenderer>().enabled = state;
    }

    void Start()
    {
        counter = 0;
        EnableQuestion(false);
    }
    /// <summary>
    /// Waits for input to change question
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            ChangeQuestionText();
        }
    }

    /// <summary>
    /// Changes Question board text
    /// </summary>
    void ChangeQuestionText()
    {
        QuestionText.GetComponent<TextMesh>().text = active_q[counter]; // change text from string[]
        counter = counter < 2 ? counter+1 : 0; //change counter to cycle through questions
    }

    /// <summary> Sets the questions and filename, and finds the Question Board GameObjects
    /// <remarks> Far from the best way to accomplish this, but it did the job</remarks>
    /// </summary>
	public void setQuestionTrigger(string name)
	{
        filename = name;//set the filename
        Debug.Log(filename);
        GameObject Paper = GameObject.Find("Paper"); //get the paper object
        GameObject Question = GameObject.Find("Question");//get the question text
        Questions q = new Questions(); // Get all the question strings
        switch (filename)//Just grab the one string[] based on filename
        {
            case "co2.csv":
                active_q = q.co2;
                break;
            case "education.csv":
                active_q = q.edu;
                break;
            case "grosscapital.csv":
                active_q = q.cap;
                break;
            case "health.csv":
                active_q = q.hlth;
                break;
            case "homicide.csv":
                active_q = q.hom;
                break;
            case "suicide.csv":
                active_q = q.sui;
                break;
            default:
                Debug.Log("No filename/ filename doesn't correspond to any 'Questions'.");
                break;
        }
	}
}
