using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIObjectiveController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _objectiveTMP = default; 
    private bool _firstObjectiveCompleted = false;
    private bool _secondObjectiveCompleted = false;
    private bool _thirdObjectiveCompleted = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_firstObjectiveCompleted)
        {
            ObjectiveTextUpdate("Find the exit door.");
        }

        if (_firstObjectiveCompleted && !_secondObjectiveCompleted)
        {
            ObjectiveTextUpdate("Find the Emergency Exit.");
        }

        if (_firstObjectiveCompleted && _secondObjectiveCompleted && !_thirdObjectiveCompleted)
        {
            ObjectiveTextUpdate("Find the key in the Security and exit the Mall by the Emergency Exit.");
        }

    }

    void ObjectiveTextUpdate(string text)
    {
        _objectiveTMP.text = text;
    }

}
