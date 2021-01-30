using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstObjectTrigger : MonoBehaviour
{
    
    private UIObjectiveController _objectiveController;

    private void Awake()
    {
        _objectiveController = GameObject.FindObjectOfType<UIObjectiveController>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        _objectiveController.CompleteMainExit();
        Destroy(gameObject);
    }

}
