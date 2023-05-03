using UnityEngine;
using NPC.Enum;
using NPC.Behaviour;
namespace NPC.Data
{
    [CreateAssetMenu(fileName = nameof(EntityDataStorage), menuName = ("EntitiesSpawner/EntityDataStorage"))]
    public class EntityDataStorage : ScriptableObject
    {
        [field: SerializeField] public EntityId Id { get; private set; }
        [field: SerializeField] public BaseEntityBehaviour EntityBehaviourPrefab { get; private set; }
    }
}

