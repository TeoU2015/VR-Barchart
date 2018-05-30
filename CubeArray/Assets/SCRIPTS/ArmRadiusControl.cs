using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRadiusControl : MonoBehaviour {

    public SteamVR_TrackedController _controller;
    public SteamVR_TrackedObject _hmd;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float radius = getDistance(_controller.transform.position, _hmd.transform.position);
        this.transform.localScale = new Vector3(radius*2, radius*2, radius*2);
	}

    //basic pythagorean for x and z axis
    //c=sqrt(a^2 b^2)
    private float getDistance(Vector3 p1, Vector3 p2)
    {
        float x = Mathf.Abs(p1.x - p2.x);
        float z = Mathf.Abs(p1.z - p2.z);

        return Mathf.Sqrt( (x*x) + (z*z) );
    }
        
}
