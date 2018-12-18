using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour {

    private Vector3 initPos;
    private Quaternion initRot;


    // Use this for initialization
    void Start () {

        initPos = transform.localPosition;
        initRot = transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Cut()
    {

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
