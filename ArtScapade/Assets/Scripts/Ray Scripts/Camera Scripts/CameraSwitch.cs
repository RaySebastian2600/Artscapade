using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject Screen1;
    public GameObject Screen2;                  //Setting places in the inspector for the Screen objects to attach to.
    public GameObject Screen3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            ScreenOne();
        }
        if (Input.GetKeyDown("2"))              //The key binds to each camera method can add later on
        {
            ScreenTwo();
        }
        if (Input.GetKeyDown("3"))
        {
            ScreenThree();
        }
       
    }

    public void ScreenOne()
    {
        Screen1.SetActive(true);                        //The camera method that displays each screen individually
        Screen2.SetActive(false);
        Screen3.SetActive(false);
        
    }
    public void ScreenTwo()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(true);
        Screen3.SetActive(false);
        
    }
    public void ScreenThree()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(false);
        Screen3.SetActive(true);
       
    }
   
}
