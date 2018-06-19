using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger : MonoBehaviour {

    private SteamVR_TrackedController _controller;
    private PrimitiveType _currentPrimitiveType = PrimitiveType.Sphere;
    public bool Room;
    public bool Table;
    public bool Hand;
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

    private void EnableQuestion(bool state)
    {
        GameObject Paper = GameObject.Find("Paper");
        GameObject Question = GameObject.Find("Question");
        Paper.GetComponent<MeshRenderer>().enabled = state;
        Question.GetComponent<MeshRenderer>().enabled = state;
    }

    void Start()
    {
        counter = 0;
        EnableQuestion(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            ChangeQuestionText();
        }
    }

    void ChangeQuestionText()
    {

        Questions newText = new Questions();
        GameObject text = GameObject.Find("Question");
        switch (filename)
        {
            case "co2.csv":
                text.GetComponent<TextMesh>().text = newText.co2[counter];
                break;
            case "education.csv":
                text.GetComponent<TextMesh>().text = newText.edu[counter];
                break;
            case "grosscapital.csv":
                text.GetComponent<TextMesh>().text = newText.cap[counter];
                break;
            case "health.csv":
                text.GetComponent<TextMesh>().text = newText.hlth[counter];
                break;
            case "homicide.csv":
                text.GetComponent<TextMesh>().text = newText.hom[counter];
                break;
            case "suicide.csv":
                text.GetComponent<TextMesh>().text = newText.sui[counter];
                break;
            default:
                Debug.Log("No filename set, fool.");
                break;
        }

        counter++;
        if (counter > 2) { counter = 0; }
    }

	public void setFilename(string name)
	{
		filename = name;
		//setBool();
	}
	public void setBool()
	{
        Debug.Log(filename);

        switch(filename)
        {
            case "co2.csv":

                break;
            case "education.csv":

                break;
            case "grosscapital.csv":

                break;
            case "health.csv":

                break;
            case "homicide.csv":

                break;
            case "suicide.csv":

                break;
        }

		if (filename == "HealthSpending.csv") {Room = true;}
		else if (filename == "GDPImport.csv") {Table = true;}
		else if (filename == "HIVData.csv") {Hand = true;}
		else{Debug.Log ("Error: not valid filename, type fool");}

	}
}
