using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskUsable : BaseUsable
{
    
    [SerializeField] private string _action = "";
    public string Action => _action;

    private Animator _animator;

    private bool _isOpen;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isOpen = false;
    }

    public override void Use()
    {
        if (_isOpen) return;
        _isOpen = true;
        _animator.SetTrigger("Open");
    }

}
