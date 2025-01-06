using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;

        FlipCharacter();

        UpdateFirePointPosition();

        UpdateAnimatorParameters();

        // RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        rb.velocity = movementInput * moveSpeed;
    }

    void FlipCharacter()
    {
        if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
            weaponHolder.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
            weaponHolder.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void UpdateFirePointPosition()
    {
        if(spriteRenderer.flipX)
        {
            firePoint.localPosition = new Vector3(-5.25f, -6.5f, 0);
        }
        else
        {
            firePoint.localPosition = new Vector3(5.25f, -6.5f, 0);
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

    // void RotateTowardsMouse() 
    // {
    //     Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     mousePosition.z = 0;

    //     Vector2 direction = (mousePosition - transform.position).normalized;

    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

    //     float angleDifference = Mathf.DeltaAngle(transform.eulerAngles.z, angle);

    //     float rotationSpeed = 5f;
    //     float maxRotationSpeed = 180f; // Maksimum dönüş hızı
    //     float rotationStep = Mathf.Clamp(angleDifference, -maxRotationSpeed, maxRotationSpeed);

    //     float smoothedAngle = transform.eulerAngles.z + rotationStep * Time.deltaTime * rotationSpeed;

    //     transform.rotation = Quaternion.Euler(0, 0, smoothedAngle); 
    // }

}


