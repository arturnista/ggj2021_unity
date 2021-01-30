using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] _pickUpSfx = default;
    private AudioSource _audioSource;

    private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(GetRandomPIckUpSfx());
    }
    AudioClip GetRandomPIckUpSfx()
    {
        return _pickUpSfx[Random.Range(0, _pickUpSfx.Length)];
    }
}
