using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorUsable : BaseUsable
{

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Use()
    {
        _animator.SetTrigger("Open");
        Destroy(this);
    }

}
