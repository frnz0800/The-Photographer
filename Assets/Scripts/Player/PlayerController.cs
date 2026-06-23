using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    
    [SerializeField] private float stamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaUsageRatio;
    [SerializeField] private float staminaRegenRatio;

    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;

    private Vector2 movement;

    private float speed;

    private bool isWalking = false;
    private bool isRunning = false;

    public bool IsWalking => isWalking;
    public bool IsRunning => isRunning;
    
    public event Action<bool, bool> IsMoving;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isWalking = true;
        }
        else if (context.canceled)
        {
            isWalking = false;
        }

        IsMoving?.Invoke(isWalking, isRunning);

        movement = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed && isWalking)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }

        IsMoving?.Invoke(isWalking, isRunning);
    }

    private void Move()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        if (stamina <= 0f)
        {
            isRunning = false;

            IsMoving?.Invoke(isWalking, false);
        }

        if (isWalking && isRunning && stamina > 0)
        {
            stamina -= staminaUsageRatio * Time.fixedDeltaTime;

            speed = Mathf.Lerp(speed, runSpeed, Time.fixedDeltaTime * 5f);
        }
        else
        {
            if (stamina < 100f)
            {
                stamina += staminaRegenRatio * Time.fixedDeltaTime;
                stamina = Mathf.Clamp(stamina, 0f, maxStamina);
            }

            speed = Mathf.Lerp(speed, walkSpeed, Time.fixedDeltaTime * 2f);
        }

        Vector3 moveDirection = (forward.normalized * movement.y + right.normalized * movement.x) * speed;

        rb.AddForce(moveDirection, ForceMode.VelocityChange);
    }
    
    private void Start()
    {
       rb = GetComponent<Rigidbody>();

       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Move();
    }
}
