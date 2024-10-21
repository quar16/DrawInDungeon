using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRemove : MonoBehaviour
{
    public AudioSource Audio;
    bool step1 = false;
    // Update is called once per frame
    void Update()
    {
        if (Audio.isPlaying)
            step1 = true;
        if (step1 && !Audio.isPlaying)
            Destroy(gameObject);
    }
}
