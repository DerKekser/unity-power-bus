using System;
using Kekser.PowerBus;
using UnityEngine;

namespace Kekser.Example
{
    public class BallDestroyer : MonoBehaviour
    {
        private void OnEnable()
        {
            gameObject.Bus<SpawnEvent>().On += OnSpawnEvent;
        }
        
        private void OnDisable()
        {
            gameObject.Bus<SpawnEvent>().On -= OnSpawnEvent;
            gameObject.DisposeBus<SpawnEvent>();
        }
        
        private void OnSpawnEvent(SpawnEvent spawnEvent)
        {
            Destroy(gameObject);
        }
    }
}