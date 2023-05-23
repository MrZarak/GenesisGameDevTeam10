using System;
using Core.Animation;
using Core.Movement.Controller;
using Core.Movement.Data;
using Core.Tools;
using Drawing;
using Items;
using Items.Core;
using Items.InventoryImpl;
using NPC.Behaviour;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : BaseEntityBehaviour
    {
        public static PlayerEntity CurrentPlayer { private set; get; }

        [SerializeField] private DirectionalMovementData _directionMovementData;
        [SerializeField] private JumpData _jumpData;
        [SerializeField] private DirectionalCameraPair _cameras;
        [field: SerializeField] public PlayerStats PlayerStats { private set; get; }

        private Jumper _jumper;

        public readonly Inventory Inventory = new(23);

        public readonly EquipmentInventory ArmorInventory = new EquipmentInventory(new[]
        {
            EquipmentType.Helmet,
            EquipmentType.Shield,
            EquipmentType.Weapon,
            EquipmentType.Armor
        });
        
        
        public override void Awake()
        {
            CurrentPlayer = this;
            
            base.Awake();
            DirectionalMover = new VelocityMover(Rigidbody, _directionMovementData);
            _jumper = new Jumper(Rigidbody, _jumpData);
        }

        private void Update()
        {
            if (_jumper.IsJumping)
                _jumper.UpdateJump();

            UpdateAnimations();
            UpdateCameras();
            PlayerStats.OnUpdate();
        }


        protected override void UpdateAnimations()
        {
            base.UpdateAnimations();
            Animator.SetAnimationState(AnimationType.Jump, _jumper.IsJumping);
        }

        public override void MoveVertically(float verticalDirection)
        {
            if (_jumper.IsJumping)
                return;

            base.MoveVertically(verticalDirection);
        }
        

        public void StartAttack()
        {
            if (!(Animator.SetAnimationState(AnimationType.Attack, true)))
                return;

            Animator.ActionRequested += Attack;
            Animator.AnimationEnded += EndAttack;
        }

        public void Jump() => _jumper.Jump();

        private void Attack()
        {
            Debug.Log("Attack");
        }

        private void EndAttack()
        {
            Animator.ActionRequested -= Attack;
            Animator.AnimationEnded -= EndAttack;
            Animator.SetAnimationState(AnimationType.Attack, false);
        }

        private void UpdateCameras()
        {
            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = cameraPair.Key == DirectionalMover.Direction;
        }
    }
}