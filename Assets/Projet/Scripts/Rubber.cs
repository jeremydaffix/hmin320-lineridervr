using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubber : MonoBehaviour {

    private Vector3 initPos;
    private Quaternion initRot;

    public bool RubberEnabled = false;


    // Use this for initialization
    void Start () {

        initPos = transform.localPosition;
        initRot = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void EnableRubber()
    {
        RubberEnabled = true;
    }

    public void Erase()
    {

    }


    public void BackToInit()
    {
        //Debug.Log("BACK");

        transform.localPosition = initPos;
        transform.localRotation = initRot;

        RubberEnabled = false;

        PaletteTools.Instance.HapticLeft(0.5f);
        PaletteTools.Instance.HapticRight(0.3f);
    }
}
