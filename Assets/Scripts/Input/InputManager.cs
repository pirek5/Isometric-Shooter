using UnityEngine;

namespace IsoShooter
{
    public class InputManager : MonoBehaviour, ISceneInjectee
    {
        public InputActions InputActions { get; private set; }
    
        public void OnInjected()
        {
            InputActions = new InputActions();
        }
    }
}


