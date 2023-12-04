/***
 * TargetInformation.cs
 * Version 1.0
 * By Nathan Boles
 * 
 * This scripts job is to hold the information about all the potential GameObjects that
 * the thieves are able to target, as well as keeping track which of those objects are 
 * art pieces and what are utility objects, like cameras. This way it's easier to see what 
 * is only unavailable for what it's already being pursued, and what is unavailable because
 * the art is already stolen. 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInformation : MonoBehaviour
{
    [Tooltip("The list of GameObjects that the thieves can currently target")]
    [SerializeField] List<GameObject> targets;
    [Tooltip("The list of GameObjects that is currently still in the game area. This is filled at the start, so please do not mess with.")]
    [SerializeField] List<GameObject> art;
    [Tooltip("The list of GameObjects that are not art pieces, but still targetable. This is filled at the start, so please do not mess with.")]
    [SerializeField] List<GameObject> utility;
    [Tooltip("The GameManager script meant to be in charge of managing the game state as a whole.")]
    [SerializeField] GameManager gameManager;
    int totalArt = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        //Start by creating a list of GameObjects which are tagged Art and which are not.
        foreach (GameObject aTarget in targets)
        {
            if (aTarget.CompareTag("Art"))
            {
                art.Add(aTarget);
                totalArt++;
            }
            else
                utility.Add(aTarget);
        }
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns the list of Targets
    /// </summary>
    /// <returns>The list of Targets</returns>
    public List<GameObject> GetTarget()
    {
        return targets;
    }

    /// <summary>
    /// Returns the list of art
    /// </summary>
    /// <returns>List of Art</returns>
    public List<GameObject> GetArt()
    {
        return art;
    }

    /// <summary>
    /// Removes the target from the list of available targets.
    /// </summary>
    /// <param name="aTarget">The target to be removed</param>
    public void TargetChosen(GameObject aTarget)
    {
        targets.Remove(aTarget);
    }

    /// <summary>
    /// Puts the target back onto the list of available targets. 
    /// </summary>
    /// <param name="aTarget">The target to be added</param>
    public void TargetSaved(GameObject aTarget)
    {
        targets.Add(aTarget);
    }

    /// <summary>
    /// Removes the art from the list of art. If there is no art left on the list, the game ends.
    /// </summary>
    /// <param name="aTarget">The art to be removed</param>
    public void TargetStolen(GameObject aTarget)
    {
        art.Remove(aTarget);
        Debug.Log("Art left: " + art.Count);
        if (art.Count == 0)
        {
            Debug.Log("Game Over");
            gameManager.GameOver();
        }
    }

    public int GetTotalArt()
    {
        return totalArt;
    }

    public int GetCurrentArt()
    {
        return art.Count;
    }
}
