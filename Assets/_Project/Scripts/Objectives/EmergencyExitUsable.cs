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
