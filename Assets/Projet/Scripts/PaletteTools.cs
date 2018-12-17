using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PaletteTools : MonoBehaviour
{

    public PaletteTools Instance;

    private int cptScreen = 0;


	// Use this for initialization
	void Start ()
	{

	    Instance = this;
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
}
