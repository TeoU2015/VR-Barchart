using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TableScale : MonoBehaviour
{
    [HideInInspector]
    public CreateVis createVis;
    [HideInInspector]
    public ReadCSV csv;
    public QuestionTrigger qt;
    public string filename;
  
    [Range(0.01f, 1f)]
    public float scaleWL = 0.3f; //scaling value for space between bars
    [Range(0.01f, 1f)]
    public float MasterScale = 0.08f;
    [Range(0.01f, 1f)]
    public float BarWidth = 0.5f;

    void Awake()
    {
        //Instantiate other scripts
        createVis = new CreateVis();

        //Set filename for correct questions
        //qt.setFilename(filename);

        //read and get CSV values
        csv = new ReadCSV();
        List<List<object>> Data = csv.getList(filename); ;

        //Create the Vis
        GameObject Vis = createVis.Create10x10(Data, scaleWL, BarWidth);

        //Final Transformations
        Vis.transform.localScale = new Vector3(MasterScale, MasterScale, MasterScale);
        Vis.transform.parent = GameObject.Find("Stand").transform;
        Vis.transform.localPosition = Vector3.zero ;
        Vis.transform.localPosition = new Vector3(-0.528f,0.64f,-0.528f);//magic numbers galore!
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}