using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSound : MonoBehaviour
{
    public AudioSource Source;

    public float volumeChange = 1.5f;
    float maxVolume = .25f;
    float volume = 0;

    void Awake()
    {
        Source.volume = volume;
    }

    void Update()
    {
        if (Time.timeScale == 0)
            volume = 0;
        var input = ReadInput();
        if (input != Vector2.zero)
        {
            if (input.y != 0)
                maxVolume = .5f;
            else
                maxVolume = .25f;
            volume += volumeChange * Time.deltaTime;
        }
        else
        {
            volume -= volumeChange * Time.deltaTime;
        }
        volume = Mathf.Clamp(volume, 0, maxVolume);
        Source.volume = volume;
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
