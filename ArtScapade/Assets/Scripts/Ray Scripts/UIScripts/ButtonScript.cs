using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] Camera cam;
 
    

    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))            //Uses a space key to bring minimap up
        {
            MapActive();
        }
    }

    public void MapActive()         //Used to bring minimap on and off screen
    {      
        if (cam.depth >= 1)
        {
            cam.depth = -1;         //Checks minimap camera depth and changes when clicked
        }
        else if (cam.depth >= -1)
        {
            cam.depth = 1;
        }
    }
    
}
