using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefManager : MonoBehaviour
{
    float nextThiefTimer;
    GameManager gameManager;
    [SerializeField] float setup;
    [SerializeField] GameObject[] thiefRoster;
    [SerializeField] int activeThieves = 0;
    [SerializeField] GameObject[] targetRoster;
    [SerializeField] int missingArtwork = 0;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(setup);
    }

    void AddThief()
    {

    }

    public void RemoveThief()
    {

    }

    public void RemoveArt()
    {

    }
}
