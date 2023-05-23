using System;
using NPC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [Serializable]
    public class PlayerStats : EntityStats
    {
        [field: SerializeField] public int Money { get; private set; }
        [field: SerializeField] public int Xp { get; private set; }
        [SerializeField] private Mask hpMask;
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text xpText;

        private float _prevHealth;
        private float _prevMaxHealth;

        public void AddMoney(int money)
        {
            Money += money;
        }

        public void AddXp(int xp)
        {
            Money += xp;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
           
            moneyText.text = Money.ToString();
            xpText.text = Xp.ToString();
            if (_prevHealth != Health || _prevMaxHealth != MaxHealth)
            {
                var maskParentTransform = hpMask.rectTransform.parent.GetComponent<RectTransform>();
                var parentH = maskParentTransform.rect.height;
                var height = Math.Min(1F, Health / MaxHealth) * parentH;
                hpMask.rectTransform.anchoredPosition = new Vector2(0, -(parentH - height) / 2);
                hpMask.rectTransform.sizeDelta = new Vector2(hpMask.rectTransform.sizeDelta.x, height);
                _prevHealth = Health;
                _prevMaxHealth = MaxHealth;
            }
        }
    }
}