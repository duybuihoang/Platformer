using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get
        {
            if(movement)
            {
                return movement;
            }
            Debug.LogError("No Movement Core Component on " + transform.parent.name);
            return null;

        }

        private set { movement = value; }

    }
    public CollisionSenses CollisionSenses
    {
        get
        {
            if (collisionsenses)
            {
                return collisionsenses;
            }
            Debug.LogError("No Collision Senses Core Component on " + transform.parent.name);
            return null;

        }

        private set { collisionsenses = value; }

    }

    private Movement movement;
    private CollisionSenses collisionsenses;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }

}
