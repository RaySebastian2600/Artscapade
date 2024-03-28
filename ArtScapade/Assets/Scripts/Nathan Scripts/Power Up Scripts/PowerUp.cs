using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] GameObject UIIcon;

    /// <summary>
    /// This is the part of the power up that gets called when it is first collected.
    /// </summary>
    public abstract void SetUp();

    /// <summary>
    /// This is called every frame a powerup is collected. 
    /// </summary>
    public abstract void Run();

    /// <summary>
    /// This is called when the player uses the corresponding button.
    /// </summary>
    public abstract void Activate();

    /// <summary>
    /// This is called when the powerup is being removed.
    /// </summary>
    public abstract void CleanUp();

}
