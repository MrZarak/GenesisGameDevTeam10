using Player;
using UnityEngine;

namespace Item.Core
{
    [CreateAssetMenu(fileName = "BaseItem", menuName = "Items/Base", order = 0)]
    public class BaseItem : Item
    {
        [SerializeField] private ItemId id;
        [SerializeField] private string baseName;
        [SerializeField] private string baseDescription;
        [SerializeField] private Sprite baseSprite;
        [SerializeField] private ItemRarity baseRarity;

        public override ItemId GetId() => id;

        public override ItemRarity GetRarity(ItemContainer itemContainer) => baseRarity;
        public override Sprite GetSprite(ItemContainer itemContainer) => baseSprite;
        public override string GetName(ItemContainer itemContainer) => baseName;
        public override string GetDescription(ItemContainer itemContainer) => baseDescription;
        public override int GetMaxDurability(ItemContainer itemContainer) => -1;

        public override void OnUse(PlayerEntity playerEntity, ItemContainer itemContainer)
        {
            Debug.Log($"USE WITH ITEM: {itemContainer.Item.GetId()}");
        }

        public override bool TryToPickup(PlayerEntity playerEntity, ItemContainer itemContainer)
        {
            Debug.Log($"ADDED {itemContainer.Item.GetName(itemContainer)} TO INV");
            return true;
        }
    }
}