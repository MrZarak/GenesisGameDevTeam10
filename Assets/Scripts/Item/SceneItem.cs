using System;
using UnityEngine;

namespace Item
{
    //todo artem pls implement logic
    public class SceneItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetItem(Sprite texture, string text, Color textColor)
        {
            spriteRenderer.sprite = texture;
            // тут тобі надходять дані для відмальовки
        }

        public void ActualizeItemData(Sprite texture, string text, Color textColor)
        {
            // тут просто порівнюй попереднє і теперішнє значення, якщо відмінні - то змінюй і перемальовуй
        }

        public void PlayDropAnimation(Vector2 targetPos)
        {
            /*
             ця функція викликається, коли айтем з'являєтсья на сцені і треба гарно його появу відобразити
             якщо лінь - то просто став це в targetPos і все)
            */
        }

        public void RegisterPickupAction(Action<SceneItem> action)
        {
            /*
             це тобі приходить callback, який треба викликати, коли юзер підбирає предмет, 
             збережи його собі десь, і коли треба викликай
            */
        }

        public void UnregisterPickupAction(Action<SceneItem> action)
        {
            /*
            як вище, тільки навпаки(якщо зберігаєш через event Action<SceneItem> action, то просто видали цей коллбек з евенту)
            */
        }

        public bool CanBePickedUp()
        {
            /*
           тут треба перевірити, и може юзер підняти предмет
           (дистанцію поки що тільки перевіряти), щось подібне в лекції було через Physic2D
          */
            return false;
        }
    }
}