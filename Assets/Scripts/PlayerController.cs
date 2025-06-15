using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool enableAimAssist = true;

    private Rigidbody rb;
    private bool lastUsedGamepad = false;

    private bool isShooting = false;
    private float shootCooldown = 0.8f;
    private float lastShootTime = 0f;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        if (!lastUsedGamepad && Mouse.current != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane groundPlane = new Plane(Vector3.up, transform.position);

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
        else if (lookInput.sqrMagnitude > 0.1f)
        {
            Vector3 lookDirection = new Vector3(lookInput.x, 0, lookInput.y).normalized;
            Vector3 playerPosition = transform.position;

            if (enableAimAssist)
            {
                Collider[] enemies = Physics.OverlapSphere(playerPosition, 5f, enemyLayer);
                Transform bestTarget = null;
                float bestDot = 0.93f; // tighter cone ~25°

                foreach (Collider enemy in enemies)
                {
                    Vector3 toEnemy = (enemy.transform.position - playerPosition).normalized;
                    toEnemy.y = 0;

                    float dot = Vector3.Dot(lookDirection, toEnemy);
                    if (dot > bestDot)
                    {
                        bestDot = dot;
                        bestTarget = enemy.transform;
                    }
                }

                if (bestTarget != null)
                {
                    Vector3 targetDir = (bestTarget.position - playerPosition).normalized;
                    targetDir.y = 0;
                    lookDirection = targetDir;
                }
            }

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            Quaternion smoothRotation = Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRotation);
        }
        else if (lastUsedGamepad && moveInput.sqrMagnitude > 0.1f)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            Quaternion smoothRotation = Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRotation);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

        if (context.control.device is Gamepad)
        {
            lastUsedGamepad = true;
        }
        else
        {
            lastUsedGamepad = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            isShooting = true;
        else if (context.canceled)
            isShooting = false;
    }

    private void Shoot()
    {
        if (bulletPrefab == null || shootOrigin == null) return;

        GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, shootOrigin.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.speed = 30f;
            bulletScript.damage = 2f;
        }
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
