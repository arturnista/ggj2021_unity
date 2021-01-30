using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    
    [SerializeField] private AudioClip _mainMusic = default;
    [SerializeField] private AudioClip _hordeMusic = default;
    private AudioSource _audioSource = default;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _mainMusic;
        _audioSource.Play();
    }

    public void PlayMusic()
    {
        StartCoroutine(ChangeSound(_mainMusic, .1f));
    }

    public void PlayHorde()
    {
        StartCoroutine(ChangeSound(_hordeMusic, .3f));
    }

    private IEnumerator ChangeSound(AudioClip music, float finalValue)
    {
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime * .1f;
            yield return null;
        }
        _audioSource.Stop();

        _audioSource.clip = music;
        
        _audioSource.volume = 0f;
        _audioSource.Play();
        while (_audioSource.volume < finalValue)
        {
            _audioSource.volume += Time.deltaTime * .2f;
            yield return null;
        }
    }

}
