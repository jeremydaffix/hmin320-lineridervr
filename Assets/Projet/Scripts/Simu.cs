using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class Simu : MonoBehaviour {


    public static Simu Instance;


    public GameObject LeftController, RightController;
    public GameObject BallPrefab;
    public CurveMesh Bezier;

    public GameObject LugePrefab;

    GameObject luge;


    VRTK_ControllerReference handL, handR;

    VRTK_ControllerEvents evL, evR;


    // Use this for initialization
    void Start()
    {

        Instance = this;

        handL =
            VRTK_ControllerReference.GetControllerReference(SDK_BaseController.ControllerHand.Left);

        handR =
            VRTK_ControllerReference.GetControllerReference(SDK_BaseController.ControllerHand.Right);

        if (LeftController != null) evL = LeftController.GetComponent<VRTK_ControllerEvents>();
        if (RightController != null) evR = RightController.GetComponent<VRTK_ControllerEvents>();

        if (evR != null)
        {
            evR.ButtonOneReleased += new ControllerInteractionEventHandler(RB1Released);
            evR.ButtonTwoReleased += new ControllerInteractionEventHandler(RB2Released);

            evL.ButtonOneReleased += new ControllerInteractionEventHandler(LB1Released);
            evL.ButtonTwoReleased += new ControllerInteractionEventHandler(LB2Released);


            evL.TriggerReleased += new ControllerInteractionEventHandler(RB1Released);
        }


        //

        Invoke("StartDemo", 0.5f); // obligé d'attendre un peu sinon la camera vrtk n'est pas chargée...

        //

        //Bezier.ControlPoints.Clear();

        foreach (Vector3 pos in TrackModel.TrackPositions) // on récupère nos boules éventuelles
        {
            Debug.Log("LOAD BALL AT " + pos);
            Vector3 newPos = pos * 30f;
            CreateBallAtPos(newPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    public void StartDemo()
    {
        // https://opengameart.org/content/jump-higher-run-faster-jump-run-miniboss-mix


        Transform rig = VRTK_DeviceFinder.PlayAreaTransform(); // objet contenant le rigidbody (multidevice normalement)

        luge = GameObject.Instantiate(LugePrefab, rig.position, Quaternion.Euler(0f,180f,0f));
        //luge.transform.SetParent(rig);

        if (TrackModel.TrackPositions.Count > 0)
        {

            Vector3 posDepart = (TrackModel.TrackPositions[0] * 30f) + new Vector3(0f, 0.5f, 1f);

            //rig.GetComponent<Rigidbody>().MovePosition(posDepart);
            rig.position = posDepart;

            luge.transform.position = posDepart; ////
            

            luge.transform.SetParent(rig.parent);
            rig.SetParent(luge.transform);
            Destroy(rig.GetComponent<Rigidbody>());
        }
    }


    public void HapticLeft(float pwr)
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(handL, pwr);
    }

    public void HapticRight(float pwr)
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(handR, pwr);
    }



    // go editor
    void RB1Released(object sender, ControllerInteractionEventArgs e)
    {

        SceneManager.LoadScene("Editor", LoadSceneMode.Single);
    }

    // reset
    void RB2Released(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("RESET");
    }

    // play
    void LB1Released(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("PLAY");
    }

    // pause
    void LB2Released(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("RESET");
    }




    void CreateBallAtPos(Vector3 pos)
    {
        Debug.Log("CREATING BALL");

        GameObject b = GameObject.Instantiate(BallPrefab, pos, new Quaternion());

        Bezier.ControlPoints.Add(b.transform);
    }
}
