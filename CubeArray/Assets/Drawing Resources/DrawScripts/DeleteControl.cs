using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteControl : MonoBehaviour {

    public SteamVR_TrackedController _controller;
    public GameObject eraser;
    private Collider coll;
    private bool CollideWithDraw;
    private bool enableDelete = false;

    //frame counter for double click bug
    private int frame = 0;

    private void OnEnable()
    {
        //Stuffs for the controller
        //_controller = GetComponent<SteamVR_TrackedController>();
        _controller.PadClicked += HandlePadClicked;
        _controller.PadUnclicked += HandlePadUnclicked;
        _controller.TriggerClicked += HandleTriggerClicked;
        _controller.TriggerUnclicked += HandleTriggerUnclicked;
        eraser.SetActive(false);
    }

    private void HandleTriggerUnclicked(object sender, ClickedEventArgs e)
    {

        if (CollideWithDraw && enableDelete)
        {
            Destroy(coll.gameObject.transform.parent.gameObject);
        }

        coll = null;
    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        coll = null;
    }

    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        if(frame != Time.frameCount)
        {
            Debug.Log("click accept");
            enableDelete = enableDelete ? false : true;
            eraser.SetActive(enableDelete);
            Debug.Log(enableDelete);
        }
        Debug.Log("click");
        

    }

    private void HandlePadUnclicked(object sender, ClickedEventArgs e)
    {
         //null
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Draw")
        {
            coll = collision;
            CollideWithDraw = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        CollideWithDraw = false;
    }
}
