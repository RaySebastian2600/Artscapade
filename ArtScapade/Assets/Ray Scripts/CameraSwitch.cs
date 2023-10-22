using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject Screen1;
    public GameObject Screen2;                  //Setting places in the inspector for the Screen objects to attach to.
    public GameObject Screen3;
    public GameObject Screen4;
    public GameObject Screen5;
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
        if (Input.GetKeyDown("4"))
        {
            ScreenFour();
        }
        if (Input.GetKeyDown("5"))
        {
            ScreenFive();
        }
    }

    void ScreenOne()
    {
        Screen1.SetActive(true);                        //The camera method that displays each screen individually
        Screen2.SetActive(false);
        Screen3.SetActive(false);
        Screen4.SetActive(false);
        Screen5.SetActive(false);
    }
    void ScreenTwo()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(true);
        Screen3.SetActive(false);
        Screen4.SetActive(false);
        Screen5.SetActive(false);
    }
    void ScreenThree()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(false);
        Screen3.SetActive(true);
        Screen4.SetActive(false);
        Screen5.SetActive(false);
    }
    void ScreenFour()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(false);
        Screen3.SetActive(false);
        Screen4.SetActive(true);
        Screen5.SetActive(false);
    }
    void ScreenFive()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(false);
        Screen3.SetActive(false);
        Screen4.SetActive(false);
        Screen5.SetActive(true);
    }
}
