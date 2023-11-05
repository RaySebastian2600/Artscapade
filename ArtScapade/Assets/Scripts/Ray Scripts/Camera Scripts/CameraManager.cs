using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] allCameras;

    public int currentCamera;

    public void Awake()
    {

        for (var i = 1; i < allCameras.Length; ++i)
            allCameras[i].enabled = false;
    }

    void IncCamera()
    {
        allCameras[currentCamera].enabled = false;
        currentCamera = ++currentCamera % allCameras.Length;
        allCameras[currentCamera].enabled = true;
    }

    void DecCamera()
    {
        allCameras[currentCamera].enabled = false;
        currentCamera = --currentCamera < 0 ? allCameras.Length - 1 : currentCamera;
        allCameras[currentCamera].enabled = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            IncCamera();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DecCamera();

        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            IncCamera();
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            DecCamera();
        }
    }
   
}
