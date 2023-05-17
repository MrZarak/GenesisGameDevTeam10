using System;
using NPC.Behaviour;
using NPC.Controller;
using NPC.Enum;
using UnityEngine;
using NPC.Data;
using Object = UnityEngine.Object;

namespace NPC.Spawn
{
    public class EntitiesFactory
    {
        private readonly Transform _entitiesContainer;
        private readonly EntitiesSpawnerDataStorage _entitiesSpawnerDataStorage;

        public EntitiesFactory(EntitiesSpawnerDataStorage entitiesSpawnerDataStorage)
        {
            _entitiesSpawnerDataStorage = entitiesSpawnerDataStorage;
            var gameObject = new GameObject
            {
                name = nameof(EntitySpawner)
            };
            _entitiesContainer = gameObject.transform;
        }

        public Entity GetEntityBrain(EntityId entityId, Vector2 position)
        {
            var data = _entitiesSpawnerDataStorage.EntitiesSpawnData.Find(element => element.Id == entityId);
            var baseEntityBehaviour = Object.Instantiate(data.EntityBehaviourPrefab, position, Quaternion.identity);
            baseEntityBehaviour.transform.SetParent(_entitiesContainer);
            switch (entityId)
            {
                case EntityId.King:
                case EntityId.Demon:
                    return new MeleeEntity(baseEntityBehaviour as MeleeEntityBehaviour);
                case EntityId.Sky:
                case EntityId.IceKing:
                    case EntityId.None:
                    default:
                    throw new NotImplementedException();
            }
        }
    }
}