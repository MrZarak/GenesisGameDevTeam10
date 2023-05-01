using Items.Core;
using Player;
using UnityEngine;

namespace Items.Impl
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "Items/Equipment", order = 0)]
    public class EquipmentItem : BaseItem
    {
        [SerializeField] private float armorAmount;
        [field: SerializeField] public EquipmentType EquipmentType { private set; get; } = EquipmentType.None;

        public override int GetMaxStack(ItemContainer itemContainer)
        {
            return 1;
        }

        public override void OnUse(PlayerEntity playerEntity, ItemContainer itemContainer)
        {
        }

        public void OnEquipped(PlayerEntity playerEntity, ItemContainer itemContainer)
        {
            Debug.Log("OnEquipped");
        }

        public void OnUnequipped(PlayerEntity playerEntity, ItemContainer itemContainer)
        {
            Debug.Log("OnUnequipped");
        }
    }
}