/***
 * Route.cs
 * Version 1.0
 * By Nathan Boles
 * 
 * This scripts job is to hold the different waypoints that this route will lead through along with
 * the target this route is leading towards. 
 * 
 * It also keeps track of the camera that is the main focus of this route, along with what waypoint
 * indexs that the camera may change at. (This last part isn't fully implemented yet.)
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [Tooltip("An array of waypoints that the thieves will follow, starting from 0")]
    [SerializeField] GameObject[] waypoints;
    [Tooltip("The current target of this route. The object that the thieves are actively trying to steal.")]
    [SerializeField] GameObject target;
    [Tooltip("Which waypoint will represent the thief having taken the art into thier hands")]
    [SerializeField] int waypointTargetIndex;
    [Tooltip("What camera is the one that will see this thief.")]
    [SerializeField] Camera[] focusedCamera;
    //[Tooltip("Which points should focused Camera change on? [Not implemented yet]")]
    //[SerializeField] int[] cameraChangePoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This returns a specified waypoint gameobject
    /// </summary>
    /// <param name="point">The index number for the array to return</param>
    /// <returns>The returned waypoint</returns>
    public GameObject GetWaypoint(int point)
    {
        return waypoints[point];
    }

    /// <summary>
    /// Returns the size of the routes array
    /// </summary>
    /// <returns>The length of Waypoints array</returns>
    public int GetWaypointSize()
    {
        return waypoints.Length;
    }

    /// <summary>
    /// Returns the WaypointTargetIndex
    /// </summary>
    /// <returns>The WaypointTargetIndex</returns>
    public int GetWaypointTargetIndex()
    {
        return waypointTargetIndex;
    }

    /// <summary>
    /// Returns the target of this route
    /// </summary>
    /// <returns>The target of this route</returns>
    public GameObject GetTarget()
    {
        return target;
    }
    
    /// <summary>
    /// Turns this route GameObject of if completed.
    /// </summary>
    public void RouteCompleted()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns the camera currently focusing on this route
    /// </summary>
    /// <returns>The FocusCamera of this route</returns>
    public Camera getFocusCamera(int point)
    {
        return focusedCamera[point];
    }
}
