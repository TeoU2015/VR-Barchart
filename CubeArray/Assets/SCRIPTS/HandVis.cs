using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandVis : MonoBehaviour
{
    [HideInInspector]
    public CreateVis createVis;
    [HideInInspector]
    public ReadCSV csv;
    [HideInInspector]
    public GameObject controller;
    [HideInInspector]
    public bool legoMode = true;
    public bool HandTied;
    public QuestionTrigger qt;
    private string filename;
    public StudyTracker Tracker;

    [Range(0.01f, 100f)]
    public float MasterScale = 0.3f;
    [Range(0f, 100f)]
    public float spaceRatio = 2.5f;

    void Awake()
    {
        
    }

    void Start()
    {
        //Instantiate other scripts
        createVis = new CreateVis();

        //get filename
        filename = Tracker.filename;

        //initialize questions
        check_qt();

        //read and get CSV values
        csv = new ReadCSV();
        List<List<object>> Data = csv.getList(filename); ;

        //Create the Vis
        GameObject Vis = createVis.CreateChart(Data, MasterScale, spaceRatio, legoMode);

        //Final Transformations
        Vis.transform.localScale = new Vector3(MasterScale, MasterScale, MasterScale);
        Vis.transform.position = (legoMode) ? Vis.transform.position : new Vector3(0.5f, 0.3f, 1.2f);
    }

    void check_qt()
    {
        if (qt != null)
        {
            Debug.Log("check_qt" + filename);
            qt.setQuestionTrigger(filename);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!HandTied)
        {
            controller = GameObject.FindWithTag("GameController");

            if (controller != null)
            {
                GameObject HandVis = GameObject.Find("Viz");

                HandVis.transform.localPosition = (legoMode) ? new Vector3(-0.055f, -0.011f, -0.055f) : new Vector3(0.01f, 0.15f, 0.01f);
                HandVis.transform.localRotation = (legoMode) ? HandVis.transform.localRotation : Quaternion.Euler(33.4f, -65.2f, -51.6f);
                HandVis.transform.parent = controller.transform;
                HandTied = true;
            }

        }
    }
}