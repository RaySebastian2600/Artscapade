/**
 * DrawPath.cs
 * Version 0.8
 * By Nathan Boles
 * Based on code from Jason Weimann
 * 
 * This script is currently decrepated and will be removed on a later date.
 * Please remove this script if this is still here after Milestone 3
 * 
 * This script is in charge of drawing a path for other scripts. 
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] List<Vector3> path = new List<Vector3>();
    [SerializeField] DroneController droneController;
    //Action<IEnumerable<Vector3>> onNewPathCreated = delegate { };

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    /*void Update()
    {
        CreatePath();
    }*/

    public void CreatePath()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit))
        {
            float point = PointCheck(rayHit);
            if (point > 1f)
            {
                path.Add(rayHit.point);

                lineRenderer.positionCount = path.Count;
                lineRenderer.SetPositions(path.ToArray());
            }
            else if (point == -1)
            {
                Debug.Log("Fail to find drawable");
                path.Clear();
            }
        }
    }

    public List<Vector3> SendPath()
    {
        Debug.Log("Sending Path");
        List<Vector3> holder = path;
        //Debug.Log(holder[0]);
        path.Clear();
        return holder;
    }

    private float PointCheck(RaycastHit rh)
    {
        if (!rh.collider.CompareTag("Drawable"))
        {
            return -1;
        }   
        else if (!path.Any())
            return Mathf.Infinity;

        return Vector3.Distance(path.Last(), rh.point);
    }


}
