using System;

namespace Assets.Scripts.UI.Touch
{
    public interface ITouchFacade
    {
        bool LockTouch { get; set; }
        event Action OnFingerDown;
        event Action OnFingerSet;
        event Action OnFingerUp;        
    }
}
