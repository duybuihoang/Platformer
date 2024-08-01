using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDectectingLedge;
    protected bool isPLayerInMinAgroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);

        isDectectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPLayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isDectectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPLayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

    }
}
