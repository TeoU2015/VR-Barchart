using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerControl : MonoBehaviour {

    private SteamVR_TrackedController _controller;
    private Shader highlight_S;
    private Shader standard_S;
    private Renderer rend;
    private Collider coll;
    private bool IsCollide;
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
        standard_S = Shader.Find("Standard");
    }

    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        useHighlight = coll.gameObject.GetComponent<MeshRenderer>().material.shader == standard_S ? true : false;
        if (IsCollide)
        {
            coll.gameObject.GetComponent<MeshRenderer>().material.shader = useHighlight ? highlight_S : standard_S;
        }

        coll = null;
    }

    private void HandlePadUnclicked(object sender, ClickedEventArgs e)
    {
        //Required redundancy to ensure coll is null
        //coll would not set to null without this on occasion
        coll = null;
    }

    private void OnTriggerStay(Collider collision)
    {
        
        if (collision.gameObject.tag == "Bar")
        { 
            coll = collision;
            IsCollide = true;
        }
       
    }

    private void OnTriggerExit(Collider collision)
    {
        IsCollide = false;
    }
}
