using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject activeCamera;

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
    public GameObject GetActiveCamera()
    {
        return activeCamera;
    }

    /// <summary>
    /// Active Camera's Setter. Call to change what the Active Camera is
    /// </summary>
    /// <returns></returns>
    public void SetActiveCamera(GameObject nActiveCamera)
    {
        activeCamera = nActiveCamera;
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
        
    }

}
