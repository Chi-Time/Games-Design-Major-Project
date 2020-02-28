using System;
using UnityEngine;

namespace SoulEngine
{
    public class HealthComponent : MonoBehaviour
    {
        public int Health { get; set; }

        [Tooltip ("The amount of health the current object has.")]
        [SerializeField] private int _Health = 100;
        [Tooltip ("Should the object be destroyed upon dying?")]
        [SerializeField] private bool _ShouldDestroy = false;

        /// <summary>The maximum amount of health this object can have.</summary>
        private int _Max = 0;

        private void Awake ()
        {
            _Max = _Health;
        }

        private void Die ()
        {
            if (_ShouldDestroy)
                Destroy (gameObject);
            else
                gameObject.SetActive (false);

            LevelSignals.OnEntityKilled?.Invoke (gameObject);
        }

        /// <summary>Damages the object by the specified amount.</summary>
        /// <param name="damage">The amount to damage the object by.</param>
        public void TakeDamage (int damage)
        {
            _Health -= damage;

            if (_Health <= 0)
            {
                Die ();
                _Health = 0;
            }
        }

        /// <summary>Heals the object by the given amount.</summary>
        /// <param name="healAmount">The amount to heal the object by.</param>
        public void Heal (int healAmount)
        {
            _Health += healAmount;

            if (_Health >= _Max)
                _Health = _Max;
        }
    }
}
