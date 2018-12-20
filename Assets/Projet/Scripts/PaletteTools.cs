using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class PaletteTools : MonoBehaviour
{

    public static PaletteTools Instance;


    public GameObject LeftController, RightController;
    public CurveMesh Bezier;


    private int cptScreen = 0;

    VRTK_ControllerReference handL, handR;

    VRTK_ControllerEvents evL, evR;


    // Use this for initialization
    void Start ()
	{

	    Instance = this;

	    handL =
	        VRTK_ControllerReference.GetControllerReference(SDK_BaseController.ControllerHand.Left);

	    handR =
	        VRTK_ControllerReference.GetControllerReference(SDK_BaseController.ControllerHand.Right);

        if(LeftController != null) evL = LeftController.GetComponent<VRTK_ControllerEvents>();
	    if (RightController != null) evR = RightController.GetComponent<VRTK_ControllerEvents>();

	    if (evR != null)
	    {
            evR.ButtonOneReleased += new ControllerInteractionEventHandler(B1Released); ;
	    }
    }
	
	// Update is called once per frame
	void Update ()
	{

	    int imageWidth = 1024;
	    bool saveAsJPEG = true;

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("360 SCREENSHOT");

	        byte[] bytes = I360Render.Capture(imageWidth, saveAsJPEG);
	        if (bytes != null)
	        {
	            string path = Path.Combine(Application.persistentDataPath, "360render" + cptScreen + (saveAsJPEG ? ".jpeg" : ".png"));
	            File.WriteAllBytes(path, bytes);
	            Debug.Log("360 render saved to " + path);

	            cptScreen++;
	        }
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




    void B1Released(object sender, ControllerInteractionEventArgs e)
    {

        // avant de changer de scène on enregistre la track à afficher

        TrackModel.TrackPositions.Clear();

        foreach (Transform t in Bezier.ControlPoints)
        {
            TrackModel.TrackPositions.Add(t.position);
        }

        SceneManager.LoadScene("Simulator");
    }
}
