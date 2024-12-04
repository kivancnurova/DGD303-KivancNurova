using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        // Rigidbody2D bileşenini al
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;

        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        rb.velocity = movementInput * moveSpeed;
    }
    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float angleDifference = Mathf.DeltaAngle(transform.eulerAngles.z, angle);

        float rotationSpeed = 5f;
        float maxRotationSpeed = 180f; // Maksimum dönüş hızı
        float rotationStep = Mathf.Clamp(angleDifference, -maxRotationSpeed, maxRotationSpeed);

        float smoothedAngle = transform.eulerAngles.z + rotationStep * Time.deltaTime * rotationSpeed;

        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle); 
    }

}


