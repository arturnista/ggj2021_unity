using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomizer : MonoBehaviour
{
    
    [Header("Speed")]
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 5f;
    [Header("Materials")]
    [SerializeField] private List<Material> _randomMaterials = default;

    private void Awake()
    {
        GetComponent<NavMeshAgent>().speed = Random.Range(_minSpeed, _maxSpeed);
        GetComponentInChildren<SkinnedMeshRenderer>().material = _randomMaterials[Random.Range(0, _randomMaterials.Count)];
    }

}
