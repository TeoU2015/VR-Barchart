using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandScale : MonoBehaviour
{
    [HideInInspector]
    public CreateVis createVis;
    [HideInInspector]
    public ReadCSV csv;
    [HideInInspector]
    public GameObject controller;
    [HideInInspector]
    public bool HandTied;
    public QuestionTrigger qt;
    public string filename;
  
    [Range(0.01f, 1f)]
    public float scaleWL = 0.3f; //scaling value for space between bars
    [Range(0.01f, 1f)]
    public float MasterScale = 0.03f;
    [Range(0.01f, 1f)]
    public float BarWidth = 0.5f;

    void Awake()
    {
        //Instantiate other scripts
        createVis = new CreateVis();

        //initialize questions
        check_qt();

        //read and get CSV values
        csv = new ReadCSV();
        List<List<object>> Data = csv.getList(filename); ;

        //Create the Vis
        GameObject Vis = createVis.Create10x10(Data, scaleWL, BarWidth);

        //Final Transformations
        Vis.transform.localScale = new Vector3(MasterScale, MasterScale, MasterScale);
        Vis.transform.position = new Vector3(0.5f, 0.3f, 1.2f);
    }

    void Start()
    {

    }

    void check_qt()
    {
        if (qt != null)
        {
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

                HandVis.transform.localPosition = new Vector3(0.01f, 0.15f, 0.01f);
                HandVis.transform.localRotation = Quaternion.Euler(33.4f, -65.2f, -51.6f);
                HandVis.transform.parent = controller.transform;
                HandTied = true;
            }

        }
    }
}