/***
 * GameManager.cs
 * Version 1.0
 * By Nathan Boles
 * 
 * This script will run general maintenance for the game and will be a common go between for what
 * would be common information. It will also keep track of how much time has passed, along with 
 * be most important for the GameOver.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("This is the camera that the player is currently seeing. Do not manually change this via editor as it's serialized for debugging purposes only.")]
    [SerializeField] Camera activeCamera;
    [Tooltip("This is the device whose is most recently clicked on. Used to change to the corresponding UI and keep track of the drone. Do not manually change this via editor as it's serialized for debugging purposes only.")]
    [SerializeField] GameObject currentDevice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Active Camera's Getter
    /// </summary>
    /// <returns>Returns the GameObject that is currently the active camera</returns>
    public Camera GetActiveCamera()
    {
        return activeCamera;
    }

    /// <summary>
    /// Active Camera setter
    /// </summary>
    /// <param name="nActiveCamera">What the active camera is</param>
    public void SetActiveCamera(Camera nActiveCamera)
    {
        activeCamera = nActiveCamera;
    }

    /// <summary>
    /// Current Device Getter
    /// </summary>
    /// <returns>The game object that is most recently swapped to by hotkeys or clicked on via minimap</returns>
    public GameObject GetCurrentDevice()
    {
        return currentDevice;
    }

    public void SetCurrentDevice(GameObject nDevice)
    {
        currentDevice = nDevice;
    }

    /// <summary>
    /// The main timer for the game. This method is used to help keep track of how much time has passed for the overall
    /// level.
    /// </summary>
    void Timer()
    {

    }

    /// <summary>
    /// The main Game Over setting for the game. This is called upon when the game ends.
    /// </summary>
    public void GameOver()
    {
        Debug.Log("Game Over");
    }

}
