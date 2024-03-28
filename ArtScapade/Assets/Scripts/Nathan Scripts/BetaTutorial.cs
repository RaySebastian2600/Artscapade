using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialCanvas;
    private void Start()
    {
        Time.timeScale = 0;
    }
    
    public void TutorialDone()
    {
        Time.timeScale = 1;
        tutorialCanvas.SetActive(false);
    }
}
