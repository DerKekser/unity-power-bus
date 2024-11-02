using System;
using Kekser.PowerBus;
using UnityEngine;

namespace Kekser.Example
{
    public class InputHandler : MonoBehaviour
    {
        private int _spawnCount;
        
        public void SetInput(string input)
        {
            if (!int.TryParse(input, out int spawnCount))
                return;
            _spawnCount = spawnCount;
        }
        
        public void TriggerSpawn()
        {
            Bus.Trigger(new SpawnEvent { SpawnCount = _spawnCount });
        }
    }
}