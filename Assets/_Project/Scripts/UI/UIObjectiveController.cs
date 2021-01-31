using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
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
        StartCoroutine(StartHordeCoroutine(5, 10f));
    }

    public void CompleteGetKey()
    {
        if (!_secondObjectiveCompleted || _thirdObjectiveCompleted) return;
        ObjectiveTextUpdate("Exit the Mall by the Emergency Exit.");
        _thirdObjectiveCompleted = true;
        StartCoroutine(StartFinalHordeCoroutine(10, 5f));
    }

    public void CompleteOpenEmergecyExit()
    {
        if (!_thirdObjectiveCompleted) return;
        SceneManager.LoadScene("Win");
    }

    private IEnumerator StartHordeCoroutine(int amount, float time)
    {
        yield return new WaitForSeconds(Random.Range(time - 5f, time + 5f));
        _spawnController.CreateHorde(amount);
    }

    private IEnumerator StartFinalHordeCoroutine(int amount, float time)
    {
        yield return new WaitForSeconds(Random.Range(time - 5f, time + 5f));
        _spawnController.CreateFinalHorde(amount);
    }

    void ObjectiveTextUpdate(string text)
    {
        _objectiveTMP.text = text;
    }

}
