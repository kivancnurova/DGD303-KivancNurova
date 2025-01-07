using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    public Transform firePoint;
    public Transform weaponHolder;

    private Animator animator; 

    public WeaponManager weaponManager;

    void Start()
    {
        if(weaponManager == null)
        {
            weaponManager = FindObjectOfType<WeaponManager>();
        }
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;

        FlipCharacter();

        UpdateAnimatorParameters();
    }

    void FixedUpdate()
    {
        rb.velocity = movementInput * moveSpeed;
    }
    

    void FlipCharacter()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = transform.position;


        if (mousePosition.x > playerPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (mousePosition.x < playerPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }       
    }

    public bool IsFacingRight()
    {
        return !spriteRenderer.flipX;
    }


    void UpdateAnimatorParameters()
    {
        bool isWalking = movementInput.x != 0 || movementInput.y != 0;
        animator.SetBool("IsWalking", isWalking);

        if(isWalking)
        {
            float animationSpeed = rb.velocity.magnitude / moveSpeed;
            animator.speed = math.clamp(animationSpeed, 0.5f, 1.5f);
        }
        else
        {
            animator.speed = 1f;
        }
    }

}


