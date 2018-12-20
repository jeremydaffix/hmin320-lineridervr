using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Pencil : MonoBehaviour {

    public GameObject BallPrefab;

    public GameObject PencilLead;

    //public CurveMesh Bezier;

    private Vector3 initPos;
    private Quaternion initRot;


	// Use this for initialization
	void Start ()
	{
	    initPos = transform.localPosition;
	    initRot = transform.localRotation;


	    //PaletteTools.Instance.Bezier.ControlPoints.Clear();;

        // BUG
        // clear quelque part qui annule ajout dans controlpoints ?

        foreach (Vector3 pos in TrackModel.TrackPositions) // on récupère nos boules éventuelles
	    {
            Debug.Log("LOAD BALL AT " + pos);
            CreateBallAtPos(pos);
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void CreateBall()
    {
        //Debug.Log("CREATING BALL");

        CreateBallAtPos(PencilLead.transform.position);

        PaletteTools.Instance.HapticRight(0.8f);
    }

    void CreateBallAtPos(Vector3 pos)
    {
        Debug.Log("CREATING BALL");

        GameObject b = GameObject.Instantiate(BallPrefab, pos, new Quaternion());

        //b.GetComponent<Ball>().Bezier = Bezier;

        PaletteTools.Instance.Bezier.ControlPoints.Add(b.transform);
    }


    public void BackToInit()
    {
        Debug.Log("BACK");
        transform.localPosition = initPos;
        transform.localRotation = initRot;

        PaletteTools.Instance.HapticLeft(0.5f);
        PaletteTools.Instance.HapticRight(0.3f);
    }
}
