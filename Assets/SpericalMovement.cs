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
    public Transform holder;

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
            var addMovement = input.y < 0f ?
                (transform.forward * -0.5f) * speed * Time.fixedDeltaTime :
                input.y == 0f ?
                (transform.forward * 0.5f) * speed * Time.fixedDeltaTime :
                (transform.forward * 1) * speed * Time.fixedDeltaTime;

            addMovement += transform.right * input.x * rotationSpeed * movement.magnitude;

            movement += addMovement * movementAdditive;
        }

        if (movement.sqrMagnitude > gateSpeedSqr)
        {
            var newPosition = transform.position + movement;
            var newPositionVector = newPosition.normalized;

            var projectedPosition = transform.position + movement * 2;
            var ray = new Ray(projectedPosition, -projectedPosition);
            var hitted = Physics.Raycast(ray, out RaycastHit hit, heightR, Layers.Ground);
            if (hitted)
            {
                var heightColor = landMap.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);
                Debug.Log(heightColor);
                if ((heightColor.r + heightColor.b + heightColor.g) / 3 > groundelevetion)
                {
                    movement = Vector3.zero;
                    return;
                }
            }

            transform.position = newPositionVector * height;
            var isMovingBackwards = Vector3.Dot(transform.forward, movement) < 0;

            var facingDirection = isMovingBackwards ?
                Vector3.ProjectOnPlane(-movement, newPositionVector).normalized :
                Vector3.ProjectOnPlane(movement, newPositionVector).normalized;
            transform.rotation = Quaternion.LookRotation(facingDirection, newPositionVector);
            holder.rotation = Quaternion.LookRotation(facingDirection, newPositionVector);

            movement *= (1 - movementDowngrade);
        }
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
}
