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
    public Transform holder;

    private void Awake()
    {
        //landMap = duplicateTexture(landMap);
        transform.position = transform.position.normalized * height;
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, transform.position).normalized, transform.position.normalized);
        holder.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, transform.position).normalized, transform.position.normalized);
    }

    void FixedUpdate()
    {
        var input = ReadInput().normalized;

        if (input.y != 0)
        {
            var movement = input.y < 0f ?
                (transform.forward * -0.5f + transform.right * input.x * rotationSpeed * 0.5f) * speed * Time.fixedDeltaTime :
                input.y == 0f ?
                (transform.forward * 0.5f + transform.right * input.x * rotationSpeed) * speed * Time.fixedDeltaTime:
                (transform.forward * 1 + transform.right * input.x * rotationSpeed) * speed * Time.fixedDeltaTime;

            var newPosition = transform.position + movement;
            var newPositionVector = newPosition.normalized;

            transform.position = (transform.position + movement).normalized * height;

            var facingDirection = input.y < 0f ?
                Vector3.ProjectOnPlane(-movement, newPositionVector).normalized :
                Vector3.ProjectOnPlane(movement, newPositionVector).normalized;
            transform.rotation = Quaternion.LookRotation(facingDirection, newPositionVector);
            holder.rotation = Quaternion.LookRotation(facingDirection, newPositionVector);

            var ray = new Ray(newPosition, -newPosition);
            var hitted = Physics.Raycast(ray, out RaycastHit hit, heightR, Layers.Ground);
            if (hitted)
            {
                //Debug.Log(hit.textureCoord.x);
                //Debug.Log(hit.textureCoord.y);
                var heightColor = landMap.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);
                Debug.Log(heightColor);
                //heightColor = landMap.GetPixelBilinear(hit.textureCoord.y, hit.textureCoord.x);
                //Debug.Log(heightColor);
                //heightColor = landMap.GetPixelBilinear(1 - hit.textureCoord.x, 1 - hit.textureCoord.y);
                //Debug.Log(heightColor);
                //heightColor = landMap.GetPixelBilinear(1 - hit.textureCoord.y, 1 - hit.textureCoord.x);
                //Debug.Log(heightColor);
            }
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
