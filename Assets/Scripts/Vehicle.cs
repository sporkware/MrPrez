using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Vehicle : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 10f;
    public float brakeForce = 20f;
    public float turnSpeed = 2f;
    public float maxSteerAngle = 30f;

    public WheelCollider[] wheelColliders;
    public Transform[] wheelTransforms;

    private Rigidbody rb;
    private float currentSpeed;
    private float steerAngle;
    public bool isOccupied = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (verticalInput > 0)
        {
            foreach (WheelCollider wheel in wheelColliders)
            {
                wheel.motorTorque = verticalInput * acceleration;
                wheel.brakeTorque = 0;
            }
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
    }

    public void ExitVehicle(PlayerController player)
    {
        player.transform.SetParent(null);
        player.transform.position = transform.position + transform.right * 2f; // Exit to side
        player.isDriving = false;
        player.gameObject.SetActive(true);
        isOccupied = false;
    }
}