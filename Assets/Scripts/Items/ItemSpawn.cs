using Items.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private float respawnTime = 1.0f;
    [SerializeField] private float itemSpawnInterval; 
    [SerializeField] private int maxItemAmount = 1;

    private Vector2 lastSpawnPosition;

    void Start()
    {
        lastSpawnPosition = gameObject.transform.position;
        SpawnItem(lastSpawnPosition);

        StartCoroutine(ItemsSpawn());
    }

    private void SpawnItem(Vector2 itemPosition)
    {

        if (items.Count > 0)
        {
            Item item = items[Random.Range(0, items.Count)];
            int itemAmount = Random.Range(1, maxItemAmount);
            var itemContainer = new ItemContainer(item, itemAmount);
            lastSpawnPosition += new Vector2(itemSpawnInterval, 0f);

            SceneItemsSystem.Instance.DropItem(itemContainer, lastSpawnPosition);
        }
    }

    IEnumerator ItemsSpawn()
    {
        while (true)
        {
            SpawnItem(lastSpawnPosition);
            yield return new WaitForSeconds(respawnTime);
        }
    }
}
