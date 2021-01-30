using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskUsable : BaseUsable
{

    [SerializeField] private GameObject _blockedPassage = default;

    private Animator _animator;

    private bool _isOpen;
    
    private UIObjectiveController _objectiveController;

    private void Awake()
    {
        _objectiveController = GameObject.FindObjectOfType<UIObjectiveController>();
        _animator = GetComponent<Animator>();
        _isOpen = false;
    }

    public override void Use()
    {
        if (_isOpen || !_objectiveController.SecondObjectiveCompleted) return;
        _blockedPassage.SetActive(true);
        _isOpen = true;
        _animator.SetTrigger("Open");
        _objectiveController.CompleteGetKey();
    }

}
