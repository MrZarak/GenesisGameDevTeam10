using System;
using Core.Animation;
using Core.Enums;
using Core.Movement.Controller;
using UnityEngine;

namespace NPC.Behaviour
{
    public class MeleeEntityBehaviour : BaseEntityBehaviour
    {
        [SerializeField] private float _afterAttackDelay;
        [SerializeField] private Collider2D _collider2D;
        
        [field: SerializeField] public Vector2 SearchBox { get; private set; }
        [field: SerializeField] public LayerMask Targets { get; private set; }

        public Vector2 Size => _collider2D.bounds.size;

        public event Action AttackSequenceEnded;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, SearchBox);
        }

        protected override void Awake()
        {
            base.Awake();
            DirectionalMover = new PositionMover(Rigidbody);
        }

        private void Update() => UpdateAnimations();

        public void StartAttack() => Animator.SetAnimationState(AnimationType.Attack, true, Attack, EndAttack);

        public void SetDirection(Direction direction) => DirectionalMover.SetDirection(direction);

        private void Attack()
        {
            Debug.Log("Attack");
        }

        private void EndAttack()
        {
            Animator.SetAnimationState(AnimationType.Attack, false);
            Invoke(nameof(EndAttackSequnce), _afterAttackDelay);
        }

        private void EndAttackSequnce()
        {
            AttackSequenceEnded?.Invoke();
        }


    }
}