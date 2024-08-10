using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool canDash { get; private set; }
    private bool isHolding;
    private bool dashInputStop;

    private float lastDashTime;
    private float fixedDeltaTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAIPos;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public bool CheckIfCanDash()
    {
        return canDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }

    public void ResetCanDash() => canDash = true;


    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.position;
    }
    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(player.transform.position, lastAIPos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    public override void Enter()
    {
        base.Enter();
        /*
        public static float timeScale;
        Description
        The scale at which time passes.

        This can be used for slow motion effects or to speed up your application. When timeScale is 1.0, time passes as fast as real time. When timeScale is 0.5 time passes 2x slower than realtime.

        When timeScale is set to zero your application acts as if paused if all your functions are frame rate independent. Negative values are ignored.

        Note that changing the timeScale only takes effect on the following frames. How often MonoBehaviour.FixedUpdate is executed per frame depends on the timeScale. Therefore, to keep the number of FixedUpdate callbacks per frame constant, you must also multiply Time.fixedDeltaTime by timeScale. Whether this adjustment is desirable is game-specific.

        FixedUpdate functions and suspended Coroutines with WaitForSeconds are not called when timeScale is set to zero.
         
         */

        this.fixedDeltaTime = Time.fixedDeltaTime;

        canDash = false;
        player.InputHandler.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * player.FacingDirection;

        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;

        player.DashDirectionIndicator.gameObject.SetActive(true);

    }

    public override void Exit()
    {
        base.Exit();
        if(player.CurrentVelocity.y > 0)
        {
            player.SetVelocityY(player.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!isExistingState)
        {
            if(isHolding)
            {
                dashDirectionInput = player.InputHandler.DashDirectionInput;
                dashInputStop = player.InputHandler.DashInputStop; 

                if(dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }
                 
                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);


                if (dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                    Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;

                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = playerData.drag;
                    player.SetVelocity(playerData.dashVelocity, dashDirection);

                    player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();

                }
            }
            else
            {
                player.SetVelocity(playerData.dashVelocity, dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= startTime + playerData.dashTime)
                {
                    player.RB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }
}
