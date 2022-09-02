using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopviewController : MonoBehaviour
{
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight;
    [SerializeField] float stepSmooth;

    Rigidbody rigidBody;

    [SerializeField] private Vector3 movementDirection;

    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float turnSpeed = 0.3f;

    public float movementSmoothingSpeed = 13f;
    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        //stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }

    private void FixedUpdate()
    {
        MoveThePlayer();
        TurnThePlayer();
        stepClimb();
        GroundToFloor();
    }

    void stepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                rigidBody.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }

        RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
        {

            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
            {
                rigidBody.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
        {

            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
            {
                rigidBody.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }

    void GroundToFloor()
    {
        RaycastHit hitfloor;
        if (!Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-transform.up), out hitfloor, 0.15f))
        {
            rigidBody.position -= new Vector3(0f, stepSmooth, 0f);
        }
    }

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        movementDirection = newMovementDirection;
    }

    private void Update()
    {
        InputvalueUpdate();
        CalculateMovementInputSmoothing();
        UpdateMovementData(smoothInputMovement);
    }

    void MoveThePlayer()
    {
        //movementDirection.y = playerRigidbody.transform.position.y;
        Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.transform.position + new Vector3(movement.x, 0, movement.z));
    }

    void TurnThePlayer()
    {
        if (movementDirection.sqrMagnitude > 0.01f)
        {

            Quaternion rotation = Quaternion.Slerp(rigidBody.rotation,
                                                 Quaternion.LookRotation(movementDirection),
                                                 turnSpeed);

            rigidBody.MoveRotation(rotation);
        }
    }

    void CalculateMovementInputSmoothing()
    {
        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
    }

    void InputvalueUpdate()
    {
        var verinput = Input.GetAxis("Vertical");
        var horinput = Input.GetAxis("Horizontal");
        Vector2 input = new Vector2(horinput, verinput);
        //Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        rawInputMovement = new Vector3(-input.x, 0, -input.y);
        //Debug.Log($"<color=red>{input}</color>");
        float _value = Vector3.Magnitude(input);
    }
}
