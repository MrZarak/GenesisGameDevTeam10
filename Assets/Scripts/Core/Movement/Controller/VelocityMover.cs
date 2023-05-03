using Core.Enums;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class VelocityMover : DirectionalMover
    {
        private Vector2 _directionVector;
        private readonly float _vMovementScale;
        public override bool IsMoving => _directionVector.magnitude > 0;

        public VelocityMover(Rigidbody2D rigidbody, float vMovementScale) : base(rigidbody)
        {
            _vMovementScale = vMovementScale;
        }

        public override void MoveHorizontally(float direction)
        {
            _directionVector = new Vector2(direction, _directionVector.y);

            var horizontalMovement = direction * _vMovementScale;

            Rigidbody.AddForce(new Vector2(horizontalMovement, 0), ForceMode2D.Impulse);

            if (direction == 0)
                return;

            SetDirection(horizontalMovement > 0 ? Direction.Right : Direction.Left);
        }

        public override void MoveVertically(float direction)
        {
            _directionVector = new Vector2(_directionVector.x, direction);

            var verticalMovement = direction;

            Rigidbody.velocity = new Vector2(Rigidbody.velocity.y, verticalMovement);
        }
    }
}