using Items.Core;
using Items.Impl;

namespace Items.InventoryImpl
{
    public class EquipmentInventory : Inventory
    {
        private EquipmentType[] _equipmentTypes;

        public EquipmentInventory(EquipmentType[] equipmentTypes) : base(equipmentTypes.Length)
        {
            _equipmentTypes = equipmentTypes;
        }

        public override ItemContainer InsertItem(int index, ItemContainer item, bool simulate)
        {
            var inSlotType = _equipmentTypes[index];

            var goodEquip = item.Item is EquipmentItem equipmentItem && equipmentItem.EquipmentType == inSlotType;
            return goodEquip ? base.InsertItem(index, item, simulate) : item;
        }
    }
}