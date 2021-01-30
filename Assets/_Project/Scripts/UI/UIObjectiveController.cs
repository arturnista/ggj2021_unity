using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OChanzSohJogaGuensinAqueleGachaDeArrombado;

public class UIObjectiveController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _objectiveTMP = default; 
    
    private bool _firstObjectiveCompleted = false;
    public bool FirstObjectiveCompleted { get => _firstObjectiveCompleted; }

    private bool _secondObjectiveCompleted = false;
    public bool SecondObjectiveCompleted { get => _secondObjectiveCompleted; }

    private bool _thirdObjectiveCompleted = false;
    public bool ThirdObjectiveCompleted { get => _thirdObjectiveCompleted; }

    private SpawnController _spawnController;

    void Start()
    {
        _spawnController = GameObject.FindObjectOfType<SpawnController>();
        ObjectiveTextUpdate("Find the exit door.");
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (!_firstObjectiveCompleted)
    //     {
    //         ObjectiveTextUpdate("Find the exit door.");
    //     }

    //     if (_firstObjectiveCompleted && !_secondObjectiveCompleted)
    //     {
    //     }

    //     if (_firstObjectiveCompleted && _secondObjectiveCompleted && !_thirdObjectiveCompleted)
    //     {
    //     }

    //     if (_firstObjectiveCompleted && _secondObjectiveCompleted && _thirdObjectiveCompleted)
    //     {
    //     }

    // }

    public void CompleteMainExit()
    {
        if (_firstObjectiveCompleted) return;
        ObjectiveTextUpdate("Find the Emergency Exit.");
        _firstObjectiveCompleted = true;
    }

    public void CompleteEmergencyExit()
    {
        if (_secondObjectiveCompleted) return;
        ObjectiveTextUpdate("Find the key in the Security Office.");
        _firstObjectiveCompleted = true;
        _secondObjectiveCompleted = true;
        StartCoroutine(StartHordeCoroutine(5));
    }

    public void CompleteGetKey()
    {
        if (!_secondObjectiveCompleted || _thirdObjectiveCompleted) return;
        ObjectiveTextUpdate("Exit the Mall by the Emergency Exit.");
        _thirdObjectiveCompleted = true;
        StartCoroutine(StartHordeCoroutine(10));
    }

    public void CompleteOpenEmergecyExit()
    {
        if (!_thirdObjectiveCompleted) return;
        ObjectiveTextUpdate("Cabo.");
    }

    private IEnumerator StartHordeCoroutine(int amount)
    {
        yield return new WaitForSeconds(Random.Range(10f, 25f));
        _spawnController.CreateHorde(amount);
    }

    void ObjectiveTextUpdate(string text)
    {
        _objectiveTMP.text = text;
    }

}
