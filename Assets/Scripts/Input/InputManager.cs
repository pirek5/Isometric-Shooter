using UnityEngine;

namespace IsoShooter
{
    public class InputManager : InstancedSystem
    {
        public InputActions InputActions { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            InputActions = new InputActions();
            InputActions.Enable();
        }
    }
}


