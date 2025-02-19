using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mendapatkan input untuk pergerakan horizontal dan vertikal
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Mengatur animasi dan arah sprite berdasarkan input
        if (movement.y > 0)
        {
            // Bergerak ke atas
            animator.SetBool("isWalking", true);
            animator.Play("WalkUp");
        }
        else if (movement.y < 0)
        {
            // Bergerak ke bawah
            animator.SetBool("isWalking", true);
            animator.Play("WalkDown");
        }
        else if (movement.x > 0)
        {
            // Bergerak ke kanan
            animator.SetBool("isWalking", true);
            animator.Play("Walk");
            spriteRenderer.flipX = false; // Tidak di-flip
        }
        else if (movement.x < 0)
        {
            // Bergerak ke kiri
            animator.SetBool("isWalking", true);
            animator.Play("Walk");
            spriteRenderer.flipX = true; // Flip sprite ke kiri
        }
        else
        {
            // Tidak bergerak
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        // Menggerakkan karakter berdasarkan input
        Vector2 position = transform.position;
        position += movement * speed * Time.fixedDeltaTime;
        transform.position = position;
    }
}
