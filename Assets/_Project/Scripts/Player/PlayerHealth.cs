using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    public float MaxHealth => _maxHealth;
    [Header("Feedback")]
    [SerializeField] private Image _feedbackImage = default;
    [Header("SFX")]
    [SerializeField] private AudioClip _damageSfx = default;
    [SerializeField] private AudioClip _deathSfx = default;

    private float _currentHealth = 100f;
    public float CurrentHealth => _currentHealth;

    private Animator _animator;
    private bool _hasPlayedDamageSfx = false;
    private AudioSource _audioSource;
    private bool _dead = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void DealDamage(float damage)
    {
        _currentHealth -= damage;
        if (!_hasPlayedDamageSfx && _currentHealth > 0)
        {
            StartCoroutine(Feedback(Color.red));
            StartCoroutine(PlayDamageSfx());
        }
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            if (!_dead)
            {
                StartCoroutine(Death(Color.black));
            }
        }
        Debug.LogFormat("Health: {0}", _currentHealth);
    }

    public void AddHealth(float health)
    {
        StartCoroutine(Feedback(Color.blue));
        _currentHealth = Mathf.Clamp(_currentHealth + health, 0f, _maxHealth);
    }

    public IEnumerator Feedback(Color color)
    {
        color.a = 0.5f;
        _feedbackImage.color = color;
        _feedbackImage.gameObject.SetActive(true);
        _feedbackImage.CrossFadeAlpha(0.01f, 0.7f, false);
        yield return new WaitForSeconds(1.3f);
        _feedbackImage.gameObject.SetActive(false);
    }

    public IEnumerator Death(Color color)
    {
        _dead = true;
        color.a = 0.01f;
        _audioSource.PlayOneShot(_deathSfx);
        _feedbackImage.color = Color.black;
        _feedbackImage.gameObject.SetActive(true);
        _feedbackImage.CrossFadeAlpha(0.9f, 0.4f, false);
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator PlayDamageSfx()
    {
        _audioSource.PlayOneShot(_damageSfx);
        _hasPlayedDamageSfx = true;
        yield return new WaitForSeconds(0.5f);
        _hasPlayedDamageSfx = false;
    }
}
