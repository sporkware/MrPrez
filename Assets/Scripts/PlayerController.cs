using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Core movement variables
    public float moveSpeed = BalanceConstants.MoveSpeed;
    public float rotationSpeed = 100f;
    public float jumpForce = BalanceConstants.JumpForce;

    // Combat variables
    public float fireRate = BalanceConstants.FireRate;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public ParticleSystem muzzleFlash;

    // Driving variables
    public bool isDriving = false;
    public Vehicle currentVehicle;

    // Health system
    public float maxHealth = BalanceConstants.MaxHealth;
    private float currentHealth;

    // Stats from GDD
    public float approvalRating = 50f;
    public float influence = 50f;
    public float wealth = 1000f;
    public float corruptionLevel = 0f;
    public float charm = 50f; // For adult interactions

    private Rigidbody rb;
    private float nextFireTime = 0f;
    private bool isGrounded = true;
    private int shotsFired = 0;
    private bool corruptAchieved = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        HandleMovement();
        HandleCombat();
        HandleDriving();
        HandleCheats();
        CheckAchievements();
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
        if (muzzleFlash)
        {
            muzzleFlash.Play();
        }
        // Play gunshot sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGunshot();
        }
        // Achievement
        shotsFired++;
        if (shotsFired == 1 && AchievementManager.Instance != null)
        {
            AchievementManager.Instance.UnlockAchievement("First Shot");
        }
    }

    void HandleDriving()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isDriving)
            {
                Vehicle nearby = FindNearbyVehicle();
                if (nearby != null)
                {
                    nearby.EnterVehicle(this);
                    currentVehicle = nearby;
                }
            }
            else
            {
                if (currentVehicle != null)
                {
                    currentVehicle.ExitVehicle(this);
                    currentVehicle = null;
                }
            }
        }
    }

    private Vehicle FindNearbyVehicle()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider col in colliders)
        {
            Vehicle v = col.GetComponent<Vehicle>();
            if (v != null && !v.isOccupied) return v;
        }
        return null;
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

    public void ModifyCharm(float amount)
    {
        charm = Mathf.Clamp(charm + amount, 0f, 100f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle death (respawn, game over, etc.)
        Debug.Log("Player died");
        // For now, disable
        gameObject.SetActive(false);
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    private void HandleCheats()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle cheat mode
            Debug.Log("Cheat mode toggled");
            // For example, god mode
            currentHealth = maxHealth;
            approvalRating = 100f;
            influence = 100f;
            wealth = 10000f;
            corruptionLevel = 0f;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Money cheat
            wealth += 10000f;
            Debug.Log("Added $10,000");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            // Health cheat
            currentHealth = maxHealth;
            Debug.Log("Health restored");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Approval cheat
            approvalRating = 100f;
            Debug.Log("Approval maxed");
        }
    }

    private void CheckAchievements()
    {
        if (corruptionLevel >= 50f && !corruptAchieved && AchievementManager.Instance != null)
        {
            corruptAchieved = true;
            AchievementManager.Instance.UnlockAchievement("Corrupt Politician");
        }
    }
}