using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
    public class ProjectileComponent : BulletComponent, IRequireComponents
    {
        public GameObject GameObject => gameObject;

        public IEnumerable<Type> RequiredComponents ()
        {
            return new Type[]
            {
                typeof (Rigidbody2D),
                typeof (Collider2D),
            };
        }

        protected override void EnteredCollider (Collider2D other)
        {
            Cull ();
            LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
        }

        protected override void ExitedCollider (Collider2D other)
        {
        }

        private void Update ()
        {
            _Rigidbody2D.MovePosition (_Rigidbody2D.position + (Vector2)_Transform.up * (_Speed * Time.deltaTime));
        }
    }
}
