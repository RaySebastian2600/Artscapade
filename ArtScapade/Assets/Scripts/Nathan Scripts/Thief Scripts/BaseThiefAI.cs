using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseThiefAI : MonoBehaviour
{
    [SerializeField] protected GameObject[] steps;
    [SerializeField] protected float thiefCooldownMin = 5f;
    [SerializeField] protected float thiefCooldownMax = 10f;
    [SerializeField] protected bool onCooldown = false;
    [SerializeField] protected bool isActive = false;
    [SerializeField] protected ThiefManager thiefManager;
    [SerializeField] protected int activeRoute;
    [SerializeField] protected GameObject[] routes;

    // Start is called before the first frame update
    void Start()
    {
        thiefManager = FindObjectOfType<ThiefManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual IEnumerator Cooldown(float thiefCooldown)
    {
        yield return new WaitForSeconds(thiefCooldown);
    }

    protected virtual void TakeStep()
    {

    }

    public void SetIsActive(bool nActive)
    {
        isActive = nActive;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    protected virtual void Captured()
    {

    }
}
