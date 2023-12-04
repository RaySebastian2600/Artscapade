/**
 * DroneController.cs
 * By Nathan Boles
 * Version 0.9
 * 
 * This code is the main controller of the drone system within the game.
 * It uses mouse input to create a line upon the world upon which the player draws.
 * This line is then used as a path for the drone to follow.
 * Everytime the mouse is used, the path disappears and the drone stops awaiting new orders.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DroneController : MonoBehaviour
{
    bool isSelected; //Keeps track if this object is selected or not. (Probably decrepate and have GameManager keep track of this instead.)
    [Tooltip("The line renderer that came along with this prefab")]
    [SerializeField] LineRenderer lineRenderer;
    [Tooltip("The color of the line when it's setting up.")]
    [SerializeField] Gradient setupGradient;
    [Tooltip("The color of the line when the drone is moving.")]
    [SerializeField] Gradient pathGradient;
    List<Vector3> path = new List<Vector3>(); //The path that this object will follow
    List<Vector3> setup = new List<Vector3>(); //A list of points that is used for setuping the path
    [Tooltip("How fast does this thing move?")]
    [SerializeField] float moveSpeed = 5;
    [Tooltip("The raidus that the mouse has to be within to see if mouse is clicking near it")]
    [SerializeField] float startToMoveRadius = 10f;
    bool clickTest = false;
    [Tooltip("How far away for a thief to be for it to be hit by this drones Capture")]
    [SerializeField] float captureRadius;
    bool captureIsOnCooldown = false;
    [Tooltip("How long should capture command be on cooldown for?")]
    [SerializeField] float captureCooldownLength = 3f;
    [Tooltip("The thieves layermask")]
    [SerializeField] LayerMask thieves;
    [Tooltip("The gameobject meant to represent the capture area of this object.")]
    [SerializeField] GameObject captureVisual;


    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        clickTest = false;
    }

    // Update is called once per frame
    void Update()
    {
        DroneMove();
        if (isSelected) //This will be important when multiple items being watched becomes important
            GetInput();
    }

    /// <summary>
    /// If there are any points on the ground, the drone will follow them in order til it reaches the last one.
    /// </summary>
    void DroneMove()
    {
        if (path.Any())
        {
            //When you have a path, move towards the path
            if (transform.position != path[0])
            {
                transform.position = Vector3.MoveTowards(transform.position, path[0], moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, path[0] - transform.position, moveSpeed * Time.deltaTime, 0.0f));
            }
            else
            {
                //Debug.Log(path[0]);
                path.RemoveAt(0);
                DrawLine();
            }
        }
    }
    
    /// <summary>
    /// Use a trigger collider to check to see if there are any 'enemies' around. If there is, use their capture method.
    /// </summary>
    public void Capture()
    {
        //When activated, do the capture action
        //Check to see if Capture is on cooldown
        //If not, enable a trigger
        //and put Capture() on a cooldown
        Debug.Log("Attempting Capture");
        if (!captureIsOnCooldown)
        {
            Debug.Log("Capturing");
            captureVisual.transform.localScale = new Vector3(captureRadius, captureRadius, captureRadius);
            captureVisual.SetActive(true);
            Collider[] col = Physics.OverlapSphere(transform.position, captureRadius, thieves);
            //Instantiate(spherePrefab, this.transform.position, Quaternion.identity);
            foreach (Collider c in col)
            {
                if (c.GetComponent<BaseThiefAI>())
                {
                    c.GetComponent<BaseThiefAI>().Captured();
                }
            }
            StartCoroutine(CaptureCooldown());
        }
    }

    /// <summary>
    /// This is a coroutine to keep track of the Capture Cooldown. When it ends, you can use the capture method again.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CaptureCooldown()
    {
        yield return new WaitForSeconds(captureCooldownLength);
        captureIsOnCooldown = false;
        captureVisual.SetActive(false);
    }



    /// <summary>
    /// Watches for input from the mouse.
    /// When there is input, clear the path and then follow the mouse to Setup a new path.
    /// When the button is raised, que the setup points into Path and redraw the line.
    /// </summary>
    void GetInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ClickCheck();
            if (clickTest)
                ClearPath();
            //add a check to make sure that the initial click is within a reasonable distance of this drone
        }
            

        if (Input.GetButton("Fire1"))
        {
            if (clickTest)
                SetupPath();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            TransferPath();
            clickTest = false;
            setup.Clear();
            lineRenderer.colorGradient = pathGradient;
            DrawLine();
        }
    }

    /// <summary>
    /// Draws the line according to the points on Path
    /// </summary>
    void DrawLine()
    {
        lineRenderer.positionCount = path.Count();
        lineRenderer.SetPositions(path.ToArray());
    }

    /// <summary>
    /// Clears both Path and Setup lists before making sure the line disappears as well.
    /// Resets the color gradiant to be that of the setup Color
    /// </summary>
    void ClearPath()
    {
        path.Clear();
        setup.Clear();
        lineRenderer.positionCount = path.Count();
        lineRenderer.colorGradient = setupGradient;
        //lineRenderer.SetPositions(null);
    }

    /// <summary>
    /// This watches the mouse on the minimap (main camera) and applies points to where it goes
    /// It will then check those points to ensure that it's on the right type of surface and
    /// that the points are far away enough. If they are, add the point to the setup list and draw
    /// the line as needed. If it's on the wrong surface, however, clear the list entirely and reset.
    /// </summary>
    void SetupPath()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        

        if (Physics.Raycast(ray, out rayHit))
        {
            //Debug.Log("Ray point is " + rayHit.point);
            float point = PointCheck(rayHit);
            if (point > 1f)
            {
                setup.Add(rayHit.point);

                lineRenderer.positionCount = setup.Count;
                lineRenderer.SetPositions(setup.ToArray());
            }
            else if (point == -1)
            {
                Debug.Log("Fail to find drawable");
                clickTest = false;
                setup.Clear();
            }
        }
    }

    /// <summary>
    /// This is a simple method that takes each point in setup and puts it into path
    /// </summary>
    void TransferPath()
    {
        foreach (Vector3 point in setup)
        {
            path.Add(point);
        }
    }


    /// <summary>
    /// This method checks to see if the inital click/point is within a reasonable 
    /// </summary>
    private void ClickCheck()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit))
        {
            //Debug.Log(Vector3.Distance(rayHit.point, transform.position));
            if (Vector3.Distance(rayHit.point, transform.position) <= startToMoveRadius)
            {
                clickTest = true;
            }
        }
    }

    /// <summary>
    /// This checks on both where the latest point is and how far each point is.
    /// If this is on object that isn't 'drawable', then return -1. 
    /// Otherwise, if there is no point in setup at all, then throw an infinite number back.
    /// Then if there is another point, check the distance between those points, and return it.
    /// </summary>
    /// <param name="rh">The Raycast Hit data that is to be checked</param>
    /// <returns>Returns -1 if on an illegal object, infinite if no other point is in the list, and the distance
    /// between the points if there is.</returns>
    private float PointCheck(RaycastHit rh)
    {
        if (!rh.collider.CompareTag("Drawable"))
        {
            return -1;
        }
        else if (!setup.Any())
            return Mathf.Infinity;

        return Vector3.Distance(setup.Last(), rh.point);
    }

    /// <summary>
    /// A public method meant to be used by other scripts. This marks if this item is selected or not on the minimap.
    /// </summary>
    /// <param name="nIsSelected">True if it is selected on minimap, false if not</param>
    public void setIsSelected(bool nIsSelected)
    {
        isSelected = nIsSelected;
    }


}
