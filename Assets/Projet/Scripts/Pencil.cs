using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour {

    public GameObject BallPrefab;

    public GameObject PencilLead;

    private Vector3 initPos;
    private Quaternion initRot;


	// Use this for initialization
	void Start ()
	{
	    initPos = transform.localPosition;
	    initRot = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void CreateBall()
    {
        Debug.Log("CREATING BALL");

        //GameObject b = GameObject.Instantiate(BallPrefab, transform.position, new Quaternion());
        GameObject b = GameObject.Instantiate(BallPrefab, PencilLead.transform.position, new Quaternion());

        //b.transform.localPosition += new Vector3(0f, 0.15f, 0f);

        //Debug.Log(transform.parent.localPosition);
        //Debug.Log(transform.parent.localRotation.eulerAngles);

        Debug.Log(PencilLead.transform.position);

    }


    public void BackToInit()
    {
        Debug.Log("BACK");
        transform.localPosition = initPos;
        transform.localRotation = initRot;
    }
}
