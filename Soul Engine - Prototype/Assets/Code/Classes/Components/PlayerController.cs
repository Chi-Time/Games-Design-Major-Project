using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Code.Classes;
using UnityEngine;

namespace SoulEngine
{
    public class PlayerController : MonoBehaviour, IRequireComponents
    {
        public GameObject GameObject => gameObject;

        private HealthComponent _HealthComponent = null;
        private InputMoveComponent _MoveComponent = null;
        private InputWeaponComponent _WeaponComponent = null;

        public IEnumerable<Type> RequiredComponents ()
        {
            return new Type[]
            {
                typeof (HealthComponent),
                typeof (InputMoveComponent),
                typeof (InputWeaponComponent),
            };
        }

        private void Awake ()
        {
            _HealthComponent = GetComponent<HealthComponent> ();
            _MoveComponent = GetComponent<InputMoveComponent> ();
            _WeaponComponent = GetComponent<InputWeaponComponent> ();
        }

        private void OnEnable ()
        {
            LevelSignals.OnEntityHit += OnEntityHit;
            LevelSignals.OnEntityBubbled += OnEntityBubbled;
        }

        private void OnDisable ()
        {
            LevelSignals.OnEntityHit -= OnEntityHit;
            LevelSignals.OnEntityBubbled -= OnEntityBubbled;
        }

        private void OnEntityHit (IDamage damageComponent, GameObject target)
        {
            if (Equals (this.gameObject, target))
            {
                _HealthComponent.TakeDamage (damageComponent.Damage);
            }
        }

        private void OnEntityBubbled (bool bubble, float bub)
        {
            //TODO: Implement slowing logic.
        }
    }
}
