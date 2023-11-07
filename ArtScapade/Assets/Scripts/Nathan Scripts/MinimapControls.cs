using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapControls : MonoBehaviour
{
    [SerializeField] Sprite cameraSpriteOff;
    [SerializeField] Sprite cameraSpriteOn;
    [SerializeField] Sprite droneSpriteOff;
    [SerializeField] Sprite droneSpriteOn;

    [SerializeField] GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = cameraSpriteOn;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;

            if (Physics.Raycast(ray, out rh))
            {
                if (rh.collider.CompareTag("Click Camera"))
                    ClickCamera(rh.collider.gameObject.transform.parent.gameObject);
                else if (rh.collider.CompareTag("Click Drone"))
                    ClickDrone(rh.collider.gameObject.transform.parent.gameObject);
            }
        }
    }

    public void ClickCamera(GameObject nCamera)
    {
        //Switches Camera view to the camera that was clicked on
        //When the camera switches, the UI should swap to the appropriate camera 
        if (gameManager.GetActiveCamera().gameObject == gameManager.GetCurrentDevice().gameObject)
        {
            gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = cameraSpriteOff;
            gameManager.GetActiveCamera().enabled = false;
        }
        else
        {
            gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = droneSpriteOff;
            gameManager.GetCurrentDevice().GetComponent<DroneController>().setIsSelected(false);
        }
        SwitchUI(nCamera);
        gameManager.SetCurrentDevice(nCamera);
        gameManager.SetActiveCamera(nCamera.GetComponent<Camera>());
        gameManager.GetActiveCamera().enabled = true;
        gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = cameraSpriteOn;


    }

    public void ClickDrone(GameObject nDrone)
    {
        if (gameManager.GetActiveCamera().gameObject == gameManager.GetCurrentDevice().gameObject)
        {
            gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = cameraSpriteOff;
        }
        else
        {
            gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = droneSpriteOff;
            gameManager.GetCurrentDevice().GetComponent<DroneController>().setIsSelected(false);
        }
        SwitchUI(nDrone);
        gameManager.SetCurrentDevice(nDrone);
        gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = droneSpriteOn;
        gameManager.GetCurrentDevice().GetComponent<DroneController>().setIsSelected(true);
    }

    public void SwitchUI(GameObject nDevice)
    {
        gameManager.GetCurrentDevice().GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        nDevice.GetComponentInChildren<Canvas>().gameObject.SetActive(true);
    }
}
