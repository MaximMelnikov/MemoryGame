using Lean.Touch;
using UnityEngine;

namespace Core.Services.Input
{
    public class InputService : IInputService
    {
        public bool IsEnabled { get; set; }
        public InputService()
        {
            Debug.Log("InputService");
            BindInputs();
        }

        public void BindInputs()
        {
            LeanTouch.OnFingerTap += HandleFingerTap;
        }

        public void UnbindInputs()
        {
            LeanTouch.OnFingerTap -= HandleFingerTap;
        }

        public void DisableInput()
        {
            IsEnabled = false;
        }

        public void EnableInput()
        {
            IsEnabled = true;
        }

        public void HandleFingerTap(LeanFinger finger)
        {
            if (finger.IsOverGui)
            {
                Debug.Log("Finger " + finger.Index + " tapped on ui");
            }
            else
            {
                var interactable = PhysRaycaster(finger);
                interactable?.InputAction(finger);
            }
        }

        private IInputInteractable PhysRaycaster(LeanFinger finger)
        {
            int mask = 1 << LayerMask.NameToLayer("Default");
            
            var physHits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(finger.ScreenPosition), Vector2.zero, int.MaxValue, mask);

            IInputInteractable result = null;

            for (int i = 0; i < physHits.Length; i++)
            {
                if (physHits[i].collider == null) continue;

                var interactable = physHits[i].transform.GetComponent<IInputInteractable>();
                
                if (interactable != null && interactable.IsInputEnabled)
                {
                    result = interactable;
                    break;
                }
            }

            return result;
        }
    }
}