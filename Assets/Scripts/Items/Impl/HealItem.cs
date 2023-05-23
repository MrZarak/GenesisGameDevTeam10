﻿using Items.Core;
using Player;
using UnityEngine;

namespace Items.Impl
{
    [CreateAssetMenu(fileName = "HealItem", menuName = "Items/HealItem", order = 0)]
    public class HealItem : BaseItem
    {
        [field: SerializeField] public int HealPoints { private set; get; }

        public override int GetMaxStack(ItemContainer itemContainer)
        {
            return 0;
        }

        public override bool TryToPickup(PlayerEntity playerEntity, ItemContainer itemContainer)
        {
            playerEntity.PlayerStats.Heal(HealPoints);
            return true;
        }
    }
}