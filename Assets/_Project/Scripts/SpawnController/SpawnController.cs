using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OChanzSohJogaGuensinAqueleGachaDeArrombado
{

    public class SpawnController : MonoBehaviour
    {
        
        [SerializeField] private GameObject _enemyPrefab = default;

        private List<Transform> _points;
        private GameObject _player;

        private void Awake()
        {
            _points = new List<Transform>();
            foreach (Transform item in transform)
            {
                _points.Add(item);
            }
        }

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(CreateEnemiesCoroutine());
        }

        private IEnumerator CreateEnemiesCoroutine()
        {
            CreateEnemies(10);

            while (true)
            {
                yield return new WaitForSeconds(30f);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                 if (enemies.Length == 0)
                {
                    CreateEnemies(3, true);
                }
                else if (enemies.Length < 5)
                {
                    CreateEnemies(5);
                }
            }
        }

        private void CreateEnemies(int amount, bool force = false)
        {
            var availablePoints = GetAvailablePoints();

            for (int i = 0; i < amount; i++)
            {
                Vector3 spawn = availablePoints[Random.Range(0, availablePoints.Count)].position;
                Vector2 randomness = Random.insideUnitCircle;

                EnemyMovement movement = Instantiate(_enemyPrefab, spawn + new Vector3(randomness.x, 0f, randomness.y), Quaternion.identity).GetComponent<EnemyMovement>();
                if (force) movement.ForceTarget();
            }
        }

        private List<Transform> GetAvailablePoints()
        {
            
            List<Transform> availablePoints = new List<Transform>();
            foreach (var item in _points)
            {
                RaycastHit hit;
                if (Physics.Linecast(item.position + Vector3.up, _player.transform.position, out hit))
                {
                    if (!hit.transform.gameObject.CompareTag("Player"))
                    {
                        availablePoints.Add(item);
                    }
                }
            }

            return availablePoints;
        }

    }

}

