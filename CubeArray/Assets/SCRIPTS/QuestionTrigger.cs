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
        if (Room) { text.GetComponent<TextMesh>().text = newText.Room_Q[counter]; }
        else if (Table) { text.GetComponent<TextMesh>().text = newText.Table_Q[counter]; }
        else if (Hand){ text.GetComponent<TextMesh>().text = newText.Hand_Q[counter]; }
        else { Debug.Log("Error, no scale boolean set, what are you doing fool?"); }

        counter++;
        if (counter > 2) { counter = 0; }
    }
}
