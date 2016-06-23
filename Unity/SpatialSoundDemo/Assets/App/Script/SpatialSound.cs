using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpatialSound : MonoBehaviour
{
    public void Awake()
    {
        var audio = GetComponent<AudioSource>();
        audio.spatialize = true;
        audio.spatialBlend = 1.0f;
    }
}
