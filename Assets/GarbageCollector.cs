using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public ClampedValue GarbageCapacity;
    [Space]
    public Pipe_SoundsPlay Pipe_SoundsPlay;
    public ClipsCollection onCollected;

    private void OnTriggerEnter(Collider other)
    {
        var garbage = other.GetComponent<Garbage>();
        if (garbage != null)
        {
            if (GarbageCapacity.value < GarbageCapacity.maxValue)
            {
                garbage.timeToLive = 0;

                GarbageCapacity.value += 1;
                Pipe_SoundsPlay.AddClip(new PlayClipData(onCollected, transform.position, transform));
            }
        }
    }
}
