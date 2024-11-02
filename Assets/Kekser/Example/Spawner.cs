using System;
using Kekser.PowerBus;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kekser.Example
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private Vector3 _spawnArea;
        
        private void OnEnable()
        {
            gameObject.Bus<SpawnEvent>().OnChange += OnSpawnEvent;
        }
        
        private void OnDisable()
        {
            gameObject.Bus<SpawnEvent>().OnChange -= OnSpawnEvent;
            gameObject.DisposeBus<SpawnEvent>();
        }
        
        private void OnSpawnEvent(SpawnEvent spawnEvent)
        {
            for (int i = 0; i < spawnEvent.SpawnCount; i++)
            {
                var position = new Vector3(
                    Random.Range(-_spawnArea.x, _spawnArea.x),
                    Random.Range(-_spawnArea.y, _spawnArea.y),
                    Random.Range(-_spawnArea.z, _spawnArea.z)
                ) + transform.position;
                Instantiate(_prefab, position, Quaternion.identity);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, _spawnArea * 2);
        }
    }
}