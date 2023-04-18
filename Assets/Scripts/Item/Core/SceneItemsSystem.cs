using System;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Item.Core
{
    public class SceneItemsSystem : IDisposable
    {
        private Dictionary<SceneItem, ItemContainer> _itemsOnScene;
        private SceneItem _prefab;
        private GameObject _parentObject;
        private PlayerEntity _playerEntity;

        public SceneItemsSystem(PlayerEntity playerEntity)
        {
            _playerEntity = playerEntity;
            _prefab = Resources.Load<SceneItem>($"Prefabs/{nameof(SceneItemsSystem)}/{nameof(SceneItem)}");
            Debug.Log($"Prefabs/{nameof(SceneItemsSystem)}/{nameof(SceneItem)}");

            _itemsOnScene = new Dictionary<SceneItem, ItemContainer>();
            _parentObject = new GameObject
            {
                name = nameof(SceneItemsSystem)
            };
        }

        public void DropItem(ItemContainer itemContainer, Vector2 position)
        {
            var item = itemContainer.Item;

            var created = Object.Instantiate(_prefab, _parentObject.transform);

            created.transform.position = position;
            created.SetItem(
                item.GetSprite(itemContainer),
                item.GetName(itemContainer),
                Color.white //todo color system
            );

            created.PlayDropAnimation(position);
            created.RegisterPickupAction(TryToPickupItems);

            _itemsOnScene[created] = itemContainer;
        }

        private void TryToPickupItems(SceneItem sceneItem)
        {
            if (!sceneItem.CanBePickedUp()) return;

            var container = _itemsOnScene[sceneItem];

            var successful = container.Item.TryToPickup(_playerEntity, container);

            if (!successful) return;

            DetachSceneItem(sceneItem);
        }

        private void DetachSceneItem(SceneItem sceneItem)
        {
            sceneItem.UnregisterPickupAction(TryToPickupItems);
            _itemsOnScene.Remove(sceneItem);
            if (!sceneItem.gameObject.IsDestroyed())
            {
                Object.Destroy(sceneItem.gameObject);
            }
        }

        public void Dispose()
        {
            foreach (var sceneItem in _itemsOnScene.Keys)
            {
                DetachSceneItem(sceneItem);
            }
        }
    }
}