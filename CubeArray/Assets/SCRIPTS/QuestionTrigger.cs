using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger : MonoBehaviour {

    private SteamVR_TrackedController _controller;
    private PrimitiveType _currentPrimitiveType = PrimitiveType.Sphere;
    private Questions.QuestionSet active_q;
    private Questions q = new Questions(); // Get all the question strings
    private char[] Order;
    private string[] SortedQuestions = new string[6];
    public GameObject Paper;
    public GameObject QuestionText;
    public StudyTracker Tracker;
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

    void ParseOrder(string input)
    {
        if (input.Length == 6)
        {
            Order = input.ToCharArray();
            for(int i = 0; i <6; i++)
            {
                if ((Order[i] == 'R') | (Order[i] == 'r'))
                {
                    SortedQuestions[i] = (i < 3) ? active_q.r1 : active_q.r2;
                }
                else if ((Order[i] == 'O') | (Order[i] == 'o'))
                {
                    SortedQuestions[i] = (i < 3) ? active_q.o1 : active_q.o2;
                }
                else if((Order[i] == 'C') | (Order[i] == 'c'))
                {
                    SortedQuestions[i] = (i < 3) ? active_q.c1 : active_q.c2;
                }
            }
        }
        else
        {
            Debug.LogWarning("Order is not equal to 6");
        }
    }

    void Start()
    {
        counter = 0;
        filename = Tracker.filename;
        if (Tracker.scene.name != "PhysicalLego")
        {
            EnableQuestion(false);
        }
        else {
            EnableQuestion(true);
        }

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
        Debug.Log(SortedQuestions[counter]);
        QuestionText.GetComponent<TextMesh>().text = SortedQuestions[counter]; // change text from string[]
        counter = counter < 5 ? counter+1 : 0; //change counter to cycle through questions
    }

    /// <summary> Sets the questions and filename, and finds the Question Board GameObjects
    /// <remarks> Far from the best way to accomplish this, but it did the job</remarks>
    /// </summary>
	public void setQuestionTrigger(string name)
	{
        filename = name;//set the filename
        GameObject Paper = GameObject.Find("Paper"); //get the paper object
        GameObject Question = GameObject.Find("Question");//get the question text
        Debug.Log(Paper);
        switch (filename)//Just grab the one string[] based on filename
        {
            case "co2":
                active_q = q.co2;
                break;
            case "education":
                active_q = q.education;
                break;
            case "grosscapital":
                active_q = q.grosscapital;
                break;
            case "health":
                active_q = q.health;
                break;
            case "homicide":
                active_q = q.homicide;
                break;
            case "suicide":
                active_q = q.suicide;
                break;
            case "agriculturalland":
                active_q = q.agriculturalland;
                break;
            case "military":
                active_q = q.military;
                break;
            case "carmortality":
                active_q = q.carmortality;
                break;
            default:
                Debug.Log("No filename/ filename doesn't correspond to any 'Questions'.");
                break;
        }
        Debug.Log("QO " + Tracker.QuestionOrder);
        ParseOrder(Tracker.QuestionOrder);

    }
}
