using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManagerEX : MonoBehaviour {

    public Material lmat;
    public SteamVR_TrackedObject trackedObj;
    public GameObject DrawSphere;
    public Transform DrawTip;
    public Transform DrawBase;
    public bool Hand;


    //variable used for a lilne in one axis
    private MeshLineRenderer currLine;
    private MeshCollider currMesh;
    private GameObject currObj;
    private GameObject Vis;
    int numClicks = 0;

    //Master obj
    private GameObject masterObj;

    //EX variables - 2nd line in another axis
    public Transform DrawTipEX;
    public Transform DrawBaseEX;
    private MeshLineRenderer currLineEX;
    private GameObject currObjEX;

    //EX TURBO variables - 3rd line in the 3rd axis
    public Transform DrawTipEXTURBO;
    public Transform DrawBaseEXTURBO;
    private MeshLineRenderer currLineEXTURBO;
    private GameObject currObjEXTURBO;


    private void Start()
    {
        if (Hand)
        {
            Vis = GameObject.Find("Viz");
        }
    }
    
    // Update is called once per frame
    void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Master Line Object
            masterObj = new GameObject();
            masterObj.tag = "Draw";

            currObj = new GameObject(); //just like make new object
            currObj.tag = "Draw";
            currObj.transform.parent = masterObj.transform;
            currLine = currObj.AddComponent<MeshLineRenderer>();

            currObjEX = new GameObject(); //just like make new object
            currObjEX.tag = "Draw";
            currObjEX.transform.parent = masterObj.transform;
            currLineEX = currObjEX.AddComponent<MeshLineRenderer>();

            currObjEXTURBO = new GameObject(); //just like make new object
            currObjEXTURBO.tag = "Draw";
            currObjEXTURBO.transform.parent = masterObj.transform;
            currLineEXTURBO = currObjEXTURBO.AddComponent<MeshLineRenderer>();

            currLine.lmat = lmat;
            currLine.setWidth(.1f); //static width
            currLineEX.lmat = lmat;
            currLineEX.setWidth(.1f); //static width
            currLineEXTURBO.lmat = lmat;
            currLineEXTURBO.setWidth(.1f); //static width

            numClicks = 0;
        }
        else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.AddPoint(DrawBase.position, DrawTip.position); // while touch keep adding point
            currLineEX.AddPoint(DrawBaseEX.position, DrawTipEX.position); // while touch keep adding point experimental
            currLineEXTURBO.AddPoint(DrawBaseEXTURBO.position, DrawTipEXTURBO.position); // while touch keep adding point experimental

            numClicks++;
        }           
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            currMesh = currObj.AddComponent<MeshCollider>();
            currMesh.sharedMesh = currObj.GetComponent<MeshFilter>().mesh;

            if (Hand)
            {
                masterObj.transform.parent = Vis.transform;
            }
            
        }
    }
}
