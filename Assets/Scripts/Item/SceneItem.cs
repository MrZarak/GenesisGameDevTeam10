using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;
namespace Item
{
    public class SceneItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TMP_Text tMP_Text;
        [SerializeField] private Button button;
        [SerializeField] private Canvas canvas;
        [SerializeField] private LayerMask mask;
        [SerializeField] private float InteractionDisntance;

        [Header("SizeControl")]
        [SerializeField] private float minSize;
        [SerializeField] private float maxSize;
        [SerializeField] private float minVerticalPosition;
        [SerializeField] private float maxVerticalPosition;
        [SerializeField] private Transform itemTransform;

        [Header("DropAnimation")]
        [SerializeField] private float dropAnimDuration;
        [SerializeField] private float dropRotation;
        [SerializeField] private float dropRadius;

        private float sizeModificator;
        private Sequence sequence;

        private bool textEnabled = true;
        public bool TextEnabled
        {
            set
            {
                if (textEnabled != value)
                {
                    return;
                }

                textEnabled = true;
                canvas.enabled = false;
            }
        }

        private event Action<SceneItem> ItemClicked;

        private void Awake()
        {
            button.onClick.AddListener(() => ItemClicked?.Invoke(this));
            float positionDifference = maxVerticalPosition - minVerticalPosition;
            float sizeDifference = maxSize - minSize;
            sizeModificator = sizeDifference / positionDifference;
        }

        private void OnMouseDown() => ItemClicked?.Invoke(this);

        public void SetItem(Sprite texture, string text, Color textColor)
        {
            spriteRenderer.sprite = texture;
            tMP_Text.text = text;
            tMP_Text.color = textColor;
            canvas.enabled = false;
        }

        private void UpdateSize()
        {
            float verticalDelta = maxVerticalPosition - itemTransform.position.y;
            float currentSizeModificator = minSize + sizeModificator * verticalDelta;
            itemTransform.localScale = Vector2.one * currentSizeModificator;
        }

        public void ActualizeItemData(Sprite texture, string text, Color textColor)
        {
            if (spriteRenderer.sprite != texture)
            {
                spriteRenderer.sprite = texture;
            }
            else if (tMP_Text.text != text)
            {
                tMP_Text.text = text;
            }
            else if (tMP_Text.color != textColor)
            {
                tMP_Text.color = textColor;
            }
        }

        public void PlayDropAnimation(Vector2 targetPos)
        {
            transform.position = targetPos;
            Vector2 movePosition = transform.position + new Vector3(Random.Range(-dropRadius, dropRadius), 0, 0);
            sequence = DOTween.Sequence();
            sequence.Join(transform.DOMove(movePosition, dropAnimDuration));
            sequence.Join(itemTransform.DORotate
                (new Vector3(0, 0, Random.Range(-dropRotation, dropRotation)), dropAnimDuration));
            sequence.OnComplete(() => canvas.enabled = textEnabled);
        }

        public void RegisterPickupAction(Action<SceneItem> action)
        {
            ItemClicked += action;
        }

        public void UnregisterPickupAction(Action<SceneItem> action)
        {
            ItemClicked -= action;
        }
        
        public bool CanBePickedUp()
        {
            Collider2D player = Physics2D.OverlapCircle(this.gameObject.transform.position, InteractionDisntance, mask);
            return player != null;
        }
    }
}