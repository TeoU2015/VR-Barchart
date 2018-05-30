using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineRenderer : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    public bool Hand;
    [Range(0.01f, 0.1f)]
    public float lineWidth = 0.1f;
    public Material lmat;


    private MeshCollider currMesh;
    private GameObject currObj;
    private GameObject Vis;
    private LineRenderer currLine;
    private int numClicks = 0;

    //Find the vis on start
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
            currObj = new GameObject();
            currObj.tag = "Draw";
            currLine = currObj.AddComponent<LineRenderer>();
            currLine.material = lmat;

            currLine.SetWidth(lineWidth,lineWidth);

            numClicks = 0;
        } else if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            currLine.SetVertexCount(numClicks + 1);
            currLine.SetPosition(numClicks, trackedObj.transform.position);
            numClicks++;
        }
		
	}
}
