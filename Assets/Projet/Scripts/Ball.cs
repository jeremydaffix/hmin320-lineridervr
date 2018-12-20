using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //public CurveMesh Bezier;

    private bool collRubber = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider c)
    {
        //Debug.Log("ENTER " + c.name);

        if (c.name.Equals("Rubber") && c.GetComponent<Rubber>().RubberEnabled)
        {
            GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f);

            collRubber = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        //Debug.Log("EXIT");

        if (c.name.Equals("Rubber") && c.GetComponent<Rubber>().RubberEnabled)
        {
            GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f);

            collRubber = false;

            PaletteTools.Instance.Bezier.ControlPoints.Remove(transform);
            Destroy(gameObject); // bim

            PaletteTools.Instance.HapticRight(1.0f);
        }
    }
}
