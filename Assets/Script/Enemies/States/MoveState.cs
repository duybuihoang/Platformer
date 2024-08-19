using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }


    private Movement movement;
    private CollisionSenses collisionSenses;


    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPLayerInMinAgroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = CollisionSenses.LedgeVertical;
        isDetectingWall = CollisionSenses.WallFront;
        isPLayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);

  

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(stateData.movementSpeed * Movement.FacingDirection);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
