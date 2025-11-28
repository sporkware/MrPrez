using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Core movement variables
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 5f;

    // Combat variables
    public float fireRate = 0.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Driving variables
    public bool isDriving = false;
    public Vehicle currentVehicle;

    // Stats from GDD
    public float approvalRating = 50f;
    public float influence = 50f;
    public float wealth = 1000f;
    public float corruptionLevel = 0f;

    private Rigidbody rb;
    private float nextFireTime = 0f;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleCombat();
        HandleDriving();
    }

    void HandleMovement()
    {
        // Basic movement (walking)
        if (!isDriving)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.Self);

            // Rotation
            float rotate = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotate, 0f);

            // Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }

    void HandleCombat()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab && firePoint)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void HandleDriving()
    {
        // Placeholder for vehicle entry/exit
        if (Input.GetKeyDown(KeyCode.E) && !isDriving)
        {
            // Enter vehicle logic (to be implemented)
        }
        else if (Input.GetKeyDown(KeyCode.E) && isDriving)
        {
            // Exit vehicle logic
            isDriving = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Methods for stat management
    public void ModifyApproval(float amount)
    {
        approvalRating = Mathf.Clamp(approvalRating + amount, 0f, 100f);
    }

    public void ModifyInfluence(float amount)
    {
        influence = Mathf.Clamp(influence + amount, 0f, 100f);
    }

    public void ModifyWealth(float amount)
    {
        wealth += amount;
    }

    public void ModifyCorruption(float amount)
    {
        corruptionLevel = Mathf.Clamp(corruptionLevel + amount, 0f, 100f);
    }
}