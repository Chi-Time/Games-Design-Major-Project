using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
    public class InputWeaponComponent : MonoBehaviour, IRequireComponents
    {
        public GameObject GameObject => gameObject;
        
        [Tooltip ("The button to use for firing the weapon on keyboard/mouse.")] [SerializeField]
        private KeyCode _KeyboardKey = KeyCode.Mouse0;
        [Tooltip ("The button to use for firing the weapon on controller/joystick.")] [SerializeField]
        private KeyCode _ControllerKey = KeyCode.Joystick1Button5;

        private WeaponSystemComponent _WeaponSystem = null;

        public IEnumerable<Type> RequiredComponents ()
        {
            return new Type[]
            {
                typeof (WeaponSystemComponent)
            };
        }

        private void Awake ()
        {
            _WeaponSystem = GetComponent<WeaponSystemComponent> ();
        }

        private void Update ()
        {
            if (Input.GetKey(_KeyboardKey) | Input.GetKey (_ControllerKey))
            {
                _WeaponSystem.Fire ();
            }
        }
    }
}
