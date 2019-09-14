using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudyTracker : MonoBehaviour {
    public string filename;
    public int chartNumber;
    public string participantID;
    public GameObject Manager;
    public bool carmortality;
    public bool co2;
    public bool grosscapital;
    public bool education;
    public bool agriculturalland;
    public bool military;
    public bool suicide;
    public string QuestionOrder;
    public Scene scene;
    // Use this for initialization
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        if (carmortality)
        {
            filename = "carmortality";
        }
        else if (co2)
        {
            filename = "co2";
        }
        else if (grosscapital)
        {
            filename = "grosscapital";
        }
        else if (education)
        {
            filename = "education";
        }
        else if (agriculturalland)
        {
            filename = "agriculturalland";
        }
        else if (military)
        {
            filename = "military";
        }
        else if (suicide)
        {
            filename = "suicide";
        }
        else
        {
            Debug.LogWarning("File not selected");
        }
    }

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
