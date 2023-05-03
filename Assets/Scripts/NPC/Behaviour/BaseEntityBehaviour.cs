using UnityEngine;
using Core.Animation;
using UnityEngine.Rendering;
using Core.Movement.Controller;

namespace NPC.Behaviour
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BaseEntityBehaviour : MonoBehaviour
    {
        [SerializeField] protected AnimatorController Animator;
        [SerializeField] private SortingGroup _sortingGroup;

        protected Rigidbody2D Rigidbody;
        protected DirectionalMover DirectionalMover;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public float VerticalPosition => transform.position.y;

        public void SetDrawingOrder(int order) => _sortingGroup.sortingOrder = order;
        public void SetSize(Vector2 size) => transform.localScale = size;
        public void MoveHorizontally(float direction) => DirectionalMover.MoveHorizontally(direction);
        public virtual void MoveVertically(float direction) => DirectionalMover.MoveVertically(direction);

        public void SetVerticalPosition(float verticalPosition) =>
            Rigidbody.position = new Vector2(Rigidbody.position.x, verticalPosition);

        protected virtual void UpdateAnimations()
        {
            Animator.SetAnimationState(AnimationType.Idle, true);
            Animator.SetAnimationState(AnimationType.Run, DirectionalMover.IsMoving);
        }
    }
}