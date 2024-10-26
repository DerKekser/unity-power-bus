using System;
using Kekser.PowerBus;
using UnityEngine;

namespace Kekser.Example
{
    public class BallDestroyer : MonoBehaviour
    {
        private Bus<SpawnEvent> _spawnEventBus = new Bus<SpawnEvent>();

        private void OnEnable()
        {
            _spawnEventBus.OnChange += OnSpawnEvent;
        }
        
        private void OnDisable()
        {
            _spawnEventBus.OnChange -= OnSpawnEvent;
        }
        
        private void OnSpawnEvent(SpawnEvent spawnEvent)
        {
            Destroy(gameObject);
        }
    }
}