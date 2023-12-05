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
using TMPro;

public class GameManager : MonoBehaviour
{
    [Tooltip("This is the camera that the player is currently seeing. Do not manually change this via editor as it's serialized for debugging purposes only.")]
    [SerializeField] Camera activeCamera;
    [Tooltip("This is the device whose is most recently clicked on. Used to change to the corresponding UI and keep track of the drone. Do not manually change this via editor as it's serialized for debugging purposes only.")]
    [SerializeField] GameObject currentDevice;
    [Tooltip("This is the canvas that holds the Game Over Screen")]
    [SerializeField] GameObject gameOverScreen;
    [Tooltip("The TMP that holds the score for the Game Over screen")]
    [SerializeField] TextMeshProUGUI scoreText;
    [Tooltip("The script meant to keep track of the art in the scene")]
    [SerializeField] TargetInformation targetInformation;

    // Start is called before the first frame update
    void Start()
    {
        targetInformation = FindObjectOfType<TargetInformation>();
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

    /// <summary>
    /// Sets what is the active device
    /// </summary>
    /// <param name="nDevice">The new active device</param>
    public void SetCurrentDevice(GameObject nDevice)
    {
        currentDevice = nDevice;
    }

    /// <summary>
    /// The main Game Over setting for the game. This is called upon when the game ends.
    /// </summary>
    public void GameOver()
    {
        //Debug.Log("Game Over");
        gameOverScreen.SetActive(true);
        scoreText.text = targetInformation.GetCurrentArt().ToString() + " / " + targetInformation.GetTotalArt().ToString();
        Time.timeScale = 0;
    }

}
