using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
    public class WeaponSystemComponent : MonoBehaviour
    {
        /// <summary>Should the component fire all weapons at once?</summary>
        public bool UseSimultaneous
        {
            get => _UseSimultaneous;
            set => _UseSimultaneous = value;
        }

        /// <summary>The currently equipped weapon on this object. </summary>
        public WeaponComponent CurrentWeapon => _CurrentWeapon;

        [Tooltip ("Should the component fire all weapons at once?"), SerializeField]
        private bool _UseSimultaneous = false;
        [Tooltip ("The currently equipped weapon.")] [SerializeField]
        private WeaponComponent _CurrentWeapon = null;
        [Tooltip ("The various weapon types that this system can make use of.")] [SerializeField]
        private List<WeaponComponent> _Weapons = null;

        /// <summary>Fires the currently equipped weapon (or weapons).</summary>
        public void Fire ()
        {
            if (_UseSimultaneous == false)
            {
                _CurrentWeapon.Fire ();
            }
            else
            {
                foreach (var weapon in _Weapons)
                {
                    weapon.Fire ();
                }
            }
        }

        /// <summary> Switches the currently equipped weapon in the system to the new one specified if it exists. </summary>
        /// <param name="component">The new weapon type to switch to.</param>
        public void SwitchWeapon (WeaponComponent component)
        {
            if (component != null)
            {
                foreach (var weapon in _Weapons)
                {
                    if (weapon.GetType () == component.GetType ())
                    {
                        _CurrentWeapon = weapon;
                    }
                }
            }
        }

        /// <summary>Removes a weapon from the inventory if it exists.</summary>
        /// <param name="component">The weapon to remove from the system.</param>
        public void RemoveWeapon (WeaponComponent component)
        {
            if (component != null)
                _Weapons.Remove (component);
        }

        /// <summary>Adds a new weapon to the inventory for use.</summary>
        /// <param name="component">The new weapon to add to the system.</param>
        public void AddWeapon (WeaponComponent component)
        {
            if (component != null)
                _Weapons.Add (component);
        }
    }
}
