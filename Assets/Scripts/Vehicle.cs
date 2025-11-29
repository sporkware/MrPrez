using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Vehicle : MonoBehaviour
{
    public float maxSpeed = BalanceConstants.VehicleMaxSpeed;
    public float acceleration = BalanceConstants.VehicleAcceleration;
    public float brakeForce = 20f;
    public float turnSpeed = 2f;
    public float maxSteerAngle = 30f;
    public float maxFuel = BalanceConstants.VehicleFuelCapacity;
    public float fuelConsumption = BalanceConstants.VehicleFuelConsumption;

    public WheelCollider[] wheelColliders;
    public Transform[] wheelTransforms;

    private Rigidbody rb;
    private float currentSpeed;
    private float steerAngle;
    private float currentFuel;
    public bool isOccupied = false;
    private static bool firstDrive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentFuel = maxFuel;
    }

    void FixedUpdate()
    {
        if (isOccupied)
        {
            HandleMovement();
        }
        UpdateWheels();
    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration and braking
        if (verticalInput > 0 && currentFuel > 0)
        {
            foreach (WheelCollider wheel in wheelColliders)
            {
                wheel.motorTorque = verticalInput * acceleration;
                wheel.brakeTorque = 0;
            }
            currentFuel -= fuelConsumption * Time.deltaTime;
        }
        else if (verticalInput < 0)
        {
            foreach (WheelCollider wheel in wheelColliders)
            {
                wheel.motorTorque = 0;
                wheel.brakeTorque = -verticalInput * brakeForce;
            }
        }
        else
        {
            foreach (WheelCollider wheel in wheelColliders)
            {
                wheel.motorTorque = 0;
                wheel.brakeTorque = brakeForce * 0.1f; // Light braking to slow down
            }
        }

        // Steering
        steerAngle = horizontalInput * maxSteerAngle;
        wheelColliders[0].steerAngle = steerAngle;
        wheelColliders[1].steerAngle = steerAngle;

        currentSpeed = rb.velocity.magnitude;
    }

    void UpdateWheels()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            UpdateWheelPose(wheelColliders[i], wheelTransforms[i]);
        }
    }

    void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }

    public void EnterVehicle(PlayerController player)
    {
        player.transform.SetParent(transform);
        player.transform.localPosition = Vector3.zero;
        player.isDriving = true;
        player.gameObject.SetActive(false); // Hide player model
        isOccupied = true;
        // Achievement
        if (!firstDrive && AchievementManager.Instance != null)
        {
            firstDrive = true;
            AchievementManager.Instance.UnlockAchievement("First Drive");
        }
    }

    public void ExitVehicle(PlayerController player)
    {
        player.transform.SetParent(null);
        player.transform.position = transform.position + transform.right * 2f; // Exit to side
        player.isDriving = false;
        player.gameObject.SetActive(true);
        isOccupied = false;
    }

    public void Refuel(float amount)
    {
        currentFuel = Mathf.Min(currentFuel + amount, maxFuel);
    }

    public float GetFuelPercentage()
    {
        return currentFuel / maxFuel;
    }
}
