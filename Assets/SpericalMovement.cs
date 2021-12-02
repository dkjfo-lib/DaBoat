using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpericalMovement : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = .05f;
    [Space]
    public Texture2D landMap;
    [Space]
    public float height = 10;
    public float heightR = 10;
    [Space]
    [Range(0, 1)] public float movementAdditive = 1;
    [Range(0, 1)] public float movementDowngrade = 1;
    [Space]
    [Range(0, 1)] public float gateSpeedSqr = 1;
    [Space]
    [Range(0, 1)] public float groundelevetion = .3f;
    [Space]
    public float fuelUsage = 1;
    public ClampedValue fuel;
    [Space]
    public Transform holder;
    [Space]
    public float addangle = 3;
    public float addangleSpeed = .2f;
    [Space]
    public float wiggleAmplitude = 3;
    public float wiggleSpeed = .2f;

    Vector3 movement = Vector3.zero;

    private void Awake()
    {
        transform.position = transform.position.normalized * height;
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, transform.position).normalized, transform.position.normalized);
        holder.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, transform.position).normalized, transform.position.normalized);
    }

    void FixedUpdate()
    {
        var input = ReadInput().normalized;

        if (input != Vector2.zero)
        {
            var _speed = speed;
            var _rotationSpeed = rotationSpeed;
            fuel.value -= fuelUsage * Time.fixedDeltaTime;
            if (fuel.value < 0)
            {
                fuel.value = 0;
                _speed /= 2;
                _rotationSpeed /= 2;
            }

            var addMovement = input.y < 0f ?
                (transform.forward * -0.5f) * _speed * Time.fixedDeltaTime :
                input.y == 0f ?
                (transform.forward * 0.5f) * _speed * Time.fixedDeltaTime :
                (transform.forward * 1) * _speed * Time.fixedDeltaTime;

            addMovement += transform.right * input.x * _rotationSpeed * movement.magnitude;

            movement += addMovement * movementAdditive;
        }

        var newPositionVector = transform.position.normalized;
        var facingDirection = transform.forward;
        if (movement.sqrMagnitude > gateSpeedSqr)
        {
            var newPosition = transform.position + movement;
            newPositionVector = newPosition.normalized;

            var projectedPosition = transform.position + movement * 2;
            var ray = new Ray(projectedPosition, -projectedPosition);
            var hitted = Physics.Raycast(ray, out RaycastHit hit, heightR, Layers.Ground);
            d_hitted = "hitted: " + hitted;
            if (hitted)
            {
                var heightColor = landMap.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);
                Debug.Log(heightColor.r);
                //Debug.Log(heightColor);
                d_uv = "uv: " + hit.textureCoord.x.ToString(".000") + " : " + hit.textureCoord.y.ToString(".000");
                d_red = "red: " + (heightColor.r * 100);
                if (heightColor.r > groundelevetion)
                {
                    movement = Vector3.zero;
                    return;
                }
            }

            transform.position = newPositionVector * height;
            var isMovingBackwards = Vector3.Dot(transform.forward, movement) < 0;

            facingDirection = isMovingBackwards ?
                Vector3.ProjectOnPlane(-movement, newPositionVector).normalized :
                Vector3.ProjectOnPlane(movement, newPositionVector).normalized;

            movement *= (1 - movementDowngrade);
        }
        var changeInDirection = facingDirection - transform.forward;
        newPositionVector -= changeInDirection * addangle;
        newPositionVector = Vector3.Lerp(transform.up, newPositionVector, addangleSpeed);

        var timeValue = Time.timeSinceLevelLoad * Mathf.PI * wiggleSpeed;
        var wigglePhase = Mathf.Sin(timeValue);
        newPositionVector += transform.right * wigglePhase * wigglePhase * Mathf.Sign(wigglePhase) * wiggleAmplitude;

        transform.rotation = Quaternion.LookRotation(facingDirection, newPositionVector);
        holder.rotation = Quaternion.LookRotation(facingDirection, newPositionVector);
    }

    Vector2 ReadInput()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            input += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            input -= Vector2.up;
        if (Input.GetKey(KeyCode.D))
            input += Vector2.right;
        if (Input.GetKey(KeyCode.A))
            input -= Vector2.right;
        return input;
    }

    string d_hitted;
    string d_red;
    string d_uv;
    void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 100, 20), d_hitted);
        //GUI.Label(new Rect(10, 50, 100, 20), d_uv);
        //GUI.Label(new Rect(10, 100, 100, 20), d_red);
    }
}
