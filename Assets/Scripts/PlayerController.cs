using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private float bulletSpeed = 20f;

    private Rigidbody rb;
    private bool lastUsedGamepad = false;

    private bool isShooting = false;
    private float shootCooldown = 0.8f;
    private float lastShootTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        var actions = GetComponent<PlayerInput>().actions;
        Vector2 movementInput = actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        // Check last used device
        if (Gamepad.current != null && (movementInput.magnitude > 0.1f))
        {
            lastUsedGamepad = true;
        }
        else if (Mouse.current != null && Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f)
        {
            lastUsedGamepad = false;
        }

        if (!lastUsedGamepad && Mouse.current != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 lookDirection = (hitPoint - transform.position).normalized;
                lookDirection.y = 0;

                if (lookDirection.sqrMagnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    Quaternion smoothRotation = Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime);
                    rb.MoveRotation(smoothRotation);
                }
            }
        }
        else
        {
            Vector2 lookInput = actions["Look"].ReadValue<Vector2>();
            if (lookInput.sqrMagnitude > 0.1f)
            {
                Vector3 lookDirection = new Vector3(lookInput.x, 0, lookInput.y);
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                Quaternion smoothRotation = Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime);
                rb.MoveRotation(smoothRotation);
            }
        }
    }
    private void Shoot()
    {
        if (bulletPrefab == null || shootOrigin == null) return;

        GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, shootOrigin.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.speed = bulletSpeed;
            bulletScript.damage = 2f;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            isShooting = true;
        else if (context.canceled)
            isShooting = false;
    }

    private void Update()
    {
        if (isShooting && Time.time - lastShootTime >= shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }
}
