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

    public GameObject GetActiveCamera()
    {
        return activeCamera;
    }
    
    public void SetActiveCamera(GameObject nActiveCamera)
    {
        activeCamera = nActiveCamera;
    }

    void Timer()
    {

    }

    public void GameOver()
    {
        
    }

}
