using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Three : MonoBehaviour
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
    public float MasterScale = 0.015f;
    [Range(0.01f, 1f)]
    public float BarWidth = 0.5f;
    private List<List<object>> Data;

    void Awake()
    {
        //Instantiate other scripts
        createVis = new CreateVis();

        //Set filename for correct questions
        qt.setFilename(filename);

        //read and get CSV values
        csv = new ReadCSV();
        Data = csv.getList(filename); ;

        //Create the Vis
        MakeRoom();
        MakeTable();
        MakeHand();
    }

    void MakeRoom()
    {
        //Create the Vis
        GameObject Vis = createVis.Create10x10(Data, scaleWL, BarWidth);

        //Final Transformations
        Vis.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        Vis.transform.position = new Vector3(0.5f, 0.01f, 1.2f);

        //Adjust labels
        GameObject countryLabels = GameObject.Find("Countries");
        countryLabels.transform.localRotation = Quaternion.Euler(90, 0, 0);
        countryLabels.transform.localPosition = new Vector3(0, 0.21f, 0);

        GameObject yearLabels = GameObject.Find("Years");
        yearLabels.transform.localRotation = Quaternion.Euler(0, 0, -90);
        yearLabels.transform.localPosition = new Vector3(0, 0.21f, 0);
    }
    void MakeTable()
    {
        //Create the Vis
        GameObject Vis = createVis.Create10x10(Data, scaleWL, BarWidth);
        GameObject stand = GameObject.CreatePrimitive(PrimitiveType.Cube);
        stand.name = "Stand";
        stand.transform.localPosition = Vector3.zero;
        stand.transform.localScale = new Vector3(0.64f, 1.6f, 0.64f);
      

        //Final Transformations
        Vis.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        Vis.transform.parent = GameObject.Find("Stand").transform;
        Vis.transform.localPosition = Vector3.zero;
        Vis.transform.localPosition = new Vector3(-0.528f, 0.64f, -0.528f);//magic numbers galore!

        stand.transform.localPosition = new Vector3(0.994f, 0f, 0f);
    }
    void MakeHand()
    {
        //Create the Vis
        GameObject Vis = createVis.Create10x10(Data, scaleWL, BarWidth);

        //Final Transformations
        Vis.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
        Vis.transform.position = new Vector3(0.712f, 1.126f, -1.2f);
    }
}