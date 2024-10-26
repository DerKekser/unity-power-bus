using Kekser.PowerBus;
using UnityEngine;

namespace Kekser.Example
{
    public class InputHandler : MonoBehaviour
    {
        private int _spawnCount;
        
        private Bus<SpawnEvent> _spawnEventBus = new Bus<SpawnEvent>();
        
        public void SetInput(string input)
        {
            if (!int.TryParse(input, out int spawnCount))
                return;
            _spawnCount = spawnCount;
        }
        
        public void TriggerSpawn()
        {
            _spawnEventBus.Value = new SpawnEvent { SpawnCount = _spawnCount };
        }
    }
}