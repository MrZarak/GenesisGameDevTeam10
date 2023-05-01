using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Tools;
using Core.Enums;
using Core.Animation;
using Core.Movement.Data;
using Core.Movement.Controller;
using Items;
using Items.InventoryImpl;
using Drawing;
using UnityEngine.Rendering;

namespace Player
{

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerEntity : MonoBehaviour
{
    public static PlayerEntity CurrentPlayer { private set; get; }
    [SerializeField] private AnimatorController _animator;
    [SerializeField] private SortingGroup _sortingGroup;

    [SerializeField] private DirectionalMovementData _directionMovementData;
    [SerializeField] private JumpData _jumpData;
    [SerializeField] private DirectionalCameraPair _cameras;

    private Rigidbody2D _rigidbody;
    private DirectionalMover _directionalMover;
    private Jumper _jumper;
    public float VerticalPosition => _rigidbody.position.y;
    public event Action<ILevelGraphicElement> VerticalPositionChanged;
    public readonly Inventory Inventory = new (23);
    public readonly Inventory ArmorInventory = new EquipmentInventory(new[]
    {
        EquipmentType.Helmet,
        EquipmentType.LeftHand,
        EquipmentType.RightHand,
        EquipmentType.Armor
    });
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _directionalMover = new DirectionalMover(_rigidbody, _directionMovementData);
        _jumper = new Jumper(_rigidbody, _jumpData);
        CurrentPlayer = this;
    }
    
    private void Update()
    {
        if(_jumper.IsJumping)
            _jumper.UpdateJump();

        UpdateAnimations();
        UpdateCameras();
    }


    private void UpdateAnimations()
    {
        _animator.PlayAnimation(AnimationType.Idle, true);
        _animator.PlayAnimation(AnimationType.Run, _directionalMover.IsMoving);
        _animator.PlayAnimation(AnimationType.Jump, _jumper.IsJumping);
    }

    public void MoveHorizontally(float direction) => _directionalMover.MoveHorizontally(direction);
    public void MoveVertically(float direction) 
    {
        if(_jumper.IsJumping) 
            return;

        _directionalMover.MoveVertically(direction);
        if(direction != 0)
            VerticalPositionChanged?.Invoke(this);
    }

    public void StartAttack()
    {
        if (!(_animator.PlayAnimation(AnimationType.Attack, true)))
            return;

        _animator.ActionRequested += Attack;
        _animator.AnimationEnded += EndAttack;
    }

    public void Jump() => _jumper.Jump();
    
    public void SetDrawingOrder(int order) => _sortingGroup.sortingOrder = order;
    public void SetSize(Vector2 size) => transform.localScale = size;
    public void SetVerticalPosition(float verticalPosition) => _rigidbody.position = new Vector2(_rigidbody.position.x, verticalPosition);
    
    private void Attack()
    {
        Debug.Log("Attack");
    }

    private void EndAttack()
    {
        _animator.ActionRequested -= Attack;
        _animator.AnimationEnded -= EndAttack;
        _animator.PlayAnimation(AnimationType.Attack, false);
    }
    private void UpdateCameras()
    {
        foreach (var cameraPair in _cameras.DirectionalCameras)
            cameraPair.Value.enabled = cameraPair.Key == _directionalMover.Direction;
    }
 }
}

