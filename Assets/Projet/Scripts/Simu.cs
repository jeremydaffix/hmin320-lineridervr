using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class Simu : MonoBehaviour {


    public static Simu Instance;


    public GameObject LeftController, RightController;


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
        }
    }

    // Update is called once per frame
    void Update()
    {
        

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

        SceneManager.LoadScene("Editor");
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
}
