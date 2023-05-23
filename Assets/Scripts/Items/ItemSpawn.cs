using System.Collections;
using System.Collections.Generic;
using Items.Core;
using UnityEngine;

namespace Items
{
    public class ItemSpawn : MonoBehaviour
    {
        [SerializeField] private List<Item> items = new List<Item>();
        [SerializeField] private float initialDelay = 5.0f;
        [SerializeField] private float respawnTime = 1.0f;
        [SerializeField] private float itemSpawnSpreadRange = 3f;
        [SerializeField] private int minItemAmount = 1;
        [SerializeField] private int maxItemAmount = 1;
        [SerializeField] private int maxSpawnIterations = 1;

        void Start()
        {
            StartCoroutine(ItemsSpawn());
        }

        private void SpawnItem()
        {
            if (items.Count <= 0 || minItemAmount <= 0 || maxItemAmount < minItemAmount) return;

            var item = items[Random.Range(0, items.Count)];
            var itemAmount = Random.Range(minItemAmount, maxItemAmount);
            var itemContainer = new ItemContainer(item, itemAmount);

            var randX = Random.value - 0.5F;
            var randY = Random.value - 0.5F;
            var center = gameObject.transform.position;
            var pos = new Vector2(itemSpawnSpreadRange * randX + center.x, itemSpawnSpreadRange * randY + center.y);

            SceneItemsSystem.Instance.DropItem(itemContainer, pos);
        }

        private IEnumerator ItemsSpawn()
        {
            yield return new WaitForSeconds(initialDelay);
            var i = 0;
            while (maxSpawnIterations > i)
            {
                SpawnItem();
                i++;
                yield return new WaitForSeconds(respawnTime);
            }
        }
    }
}