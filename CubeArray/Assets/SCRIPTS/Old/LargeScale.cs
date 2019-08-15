using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LargeScale : MonoBehaviour
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
    [Range(0, 100f)]
    public float barWidth = 5f;
    [Range(0f, 100f)]
    public float spaceWidth = 2f;
    [Range(0f, 100f)]
    public float spaceRatio = 2.5f;

    void Awake()
    {
        //Instantiate other scripts
        createVis = new CreateVis();

        //Set filename for correct questions
        //qt.setFilename(filename);

        //read and get CSV values
        csv = new ReadCSV();
        List<List<object>> Data = csv.getList("hiv.csv"); ;

        //Create the Vis
        GameObject Vis = createVis.CreateChart(Data, MasterScale, spaceRatio, false);

        //Final Transformations
        Vis.transform.localScale = new Vector3(MasterScale, MasterScale, MasterScale);
        Vis.transform.position = new Vector3(0.5f, 0.01f, 1.2f);

        //Adjust labels
        GameObject countryLabels = GameObject.Find("Countries");
        countryLabels.transform.localRotation = Quaternion.Euler(90, 0, 0);
        countryLabels.transform.localPosition = new Vector3(0, 0.21f, 0);

        GameObject yearLabels = GameObject.Find("Years");
        yearLabels.transform.localRotation = Quaternion.Euler(0, 0, -90);
        yearLabels.transform.localPosition = new Vector3(0, 0.21f, 0);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}