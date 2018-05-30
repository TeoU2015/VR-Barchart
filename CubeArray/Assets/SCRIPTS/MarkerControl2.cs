using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerControl2 : MonoBehaviour {

    private SteamVR_TrackedController _controller;
    private Shader highlight_S;
    private Shader standard_S;
    private Shader transparent_S;
    private Renderer rend;
    private Collider coll;
    private Color transparent_h = new Color(0, 1, 0, 0.25f);
    private Color opaque_h = new Color(0, 1, 0, 1);
    private bool IsCollide;
    private bool IsClick;
    private bool useHighlight;

    private void OnEnable()
    {
        //Stuffs for the controller
        _controller = GetComponent<SteamVR_TrackedController>();
        _controller.PadClicked += HandlePadClicked;
        _controller.PadUnclicked += HandlePadUnclicked;

        //Stuffs for changing shaders
        rend = GetComponent<Renderer>();
        highlight_S = Shader.Find("Outlined/Uniform");
        transparent_S = Shader.Find("Outlined/Transparent");
        standard_S = Shader.Find("Standard");
    }

    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        IsClick = true; //set click bool
        coll.gameObject.GetComponent<MeshRenderer>().material.shader = useHighlight ? highlight_S : standard_S; // Change shader
        useHighlight = !useHighlight;
    }

    private void HandlePadUnclicked(object sender, ClickedEventArgs e)
    {
        //Not sure if need this any more
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bar")
        {
            //below if is bug fix for colliding with two objects at same time
            if (coll !=null && collision != coll) //if prev not same as current, and not null
            {
                coll.gameObject.GetComponent<MeshRenderer>().material.shader = !useHighlight ? highlight_S : standard_S; //revert previous collision before continuing
            }

            coll = collision; // set new collision object
            useHighlight = coll.gameObject.GetComponent<MeshRenderer>().material.shader == standard_S ? true : false;//set bool for which shader to use
        }
    }

    private void OnTriggerStay(Collider collision)
    {

        if (collision.gameObject.tag == "Bar" && !IsClick)
        {
            coll.gameObject.GetComponent<MeshRenderer>().material.shader = transparent_S; // Change to transparent outline shader
            IsCollide = true;
        }
       
    }

    private void OnTriggerExit(Collider collision)
    {
        if (IsClick)
        {
            IsClick = false;    // reset bool on exit
            //coll.gameObject.GetComponent<MeshRenderer>().material.shader = useHighlight ? highlight_S : standard_S; // Change shader

            //do nothing else and keep new shader
        }
        else if (coll != null)  // if condition removes errors with non bar objects, QoL addition
        {
            coll.gameObject.GetComponent<MeshRenderer>().material.shader = !useHighlight ? highlight_S : standard_S; // revert to initial shader
        }

        //below added to fix bug when colliding with two objects at same time
        if (collision == coll)  //check that the object you're exiting is same as one you entered
        { 
            coll = null;
        }
        IsCollide = false;  //reset collision bool
    }
}
