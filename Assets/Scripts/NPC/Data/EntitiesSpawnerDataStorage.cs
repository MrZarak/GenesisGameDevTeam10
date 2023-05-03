using UnityEngine;
using System.Collections.Generic;

namespace NPC.Data
{
    [CreateAssetMenu(fileName = nameof(EntitiesSpawnerDataStorage), menuName = ("EntitiesSpawner/EntitiesSpawnerDataStorage"))]
    public class EntitiesSpawnerDataStorage : ScriptableObject
    {
        [field: SerializeField] public List<EntityDataStorage> EntitiesSpawnData { get; private set; }
    }
}


