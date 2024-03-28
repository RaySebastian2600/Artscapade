using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScreen : MonoBehaviour
{
    CameraManager cameraManagerLink;
    bool minimapActive = false;
    [SerializeField] Vector2 cameraSize = new Vector2(.4f, .4f);

    // Start is called before the first frame update
    void Start()
    {
        cameraManagerLink = FindObjectOfType<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetButtonDown("Jump"))
            ScreenSwap();
    }

    public void ScreenSwap()
    {
       if (!minimapActive)
        {
            foreach (Camera i in cameraManagerLink.allCameras)
            {
                i.rect = new Rect(new Vector2(0, 0), cameraSize);
            }
            minimapActive = true;
        }
       else
        {
            foreach (Camera i in cameraManagerLink.allCameras)
            {
                i.rect = new Rect(new Vector2(0, 0), new Vector2(1f, 1f));
            }
            minimapActive = false;
        }
    }
}
