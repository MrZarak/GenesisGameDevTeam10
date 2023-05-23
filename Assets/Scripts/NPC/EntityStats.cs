using System;
using UnityEngine;

namespace NPC
{
    [Serializable]
    public class EntityStats
    {
        [field: SerializeField] public float MaxHealth { get; private set; } = 20;

        [field: SerializeField] public float Health { get; private set; } = 10;

        public void SetHealth(float health)
        {
            Health = Math.Min(health, MaxHealth);
        }

        public void Heal(float health)
        {
            SetHealth(Health + health);
        }

        public virtual void OnUpdate()
        {
        }
    }
}