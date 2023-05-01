using UnityEngine;
using Core.Animation;
using UnityEngine.Rendering;
using Core.Movement.Controller;
namespace NPC.Behaviour
{
    public class BaseEntityBehaviour : MonoBehaviour
    {
        [SerializeField] protected AnimatorController Animator;
        [SerializeField] private SortingGroup _sortingGroup;

        protected Rigidbody2D Rigidbody;
        protected DirectionalMover DirectionalMover;

        //Initialize()
        
        //VerticalPosition(...)

        //SetDrawingOrder(...)

        //SetSize(...)

        //MoveHorizontally(...)

        //MoveVertically(...)

        //SetVerticalPosition(...)
    }

}
