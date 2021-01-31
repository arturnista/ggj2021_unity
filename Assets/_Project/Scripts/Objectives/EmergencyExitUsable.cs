using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyExitUsable : BaseUsable
{
    
    private UIObjectiveController _objectiveController;

    private void Awake()
    {
        _objectiveController = GameObject.FindObjectOfType<UIObjectiveController>();
    }

    private void Update()
    {
        if (_objectiveController.SecondObjectiveCompleted)
        {
            if (!_objectiveController.ThirdObjectiveCompleted)
            {
                _action = "Locked";
            }
            else
            {
                _action = "Exit";
            }
        }
    }

    public override void Use()
    {
        if (!_objectiveController.SecondObjectiveCompleted)
        {
            _objectiveController.CompleteEmergencyExit();
        }
        else if (_objectiveController.ThirdObjectiveCompleted)
        {
            _objectiveController.CompleteOpenEmergecyExit();
        }
        else
        {
            Debug.Log("Trancado");
        }
    }

}
