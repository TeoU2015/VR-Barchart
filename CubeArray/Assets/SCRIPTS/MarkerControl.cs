using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerControl : MonoBehaviour {

    private SteamVR_TrackedController _controller;
    private PrimitiveType _currentPrimitiveType = PrimitiveType.Sphere;
    private Shader highlight_S;
    private Shader standard_S;
    private Renderer rend;
    private Collider coll;
    private bool IsCollide;
    private bool useHighlight;
    private bool waitForUnpress = true;
    public bool Room;
    public bool Table;
    public bool Hand;
    public bool Draw;
    public int counter;

    private void OnEnable()
    {
        //Stuffs for the controller
        _controller = GetComponent<SteamVR_TrackedController>();
        _controller.TriggerClicked += HandleTriggerClicked;
        _controller.TriggerUnclicked += HandleTriggerUnclicked;
        _controller.PadClicked += HandlePadClicked;
        _controller.PadUnclicked += HandlePadUnclicked;

        //Stuffs for changing shaders
        rend = GetComponent<Renderer>();
        highlight_S = Shader.Find("Outlined/Uniform");
        standard_S = Shader.Find("Standard");
    }

    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        useHighlight = coll.gameObject.GetComponent<MeshRenderer>().material.shader == standard_S ? true : false;
        if (IsCollide)
        {
            coll.gameObject.GetComponent<MeshRenderer>().material.shader = useHighlight ? highlight_S : standard_S;
        }
        Debug.Log("clicked" + coll);
    }

    private void HandlePadUnclicked(object sender, ClickedEventArgs e)
    {
        coll = null;
        Debug.Log("unclicked" + coll);
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

    private void OnTriggerStay(Collider collision)
    {
        IsCollide = true;
        if (collision.gameObject.tag == "Bar")
        { 
            coll = collision;
        }
       
    }
    private void OnTriggerExit(Collider collision)
    {
        IsCollide = false;
    }

    void MarkBar()
    {

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
