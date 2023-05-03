using System.Collections;
using Core.Enums;
using Core.Services.Updater;
using NPC.Behaviour;
using Pathfinding;
using UnityEngine;

namespace NPC.Controller
{
    public class MeleeEntity : Entity
    {
        private readonly Seeker _seeker;
        private readonly MeleeEntityBehaviour _meleeEntityBehaviour;
        private readonly Vector2 _moveDelta;
        private bool _isAttacking;

        private Coroutine _searchCoroutine;
        private Collider2D _target;
        private Vector3 _previousTargetPosition;
        private Vector3 _destination;
        private float _stoppingDistance;
        private Path _currentPath;
        private int _currentWayPoint;

        public MeleeEntity(MeleeEntityBehaviour meleeEntityBehaviour) : base(meleeEntityBehaviour)
        {
            _seeker = meleeEntityBehaviour.GetComponent<Seeker>();
            _meleeEntityBehaviour = meleeEntityBehaviour;
            _meleeEntityBehaviour.AttackSequenceEnded += OnAttackEnded;
            _searchCoroutine = ProjectUpdater.Instance.StartCoroutine(SearchCoroutine());
            ProjectUpdater.Instance.FixedUpdateCalled += OnFixedUpdateCalled;
            //var speedDelta 
            _moveDelta = new Vector2(4, 4 / 2);
        }

        private IEnumerator SearchCoroutine()
        {
            while (!_isAttacking)
            {
                if (!TryGetTarget(out _target))
                {
                    ResetMovement();
                }
                else if (TryGetTarget(out _target) && _target.transform.position != _previousTargetPosition)
                {
                    Vector2 position = _target.transform.position;
                    _previousTargetPosition = position;
                    _stoppingDistance = (_target.bounds.size.x + _meleeEntityBehaviour.Size.x) / 2;
                    var delta = position.x < _meleeEntityBehaviour.transform.position.x ? 1 : -1;
                    _destination = position + new Vector2(_stoppingDistance * delta, 0);
                    _seeker.StartPath(_meleeEntityBehaviour.transform.position, _destination, OnPathCalculated);
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        private void OnPathCalculated(Path path)
        {
            if (path.error)
                return;

            _currentPath = path;
            _currentWayPoint = 0;
        }

        private bool TryGetTarget(out Collider2D target)
        {
            target = Physics2D.OverlapBox(_meleeEntityBehaviour.transform.position, _meleeEntityBehaviour.SearchBox, 0,
                _meleeEntityBehaviour.Targets);

            return target != null;
        }

        private void OnFixedUpdateCalled()
        {
            if (_isAttacking || _target == null || _currentPath == null || CheckIfCanAttack() ||
                _currentWayPoint >= _currentPath.vectorPath.Count)
                return;
            var currentPosition = _meleeEntityBehaviour.transform.position;
            var waypointPosition = _currentPath.vectorPath[_currentWayPoint];
            var waypointDirection = waypointPosition - currentPosition;

            if (Vector2.Distance(waypointPosition, currentPosition) < 0.05f)
            {
                _currentWayPoint++;
                return;
            }

            if (waypointDirection.y != 0)
            {
                waypointDirection.y = waypointDirection.y > 0 ? 1 : -1;
                var newVerticalPosition = currentPosition.y + _moveDelta.y * waypointDirection.y;
                if (waypointDirection.y > 0 && waypointDirection.y < newVerticalPosition ||
                    waypointDirection.y < 0 && waypointDirection.y > newVerticalPosition)
                    newVerticalPosition = waypointDirection.y;

                if (newVerticalPosition != _meleeEntityBehaviour.transform.position.y)
                {
                    _meleeEntityBehaviour.MoveVertically(newVerticalPosition);
                    OnVerticalPositionChanged();
                }
            }

            if (waypointDirection.x == 0)
                return;

            waypointDirection.x = waypointDirection.x > 0 ? 1 : -1;
            var newHorizontalPosition = currentPosition.x + _moveDelta.y * waypointDirection.x;
            if (waypointDirection.x > 0 && waypointDirection.x < newHorizontalPosition ||
                waypointDirection.x < 0 && waypointDirection.x > newHorizontalPosition)
                newHorizontalPosition = waypointDirection.x;

            if (newHorizontalPosition != _meleeEntityBehaviour.transform.position.x)
                _meleeEntityBehaviour.MoveVertically(newHorizontalPosition);
        }

        private bool CheckIfCanAttack()
        {
            var distance = _destination - _meleeEntityBehaviour.transform.position;
            if (Mathf.Abs(distance.x) > 0.2f || Mathf.Abs(distance.y) > 0.2f)
                return false;

            _meleeEntityBehaviour.MoveHorizontally(_destination.x);
            _meleeEntityBehaviour.MoveVertically(_destination.y);
            _meleeEntityBehaviour.SetDirection(_meleeEntityBehaviour.transform.position.x > _target.transform.position.x
                ? Direction.Left
                : Direction.Right);
            _isAttacking = true;
            _meleeEntityBehaviour.StartAttack();
            if (_searchCoroutine != null)
                ProjectUpdater.Instance.StopCoroutine(_searchCoroutine);

            return true;
        }

        private void ResetMovement()
        {
            _target = null;
            _currentPath = null;
            _previousTargetPosition = Vector2.negativeInfinity;
            var position = _meleeEntityBehaviour.transform.position;
            _meleeEntityBehaviour.MoveVertically(position.y);
            _meleeEntityBehaviour.MoveHorizontally(position.x);
        }

        private void OnAttackEnded()
        {
            _isAttacking = false;
            _searchCoroutine = ProjectUpdater.Instance.StartCoroutine(SearchCoroutine());
        }
    }
}