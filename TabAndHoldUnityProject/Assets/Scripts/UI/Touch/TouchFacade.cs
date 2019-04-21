using Lean.Touch;
using System;

namespace Assets.Scripts.UI.Touch
{
    public class TouchFacade : ITouchFacade
    {
        public bool LockTouch { get; set; }

        public event Action OnFingerDown = () => { };
        public event Action OnFingerSet = () => { };
        public event Action OnFingerUp = () => { };        

        public TouchFacade()
        {
            LeanTouch.OnFingerDown += ProcessOnFingerDown;
            LeanTouch.OnFingerSet += ProcessOnFingerSet;
            LeanTouch.OnFingerUp += ProcessOnFingerUp;            
        }

        public void ProcessOnFingerDown(LeanFinger finger)
        {
            if (LockTouch) return;

            OnFingerDown();
        }

        public void ProcessOnFingerSet(LeanFinger finger)
        {
            if (LockTouch) return;

            OnFingerSet();
        }

        public void ProcessOnFingerUp(LeanFinger finger)
        {
            if (LockTouch) return;

            OnFingerUp();
        }        
    }
}
