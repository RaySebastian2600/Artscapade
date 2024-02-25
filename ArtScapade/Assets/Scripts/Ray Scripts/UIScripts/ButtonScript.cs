using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    //CameraDepth = -1;
    

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapActive()
    {      
        if (cam.depth >= 1)
        {
            cam.depth = -1;
        }
        else
        {
            cam.depth = 1;
        }
    }
}
