using UnityEngine;
using Core.Enums;
using Core.Movement.Data;

namespace Core.Movement.Controller
{
    public abstract class DirectionalMover : MonoBehaviour
    {
        protected readonly Rigidbody2D Rigidbody;

        public Direction Direction { get; private set; }

        public abstract bool IsMoving { get; }

        public DirectionalMover(Rigidbody2D rigidbody)
        {
            Direction = Direction.Left;
            Rigidbody = rigidbody;
        }

        public abstract void MoveHorizontally(float direction);

        public abstract void MoveVertically(float verticalMovement);

        public void SetDirection(Direction newDirection)
        {
            if (newDirection == Direction)
                return;

            
            Direction = newDirection;
            var rotated = newDirection == Direction.Right;
            var current = Rigidbody.transform.rotation;
            Rigidbody.transform.rotation = new Quaternion(current.x, rotated ? 180 : 0, current.z, current.w);
        }
    }
}