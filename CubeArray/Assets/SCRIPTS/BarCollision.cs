using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarCollision : MonoBehaviour {

    private bool setInvis;
    private Color baseColor;
    private Color lowOpacity;

    private void Start()
    {
        baseColor = this.GetComponent<MeshRenderer>().material.color;
        lowOpacity = baseColor;
        lowOpacity.a = 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.tag == "armRadius")
        {
            setInvis = true;
            this.GetComponent<MeshRenderer>().material.color = lowOpacity;
            ChangeChildren(0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (setInvis && other.gameObject.tag == "armRadius")
        {
            this.GetComponent<MeshRenderer>().material.color = baseColor;
            ChangeChildren(1f);
            setInvis = false;
        }
    }

    private void ChangeChildren(float alpha)
    {
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
        Color newColor;
        foreach(MeshRenderer child in children)
        {
            if (child.material.color != lowOpacity)
            {
                newColor = child.material.color;
                newColor.a = alpha;
                child.material.color = newColor;
            }
        }
    }
}
