using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerHealthbar : MonoBehaviour
{
    
    [SerializeField] private Image _valueImage = default;
    [SerializeField] private TextMeshProUGUI _valueText = default;

    private PlayerHealth _playerHealth;
    private float _lastHealth;

    private void Awake()
    {
        _playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        UpdateHealth();
    }

    private void Update()
    {
        if (_lastHealth != _playerHealth.CurrentHealth)
        {
            UpdateHealth();
        }
    }

    private void UpdateHealth()
    {
        _lastHealth = _playerHealth.CurrentHealth;
        _valueImage.fillAmount = Mathf.Clamp01(_playerHealth.CurrentHealth / _playerHealth.MaxHealth);
        _valueText.text = string.Format("+ {0}", _playerHealth.CurrentHealth);
    }

}
