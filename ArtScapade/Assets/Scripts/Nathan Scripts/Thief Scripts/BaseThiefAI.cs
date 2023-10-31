/***
 * BaseThiefAI.cs
 * Version 1.0
 * By Nathan Boles
 * 
 * This is the base thief AI. This is the main powerhouse in charge of watching this thief's behavior, with
 * the intent that other thief AI classes will be made inheriting from this.
 * 
 * This AI main job is to do nothing while it's inactive, and once it's activated, it'll select an available route at
 * random (if able) and then start trying to make attempts to push towards the next step whenever it is allowed.
 * If successful, it'll move its transform to the next step, and if not, it'll remain where it is. If it manages to make 
 * it's final check while at the final waypoint, it will escape with the artwork in hand. Resetting the AI to wait to be
 * activated again. The same is true if it's captured instead, with any artwork it grabbed being returned before being reset.
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseThiefAI : MonoBehaviour
{
    //Note: Some variables below are marked SerializeField for debugging purposes only. They will have [SerializeField] removed on
    //a later version of this script. 

    [Tooltip("The minimal waiting time (in seconds) before the thief can try to move.")]
    [SerializeField] protected float thiefCooldownMin = 5f;
    [Tooltip("The maximum waiting time (in seconds) before the thief can try to move.")]
    [SerializeField] protected float thiefCooldownMax = 10f;
    [Tooltip("A reduction to the cooldown when it fails to make a step attempt. Serialized only for debugging purposes. Do not set in editor.")]
    [SerializeField] protected float cooldownReduction = 0;

    [Tooltip("The chance this thief has to make a move when it is able to. The check is between 0 to 100, with 100 being guarenteed to pass.")]
    [SerializeField] protected int stepChance = 50;

    [Tooltip("Whether this is on cooldown or not. True if it is on Cooldown, false if not. Do not set in editor")]
    [SerializeField] protected bool onCooldown = false;
    [Tooltip("Whether this is active or not. True if it is active, false it isn't. Do not set in editor")]
    [SerializeField] protected bool isActive = false;

    [Tooltip("The list of routes available to this thief")]
    [SerializeField] protected List<Route> routes;
    [Tooltip("The route this thief is currently on. Do not set in editor")]
    [SerializeField] protected int activeRoute = 0;
    [Tooltip("What step on it's route is this thief at. Do not set in editor")]
    [SerializeField] protected int step = 0;
    [Tooltip("What the current target of the active route. Do not set in editor")]
    [SerializeField] protected GameObject currentTarget;
    [Tooltip("A list of routes that are currently unavailable due to the target already being targeted by someone else. Do not set in editor.")]
    [SerializeField] protected List<Route> unavailableRoutes;
    [Tooltip("A boolean intended to signify when this thief can put all the objects he has in unavailableRoutes list back into his routes list. Do not set in editor.")]
    [SerializeField] protected bool refreshAvailable = false;

    [Tooltip("The return point for this thief when it's captured or escapes.")]
    [SerializeField] protected GameObject home;

    [Tooltip("The ThiefManager script in the scene.")]
    [SerializeField] protected ThiefManager thiefManager;
    [Tooltip("The GameManager script in the scene")]
    [SerializeField] protected GameManager gameManager;
    [Tooltip("The TargetInformation script in the scene")]
    [SerializeField] protected TargetInformation targetInfo;

    // Start is called before the first frame update
    protected void Start()
    {
        thiefManager = FindObjectOfType<ThiefManager>();
        targetInfo = FindObjectOfType<TargetInformation>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(!onCooldown && isActive) //Checks to see if this thief can make an action in the first place
        {
            if (Random.Range(0,100) < stepChance) //Check to see if it is able to get a random number below stepChance
            {
                Debug.Log("Step Chance success check");
                if (routes[activeRoute].getFocusCamera(step) == gameManager.GetActiveCamera()) //Check to see if the thief is currently on camera
                {
                    ReduceCooldown();
                }
                else
                {
                    TakeStep();
                    cooldownReduction = 0;
                }
            }
            else
            {
                ReduceCooldown();
            }
            StartCoroutine(Cooldown());
        }
    }

    /// <summary>
    /// This method is used to reduce help increase the cooldownReduction variable, with it making sure the minimum remains to at least 1 second
    /// and the range between the two doesn't change.
    /// </summary>
    private void ReduceCooldown()
    {
        if (cooldownReduction < thiefCooldownMin - 1)
        {
            cooldownReduction += .5f;
            if (cooldownReduction > thiefCooldownMin - 1)
            {
                cooldownReduction = thiefCooldownMin - 1;
            }
        }
    }

    /// <summary>
    /// This is the method that places the thief from their home base into the museum.
    /// It does this by first checking to see if any unavailable routes that need to be added back into the main list
    /// Then it chooses a route at random, checking to see if the target at that route is taken. If it is, put the route
    /// into the unavailable list and check again. If not, then get the thief into position then start cooldown.
    /// </summary>
    /// <returns></returns>
    public virtual bool Activate()
    {
        if (unavailableRoutes.Count != 0 && refreshAvailable)
        {
            foreach (Route aRoute in unavailableRoutes)
            {
                routes.Add(aRoute);
            }
            unavailableRoutes.Clear();
            refreshAvailable = false;
        }

        if (routes.Count != 0)
        {
            activeRoute = Random.Range(0, routes.Count);
            currentTarget = routes[activeRoute].GetTarget();
            if (targetInfo.GetTarget().Contains(currentTarget))
            {
                step = 0;
                transform.position = routes[activeRoute].GetWaypoint(step).transform.position;
                isActive = true;
                targetInfo.TargetChosen(currentTarget);
                refreshAvailable = true;
            }
            else
            {
                if (targetInfo.GetArt().Contains(currentTarget))
                {
                    unavailableRoutes.Add(routes[activeRoute]);
                }
                routes.Remove(routes[activeRoute]);
                Activate();
            }
        }
        else
        {
            if (unavailableRoutes.Count == 0) //If there are no unavailable routes waiting to be recycled.
                thiefManager.RemoveThief(gameObject);
            else //If there are unavailable routes waiting to be recycled, simple put this thief back into the roster and reset unavailable routes next time.
            { 
                thiefManager.ReturnThief(gameObject);
                refreshAvailable = true;
            }
            return false;
        }
        StartCoroutine(Cooldown());
        return true;
    }

    /// <summary>
    /// A coroutine intended to act as a cooldown for this thief, ensuring it only moves exactly when desired (with some randomness involved.)
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Cooldown()
    {
        float thiefCooldown = Random.Range(thiefCooldownMin - cooldownReduction, thiefCooldownMax - cooldownReduction);
        onCooldown = true;
        yield return new WaitForSeconds(thiefCooldown);
        onCooldown = false;
    }

    /// <summary>
    /// If there are steps this thief can take, they'll take the next one then check if they are in 'stealing' position. 
    /// If the thief is on their last step, they will escape instead. 
    /// </summary>
    protected virtual void TakeStep()
    {
        Debug.Log("TakeStep Check");
        step++;
        if (step < routes[activeRoute].GetWaypointSize())
        {
            transform.position = routes[activeRoute].GetWaypoint(step).transform.position;
            if (step == routes[activeRoute].GetWaypointTargetIndex())
            {
                routes[activeRoute].GetTarget().SetActive(false);
            }
        }
        else
            Escape();
    }

    /// <summary>
    /// This sets whether the thief is active or not. (Might be defunct)
    /// </summary>
    /// <param name="nActive">Sets isActive</param>
    public void SetIsActive(bool nActive)
    {
        isActive = nActive;
    }

    /// <summary>
    /// Gets isActive of this thief
    /// </summary>
    /// <returns>The isActive of this thief</returns>
    public bool GetIsActive()
    {
        return isActive;
    }

    /// <summary>
    /// Remove this character from the play area and remove the art it is suppose to steal from the game. 
    /// </summary>
    private void Escape()
    {
        ReturnHome();
        targetInfo.TargetStolen(currentTarget);
    }

    /// <summary>
    /// Remove this character from the play area and reset the artwork.
    /// </summary>
    public virtual void Captured()
    {
        //Play a captured animation then...
        ReturnHome();
        targetInfo.TargetSaved(currentTarget);
        routes[activeRoute].GetTarget().SetActive(true);
    }

    /// <summary>
    /// Returns the NPC back to it's homepoint and resets it's status in ThiefManager
    /// </summary>
    void ReturnHome()
    {
        isActive = false;
        transform.position = home.transform.position;
        thiefManager.ReturnThief(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Capture"))
        {
            Captured();
        }
    }
}
