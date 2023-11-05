using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float remainingTime;
    
    // Update is called once per frame
    void Update()
    {
       if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
       else if (remainingTime < 0)
        {
            remainingTime = 0;
            GameOver();
        }
        
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    public void GameOver()
    {
        Debug.Log("GameOver");
    }

}