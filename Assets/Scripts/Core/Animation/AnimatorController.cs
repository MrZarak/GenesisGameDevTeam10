using System;
using UnityEngine;

namespace Core.Animation
{
    public abstract class AnimatorController : MonoBehaviour
    {
        private AnimationType _currentAnimationType;

        public event Action ActionRequested;
        public event Action AnimationEnded;

        public bool SetAnimationState(AnimationType animationType, bool active, Action animationAction = null, Action endAnimationAction = null)
        {
            if(!active)
            {
                if (_currentAnimationType == AnimationType.Idle || _currentAnimationType != animationType)
                    return false;

                ActionRequested = null;
                AnimationEnded = null;
                OnAnimationEnded();
                return false;
            }
                


            if(_currentAnimationType >= animationType)
                return false;
            
            ActionRequested = animationAction;
            AnimationEnded = endAnimationAction;
            SetAnimationState(animationType);

            return true;
        }

        private void SetAnimation(AnimationType animationType)
        {
            _currentAnimationType = animationType;
            SetAnimationState(_currentAnimationType);
        }
        
        protected abstract void SetAnimationState(AnimationType animationType);

        protected void OnActionRequested()
        {
            ActionRequested?.Invoke();
        }

        protected void OnAnimationEnded()
        {
            AnimationEnded?.Invoke();
            SetAnimation(AnimationType.Idle);
        }
    }
}
