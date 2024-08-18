using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();

        core.Movement.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        
        amountOfJumpLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        return amountOfJumpLeft > 0;
    }

    public void ReSetAmountOfJumpLeft()
    {
        amountOfJumpLeft = playerData.amountOfJumps;
    }

    public void DecreaseAmountOfJumpLeft() => amountOfJumpLeft--;

  
}
