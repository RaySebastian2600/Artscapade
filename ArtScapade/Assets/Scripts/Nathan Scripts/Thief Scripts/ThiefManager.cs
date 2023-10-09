/***
 * ThiefManager.cs
 * Version 1.0
 * By Nathan Boles
 * 
 * This script is in charge of keeping track of the thieves who are currently not inside the play area. 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefManager : MonoBehaviour
{
    [Tooltip("How long the player has at the start before any thiefs will activate")]
    [SerializeField] float setup = 20f;
    [Tooltip("How long before another thief is able to enter the play area.")]
    [SerializeField] float nextThiefTimer = 10f;
    [Tooltip("How much to add to Setup and nextThiefTimer for the maxInclusive of Random.Range")]
    [SerializeField] float range = 5;
    bool nextThiefReady; //A boolean meant to check to see if the next thief can start or not.

    //GameManager gameManager;

    [Tooltip("The list of thieves available to activate")]
    [SerializeField] List<GameObject> thiefRoster;
    
    //[SerializeField] int activeThieves = 0;
    //[SerializeField] GameObject[] targetRoster;
    //[SerializeField] int missingArtwork = 0;


    /*private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }*/

    // Start is called before the first frame update
    void Start()
    {
        nextThiefReady = false; //Ensure that this check is false before starting
        StartCoroutine(Setup()); //Give the player time to setup
    }

    // Update is called once per frame
    void Update()
    {
        if (nextThiefReady) //If you are able, start testing to see if a thief can be added to the play area.
            AddThief();
    }

    /// <summary>
    /// A coroutine that is called at the start of the game to give the player some time to acquaint themselves with the game area.
    /// </summary>
    /// <returns></returns>
    IEnumerator Setup()
    {
        float timer = Random.Range(setup, setup + range);
        yield return new WaitForSeconds(timer);
        nextThiefReady = true;
    }

    /// <summary>
    /// A coroutine that is used to give a buffer between when each thief will enter the building. 
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForNextThief()
    {
        nextThiefReady = false;
        float timer = Random.Range(nextThiefTimer, nextThiefTimer + range);
        yield return new WaitForSeconds(timer);
        nextThiefReady = true;
    }

    /// <summary>
    /// This method checks to see if there are any thieves left in the roster, and if they are, it'll pick one at random and see if they are available. 
    /// </summary>
    void AddThief()
    {
        Debug.Log("Trying to add a thief");
        if (thiefRoster.Count != 0)
        {
            int nextThief = Random.Range(0, thiefRoster.Count);
            Debug.Log(nextThief);
            if (thiefRoster[nextThief].GetComponent<BaseThiefAI>().Activate())
            {
                thiefRoster.RemoveAt(nextThief);
                StartCoroutine(WaitForNextThief());
            }
            else
                AddThief();
        }
        else
            StartCoroutine(WaitForNextThief());
    }

    /// <summary>
    /// This method removes the thief from the thiefRoster List. Mostly meant for marking when there are no targets available for this thief to take.
    /// Honestly this method might not be needed and will likely be removed on a later iteration of this script. 
    /// </summary>
    /// <param name="thief">The thief gameobject this is to be removed from the list.</param>
    public void RemoveThief(GameObject thief)
    {
        thiefRoster.Remove(thief);
    }

    /// <summary>
    /// This method returns the thief to the roster list.
    /// </summary>
    /// <param name="thief">The thief gameobject this is to be returned to the list.</param>
    public void ReturnThief(GameObject thief)
    {
        Debug.Log("Adding thief back in");
        thiefRoster.Add(thief);
    }
}
