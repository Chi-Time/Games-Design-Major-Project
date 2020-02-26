using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
    public class InputWeaponComponent : MonoBehaviour, IRequireComponents
    {
        public GameObject GameObject => gameObject;
        
        [Tooltip ("The button to use for firing the weapon.")] [SerializeField]
        private KeyCode _FireButton = KeyCode.Mouse0;

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
            if (Input.GetKey(_FireButton))
            {
                _WeaponSystem.Fire ();
            }
        }
    }
}
