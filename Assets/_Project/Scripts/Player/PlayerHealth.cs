using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    [Header("Feedback")]
    [SerializeField] private Image _feedbackImage = default;

    private float _currentHealth = 100f;

    private Animator _animator;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void DealDamage(float damage)
    {
        StartCoroutine(feedback(Color.red));
        _currentHealth -= damage;
        Debug.LogFormat("Health: {0}", _currentHealth);
    }

    public void AddHealth(float health)
    {
        StartCoroutine(feedback(Color.blue));
        _currentHealth = Mathf.Clamp(0f, _maxHealth, _currentHealth + health);
    }

    public IEnumerator feedback(Color color)
    {
        color.a = 0.5f;
        _feedbackImage.color = color;
        _feedbackImage.gameObject.SetActive(true);
        _feedbackImage.CrossFadeAlpha(0.01f, 0.7f, false);
        yield return new WaitForSeconds(1.3f);
        _feedbackImage.gameObject.SetActive(false);
    }

}
