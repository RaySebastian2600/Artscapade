using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRunner : MonoBehaviour
{
    GameObject currentPU = null;

    // Update is called once per frame
    void Update()
    {
        if (currentPU != null)
        {
            currentPU.GetComponent<PowerUp>().Run();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collGameObject = collision.gameObject;
        if (collGameObject.CompareTag("Power Up"))
        {
            if (currentPU != null)
            {
                currentPU.GetComponent<PowerUp>().CleanUp();
            }
            currentPU = collGameObject;
            currentPU.GetComponent<PowerUp>().SetUp();
        }
    }

    public void ActivatePowerUp()
    {
        if (currentPU != null)
            currentPU.GetComponent<PowerUp>().Activate();
    }

    public void RemovePowerUp()
    {
        currentPU = null;
    }
}
