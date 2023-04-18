﻿namespace Item.Core
{
    public class ItemContainer
    {
        public readonly Item Item;
        public int Amount { private set; get; }

        public ItemContainer(Item item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public ItemContainer(Item item)
        {
            Item = item;
            Amount = 1;
        }

        public void SetAmount(int amount)
        {
            Amount = amount;
        }
    }
}