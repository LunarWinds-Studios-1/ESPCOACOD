using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown
{
    //code modified from tutorial by Chonk
    private float cooldownTime;
    private float nextFireTime;

    public bool isCoolingDown => Time.time < nextFireTime;
    public void StartCooldown() => nextFireTime = Time.time + cooldownTime;

    public void StopCooldown() => nextFireTime = Time.time;

    public float completionRatio => (nextFireTime - Time.time) / cooldownTime;

    public Cooldown(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
    }
   
}
