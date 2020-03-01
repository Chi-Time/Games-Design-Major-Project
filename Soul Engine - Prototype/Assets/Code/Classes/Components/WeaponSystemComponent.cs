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

        private bool _HasEquippedWeapon = false;

        private void Awake ()
        {
            if (_CurrentWeapon == null)
                _HasEquippedWeapon = false;
            else
                _HasEquippedWeapon = true;
        }

        /// <summary>Fires the currently equipped weapon (or weapons).</summary>
        public void Fire ()
        {
            if (_UseSimultaneous == false && _HasEquippedWeapon)
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
        /// <param name="id">The ID of the weapon to switch to.</param>
        public void SwitchWeapon (int id)
        {
            foreach (var weapon in _Weapons)
            {
                if (weapon.GetInstanceID () == id)
                {
                    _CurrentWeapon = weapon;
                }
            }
        }

        /// <summary> Switches the currently equipped weapon in the system to the new one specified if it exists. </summary>
        /// <param name="name">The name of the weapon to switch to.</param>
        public void SwitchWeapon (string name)
        {
            foreach (var weapon in _Weapons)
            {
                if (weapon.WeaponName == name)
                {
                    _CurrentWeapon = weapon;
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

        /// <summary>Determines if the system contains a weapon with the given ID.</summary>
        /// <param name="id">The unique instance ID of the weapon component.</param>
        /// <returns>True: If the given ID matches with one in the system.</returns>
        public bool HasWeapon (int id)
        {
            foreach (var weapon in _Weapons)
            {
                if (weapon.GetInstanceID () == id)
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>Determines if the system contains a weapon with the given name.</summary>
        /// <param name="name">The name of the weapon component.</param>
        /// <returns>True: If the given name matches with one in the system.</returns>
        public bool HasWeapon (string name)
        {
            foreach (var weapon in _Weapons)
            {
                if (weapon.WeaponName == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
