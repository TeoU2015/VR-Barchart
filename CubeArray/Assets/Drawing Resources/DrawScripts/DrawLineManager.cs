using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

    public Material lmat;
    public SteamVR_TrackedObject trackedObj; //dont need?
    public GameObject DrawSphere;
    public Transform DrawTip;
    public Transform DrawBase;
    public Transform DrawTipEX;
    public Transform DrawBaseEX;
    public bool Hand;

    private MeshLineRenderer currLine;
    private MeshCollider currMesh;
    private GameObject currObj;
    private GameObject Vis;
    int numClicks = 0;

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
            currObj = new GameObject(); //just like make new object
            currObj.tag = "Draw";
            currLine = currObj.AddComponent<MeshLineRenderer>();

            currLine.lmat = lmat;
            currLine.setWidth(.1f); //static width

            numClicks = 0;
        }
        else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.AddPoint(DrawBase.position, DrawTip.position); // while touch keep adding point
            currLine.AddPoint(DrawBaseEX.position, DrawTipEX.position); // while touch keep adding point experimental

            numClicks++;
        }           
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            currMesh = currObj.AddComponent<MeshCollider>();
            currMesh.sharedMesh = currObj.GetComponent<MeshFilter>().mesh;

            if (Hand)
            {
                currObj.transform.parent = Vis.transform;
            }
            
        }
    }
}
