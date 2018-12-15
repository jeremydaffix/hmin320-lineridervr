using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour {

    public GameObject BallPrefab;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void CreateBall()
    {
        Debug.Log("CREATING BALL");
        GameObject b = GameObject.Instantiate(BallPrefab, transform.position, new Quaternion());
        b.transform.localPosition += new Vector3(0f, 0.15f, 0f);
    }
}
