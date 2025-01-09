using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    Vector2 lastMovementDirection;

    void Update()
    {
        if (!Dialogue.isDialogueActive)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement != Vector2.zero)
            {
                lastMovementDirection = movement;
            }

            animator.SetFloat("Horizontal", lastMovementDirection.x);
            animator.SetFloat("Vertical", lastMovementDirection.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            movement = Vector2.zero;
            animator.SetFloat("Speed", 0f);
        }
    }

    void FixedUpdate()
    {
        if (!Dialogue.isDialogueActive)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
