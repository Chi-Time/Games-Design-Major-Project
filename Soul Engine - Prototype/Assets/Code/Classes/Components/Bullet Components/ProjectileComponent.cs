using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
    public class ProjectileComponent : BulletComponent, IRequireComponents
    {
        protected override void EnteredCollider (Collider2D other)
        {
            Cull ();
            LevelSignals.OnEntityHit?.Invoke (this, other.gameObject);
        }

        protected override void ExitedCollider (Collider2D other)
        {
        }

        private void FixedUpdate ()
        {
            Vector2 velocity = _Transform.up * ( _Speed * Time.deltaTime );
            _Rigidbody2D.MovePosition (_Rigidbody2D.position + velocity);
        }
    }
}
