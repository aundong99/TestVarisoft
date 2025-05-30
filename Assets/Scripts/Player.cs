using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float movementSpeed = 1f;
    public Healthbar healthbar;
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    [SerializeField] private InputActionReference moveActionTouse;
    [SerializeField] private float speed;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    //[Range(0.1f, 2f)]

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveDirection = moveActionTouse.action.ReadValue<Vector2>();
        Vector2 currentPos = rbody.position;
        Vector2 inputVector = Vector2.ClampMagnitude(moveDirection, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);

        
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            firingPoint.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }

        if (attackAction.action.triggered)
        {
            Debug.Log("Attack Triggered");
        }
    }

    private void shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }

    private void OnEnable()
    {
        attackAction.action.performed += OnAttack;
        attackAction.action.Enable();
    }

    private void OnDisable()
    {
        attackAction.action.performed -= OnAttack;
        attackAction.action.Disable();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            shoot();
        }
    }

    public void TakeDamage(int amount)
    {
        if (healthbar != null)
        {
            healthbar.TakeDamage(amount);
        }
    }
}