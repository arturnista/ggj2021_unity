using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    
    [SerializeField] private bool _isAvailable = true;
    public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }

    public abstract void StartAttack();
    public abstract void StopAttack();
    public abstract void Disable();

}
