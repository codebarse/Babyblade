using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility
{
    public DashState dashState;
    public float dashTimer;
    public float maxDash = 20f;
    public float dashMultiplier = 200f;  //Increase this value to increase the speed of dash 
    public float dashCooldownRate = 20f; //Increase this value to decrease the amount of cooldown
    public float dashDurationRate = 20f; //Increase this value to decrease the duration of dash.

    public Vector2 savedVelocity;

    public void checkDash(Rigidbody rigidbody, Keyset ks)
    {
        Debug.Log(dashState.ToString());
        switch (dashState)
        {        
            case DashState.Ready:
                var isDashKeyDown = Input.GetKeyDown(ks.ATTACK) && (Input.GetKeyDown(ks.LEFT) || Input.GetKeyDown(ks.RIGHT)
                    || Input.GetKeyDown(ks.UP) || Input.GetKeyDown(ks.DOWN));
                if (isDashKeyDown)
                {
                    //savedVelocity = rigidbody.velocity;
                    //rigidbody.velocity = new Vector3(rigidbody.velocity.x * dashMultiplier, rigidbody.velocity.y, 
                    //rigidbody.velocity.z * dashMultiplier);
                    Vector3 forceVector = new Vector3(rigidbody.velocity.x * dashMultiplier * Time.deltaTime, 0, 
                        dashMultiplier * rigidbody.velocity.z * Time.deltaTime);
                    rigidbody.AddForce(forceVector, ForceMode.Impulse);
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * dashDurationRate;
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    //rigidbody.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime * dashCooldownRate;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }
}

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}