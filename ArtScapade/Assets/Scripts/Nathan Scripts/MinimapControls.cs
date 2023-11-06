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
                if (rh.collider.CompareTag("Clickable"))
                {
                    //Find out if it's a camera or a drone
                    //use the method that object belongs to
                }
            }
        }
    }

    public void ClickCamera(int cam)
    {
        //Switches Camera view to the camera that was clicked on
        //When the camera switches, the UI should swap to the appropriate camera 
    }

    public void ClickDrone(int drone)
    {
        //Switches the UI to show Drone stuff when activated
    }
}
