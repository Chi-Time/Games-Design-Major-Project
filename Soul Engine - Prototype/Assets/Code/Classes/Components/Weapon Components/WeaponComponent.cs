using System;
using System.Collections.Generic;
using Assets.Code.Classes.Utilities;
using UnityEngine;

namespace SoulEngine
{
    public abstract class WeaponComponent : MonoBehaviour, IRequireComponents
    {
        //Shader.PropertyToID (_WeaponName);
        //NOT: Animator.HashToString () - https://forum.unity.com/threads/animator-stringtohash-performance-collisions.516016/
        public string WeaponName => _WeaponName;
        public GameObject GameObject => gameObject;

        [Tooltip("The name of this weapon in the game."), SerializeField]
        protected string _WeaponName = "";
        [Tooltip("The number of bullets to preload into memory for this weapon."), SerializeField]
        protected int _BulletPoolSize = 0;
        [Tooltip ("Should the weapon pool it's bullets locally as children?"), SerializeField]
        protected bool _HasLocalPool = false;
        [Tooltip ("Should the weapon pick different bullet types at random?"), SerializeField]
        protected bool _RandomiseBullets = false;
        [Tooltip("The bullet type to spawn from the weapon at each fire."), SerializeField]
        protected Transform[] _BulletPrefabs = null;

        protected Transform _Transform = null;
        /// <summary>The pool of bullets for this weapon.</summary>
        protected ObjectPool<Transform>[] _BulletPools = null;

        public IEnumerable<Type> RequiredComponents ()
        {
            return new Type[]
            {
                typeof (Collider),
            };
        }

        protected void Awake ()
        {
            _Transform = GetComponent<Transform> ();
            SetupPools ();
        }

        private void SetupPools ()
        {
            _BulletPools = new ObjectPool<Transform>[_BulletPrefabs.Length];

            for (int i = 0; i < _BulletPools.Length; i++)
            {
                var bullet = _BulletPrefabs[i];
                var poolName = $"{_WeaponName} : {bullet.name}";
                _BulletPools[i] = new ObjectPool<Transform> ();
                
                if(_HasLocalPool)
                    _BulletPools[i].Construct (this, bullet, _Transform, _BulletPoolSize, poolName);
                else
                    _BulletPools[i].Construct (this, bullet, _BulletPoolSize, poolName);
            }
        }

        protected virtual void Update ()
        {
            CalculateTimers ();
        }

        /// <summary>Fires the weapon if it's cooldown is ok.</summary>
        public abstract void Fire ();
        protected abstract void Shoot ();
        protected abstract bool CanFire ();
        protected abstract void CalculateTimers ();
    }
}
