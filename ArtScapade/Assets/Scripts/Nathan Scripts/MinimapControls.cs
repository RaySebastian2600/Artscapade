/***
 * MinimapControls.cs
 * Version 0.9
 * By Nathan Boles
 * 
 * This script allows for the player to click upon the camera and drone icons on the minimap, switching over to using 
 * those gameobjects.
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapControls : MonoBehaviour
{
    [Tooltip("The Sprite for the UI representing the camera being off")]
    [SerializeField] Sprite cameraSpriteOff;
    [Tooltip("The Sprite for the UI representing the camera being on")]
    [SerializeField] Sprite cameraSpriteOn;
    [Tooltip("The Sprite for the UI representing the drone not being active")]
    [SerializeField] Sprite droneSpriteOff;
    [Tooltip("The Sprite for the UI representing the drone being active")]
    [SerializeField] Sprite droneSpriteOn;


    [Tooltip("The GameManager object for this scene")]
    [SerializeField] GameManager gameManager;
    [Tooltip("The CameraManager object for this scene")]
    [SerializeField] CameraManager cameraManager;
    [Tooltip("The AudioListener GameObject that will follow along with the active camera")]
    [SerializeField] GameObject audioListener;


    // Start is called before the first frame update
    void Start()
    {
        audioListener = FindObjectOfType<AudioListener>().gameObject;
        gameManager = FindObjectOfType<GameManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = cameraSpriteOn;
        audioListener.transform.SetPositionAndRotation(gameManager.GetActiveCamera().transform.position, gameManager.GetActiveCamera().transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    /// <summary>
    /// Pretty much what it sounds like on the tin. It looks for the player to click on the screen, gets the point of the mouse there. 
    /// </summary>
    private void GetInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;

            if (Physics.Raycast(ray, out rh))
            {
                //Debug.Log("Checking: " + rh.point);
                if (rh.collider.CompareTag("Click Camera"))
                {
                    ClickCamera(rh.collider.gameObject.transform.parent.gameObject);
                    Debug.Log("Camera Clicked");
                }
                else if (rh.collider.CompareTag("Click Drone"))
                    ClickDrone(rh.collider.gameObject.transform.parent.gameObject);
            }
        }
    }

    /// <summary>
    /// A method to use when you switch to a new camera. This first removes all the stuff associated with the old device whose UI is up then
    /// switches things around to the targetted camera.
    /// </summary>
    /// <param name="nCamera">The camera you wish to be the new active device</param>
    public void ClickCamera(GameObject nCamera)
    {
        //Switches Camera view to the camera that was clicked on
        //When the camera switches, the UI should swap to the appropriate camera 
        if (gameManager.GetActiveCamera().gameObject == gameManager.GetCurrentDevice().gameObject)
        {
            gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = cameraSpriteOff;
        }
        else
        {
            gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = droneSpriteOff;
            gameManager.GetCurrentDevice().GetComponent<DroneController>().setIsSelected(false);
            gameManager.GetCurrentDevice().GetComponentInChildren<BoxCollider>().enabled = true;
        }
        gameManager.GetActiveCamera().enabled = false;
        SwitchUI(nCamera);
        gameManager.SetCurrentDevice(nCamera);
        gameManager.SetActiveCamera(nCamera.GetComponent<Camera>());
        gameManager.GetActiveCamera().enabled = true;
        gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = cameraSpriteOn;
        audioListener.transform.SetPositionAndRotation(gameManager.GetActiveCamera().transform.position,gameManager.GetActiveCamera().transform.rotation);
        cameraManager.SetCamera(nCamera);
    }

    /// <summary>
    /// A method to use when you switch to a new drone. This first removes all the stuff associated with the old device whose UI is up then
    /// switches things around to the targetted drone while maintaining the camera view currently being used.
    /// </summary>
    /// <param name="nDrone">The drone you wish to be the new active device</param>
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
            gameManager.GetCurrentDevice().GetComponentInChildren<BoxCollider>().enabled = true;
        }
        SwitchUI(nDrone);
        gameManager.SetCurrentDevice(nDrone);
        gameManager.GetCurrentDevice().GetComponentInChildren<SpriteRenderer>().sprite = droneSpriteOn;
        gameManager.GetCurrentDevice().GetComponentInChildren<BoxCollider>().enabled = false;
        gameManager.GetCurrentDevice().GetComponent<DroneController>().setIsSelected(true);
        gameManager.GetCurrentDevice().GetComponent<DroneController>().ClickCheck();
    }

    /// <summary>
    /// This switches the UI over to that of the new object. 
    /// </summary>
    /// <param name="nDevice"></param>
    public void SwitchUI(GameObject nDevice)
    {
        Debug.Log("nDevice = " + nDevice.name);
        gameManager.GetCurrentDevice().GetComponentInChildren<Canvas>().gameObject.SetActive(false);
        nDevice.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
    }
}
