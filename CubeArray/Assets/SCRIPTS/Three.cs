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

    [Range(0.01f, 100f)]
    public float MasterScale = 0.3f;
    [Range(0f, 100f)]
    public float spaceRatio = 2.5f;
    private List<List<object>> Data;

    void Awake()
    {
        //Instantiate other scripts
        createVis = new CreateVis();

        //Set filename for correct questions
        //qt.setFilename(filename);

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
        GameObject Vis = createVis.CreateChart(Data, MasterScale, spaceRatio);

        //Final Transformations
        Vis.transform.localScale = new Vector3(8f, 8f, 8f);
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
        GameObject Vis = createVis.CreateChart(Data, MasterScale, spaceRatio);
        GameObject stand = GameObject.CreatePrimitive(PrimitiveType.Cube);
        stand.name = "Stand";
        stand.transform.localPosition = Vector3.zero;
        stand.transform.localScale = new Vector3(0.64f, 1.6f, 0.64f);
      

        //Final Transformations
        Vis.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        Vis.transform.parent = GameObject.Find("Stand").transform;
        Vis.transform.localPosition = Vector3.zero;
        Vis.transform.localPosition = new Vector3(-0.528f, 0.64f, -0.528f);//magic numbers galore!

        stand.transform.localPosition = new Vector3(0.994f, 0f, 0f);
    }
    void MakeHand()
    {
        //Create the Vis
        GameObject Vis = createVis.CreateChart(Data, MasterScale, spaceRatio);

        //Final Transformations
        Vis.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
        Vis.transform.position = new Vector3(0.712f, 1.126f, -1.2f);
    }
}