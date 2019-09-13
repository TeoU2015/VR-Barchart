using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TableVis : MonoBehaviour
{
    [HideInInspector]
    public CreateVis createVis;
    [HideInInspector]
    public bool legoMode = true;
    public ReadCSV csv;
    public QuestionTrigger qt;
    private string filename;
    public StudyTracker Tracker;

    [Range(0.01f, 100f)]
    public float MasterScale = 0.8f;
    [Range(0f, 100f)]
    public float spaceRatio = 2.5f;

    void Awake()
    {
        //Instantiate other scripts
        createVis = new CreateVis();

        //get filename
        filename = Tracker.filename;

        //Set filename for correct questions
        //qt.setFilename(filename);

        //initialize questions
        check_qt();

        //read and get CSV values
        csv = new ReadCSV();
        List<List<object>> Data = csv.getList(filename); ;

        //Create the Vis
        GameObject Vis = createVis.CreateChart(Data, MasterScale, spaceRatio, legoMode );

        //Final Transformations
        Transform stand = GameObject.Find("Stand").transform;
        Vis.transform.localScale = new Vector3(MasterScale, MasterScale, MasterScale);
        Vis.transform.localPosition = new Vector3(-MasterScale/2f, stand.localScale.y/2f, -MasterScale/2f);//magic numbers galore! place in center of VR space
        Vis.transform.parent = stand; //Attach to Pillar
    }

    void check_qt()
    {
        if (qt != null)
        {
            qt.setQuestionTrigger(filename);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}