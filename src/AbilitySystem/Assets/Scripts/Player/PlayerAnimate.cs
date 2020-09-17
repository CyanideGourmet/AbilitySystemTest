using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimate : MonoBehaviour
{
    Animator animator;
    bool movingLast = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        int direction = 0;
        Vector2 movementVector = callbackContext.ReadValue<Vector2>();
        if (movementVector != Vector2.zero)
        {
            if (Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.y))
            {
                direction = 1;
                if (movementVector.x < 0) direction = 2;
            }

            animator.SetTrigger("DirectionChange");

            switch (direction)
            {
                case 0:
                    {
                        animator.SetTrigger("YAxis");
                        break;
                    }
                case 1:
                    {
                        animator.SetTrigger("Right");
                        break;
                    }
                case 2:
                    {
                        animator.SetTrigger("Left");
                        break;
                    }
            }
        }
        if (movingLast && movementVector == Vector2.zero)
        {
            animator.SetTrigger("StartStopMovement");
            movingLast = false;
        }
        else if (!movingLast && movementVector != Vector2.zero)
        {
            animator.SetTrigger("StartStopMovement");
            movingLast = true;
        }
    }
}
