using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteControl : MonoBehaviour {

    private SteamVR_TrackedController _controller;
    private Collider coll;
    private bool CollideWithDraw;

    private void OnEnable()
    {
        //Stuffs for the controller
        _controller = GetComponent<SteamVR_TrackedController>();
        _controller.PadClicked += HandlePadClicked;
        _controller.PadUnclicked += HandlePadUnclicked;
    }

    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        if (CollideWithDraw)
        {
            Destroy(coll.gameObject);
        }

        coll = null;
    }

    private void HandlePadUnclicked(object sender, ClickedEventArgs e)
    {
         coll = null;
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
