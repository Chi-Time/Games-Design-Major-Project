using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Code.Classes.Utilities
{
    [Serializable]
    public class ObjectPool<T> where T : Component
    {
        //TODO: Consider using 1 list rather than splitting between the two as it could cause more perfomance overhead.
        //NOTE: Hashset's are more expensive at run time as they cause heap allocation USE LISTS INSTEAD FOR POOLS
        // The pool objects currently active in-scene.
        private List<T> _ActivePool = null;
        // The pool objects currently inactive in-scene.
        private List<T> _InactivePool = null;
        // The various prefab types to pool.
        private T[] _Prefabs = null;
        // The monobehaviour instance that owns this pool.
        private MonoBehaviour _Owner = null;
        // The pool holder in-scene which contains the objects.
        private Transform _PoolHolder = null;

        /// <summary>Constructs the pool for use.</summary>
        /// <param name="owner">The monobehaviour who owns this instance.</param>
        /// <param name="prefab">The prefab to clone and generate a pool from.</param>
        /// <param name="size">The amount of each clone to have in the pool.</param>
        /// <param name="name">The name of the pool in the scene hierarchy.</param>
        public void Construct (MonoBehaviour owner, T prefab, int size = 1, string name = "Pool")
        {
            _Owner = owner;
            _Prefabs = new T[] { prefab };
            _ActivePool = new List<T> ();
            _InactivePool = new List<T> ();

            CreatePool (size, name);
        }
        
        /// <summary>Constructs the pool for use.</summary>
        /// <param name="owner">The monobehaviour who owns this instance.</param>
        /// <param name="prefab">The prefab to clone and generate a pool from.</param>
        /// <param name="holder">The Transform to use as a parent.</param>
        /// <param name="size">The amount of each clone to have in the pool.</param>
        /// <param name="name">The name of the pool in the scene hierarchy.</param>
        public void Construct (MonoBehaviour owner, T prefab, Transform holder, int size = 1, string name = "Pool")
        {
            _Owner = owner;
            _Prefabs = new T[] { prefab };
            _PoolHolder = holder;
            _ActivePool = new List<T> ();
            _InactivePool = new List<T> ();

            CreatePool (size, name);
        }

        /// <summary>Constructs the pool for use.</summary>
        /// <param name="owner">The monobehaviour who owns this instance.</param>
        /// <param name="prefabs">The prefabs to clone and generate a pool from.</param>
        /// <param name="size">The amount of each clone to have in the pool.</param>
        /// <param name="name">The name of the pool in the scene hierarchy.</param>
        public void Construct (MonoBehaviour owner, T[] prefabs, int size = 1, string name = "Pool")
        {
            _Owner = owner;
            _Prefabs = prefabs;
            _ActivePool = new List<T> ();
            _InactivePool = new List<T> ();

            CreatePool (size, name);
        }
        
        /// <summary>Constructs the pool for use.</summary>
        /// <param name="owner">The monobehaviour who owns this instance.</param>
        /// <param name="prefabs">The prefabs to clone and generate a pool from.</param>
        /// <param name="holder">The Transform to use as a parent.</param>
        /// <param name="size">The amount of each clone to have in the pool.</param>
        /// <param name="name">The name of the pool in the scene hierarchy.</param>
        public void Construct (MonoBehaviour owner, T[] prefabs, Transform holder, int size = 1, string name = "Pool")
        {
            _Owner = owner;
            _Prefabs = prefabs;
            _PoolHolder = holder;
            _ActivePool = new List<T> ();
            _InactivePool = new List<T> ();

            CreatePool (size, name);
        }

        private void CreatePool (int size, string name)
        {
            SetupHolder (name);

            foreach (var prefab in _Prefabs)
            {
                for (int j = 0; j < size; j++)
                {
                    _InactivePool.Add (CreatePoolObject (prefab));
                }
            }
        }

        private void SetupHolder (string name)
        {
            var holder = new GameObject ($"POOL: {name}").transform;

            if (_PoolHolder != null)
            {
                holder.SetParent (_PoolHolder);
            }
            
            _PoolHolder = holder;
        }

        private T CreatePoolObject (T poolPrefab)
        {
            var poolObject = MonoBehaviour.Instantiate (poolPrefab, _PoolHolder, true);
            poolObject.gameObject.SetActive (false);
            poolObject.transform.position = _PoolHolder.position;

            return poolObject;
        }

        /// <summary>Retrieves an object from the pool if one exists.</summary>
        /// <returns>Returns the object if one available or null.</returns>
        public T Get ()
        {
            CullActivePool ();

            if (_InactivePool.Count > 0)
            {
                var poolObject = _InactivePool.FirstOrDefault ();
                _InactivePool.Remove (poolObject);
                _ActivePool.Add (poolObject);
                //BUG: Set transformations BEFORE setting the object as active.
                // IF we don't do this, then we transform the object after it's OnEnable has been called and screw up any and all calculations which are being performed.
                // This is a naff bug and a custom callback will need to be added so that we "get" bulletcomponents from the pool
                // And then proceed to fire a function when we want them to setup for use such as a .Construct () method.
                // This means that they can then do any and all of the calculations that they need to without fear of worrying about things moving.
                //poolObject.gameObject.SetActive (true);

                return poolObject;
            }

            return null;
        }

        private void CullActivePool ()
        {
            for (int i = _ActivePool.Count - 1; i >= 0; i--)
            {
                var poolObject = _ActivePool.ElementAt (i);

                if (poolObject.gameObject.activeSelf == false)
                    Put (poolObject);
            }
        }

        /// <summary>Puts the object back into the pool and resets it for use.</summary>
        /// <param name="poolObject">The object to place back within the pool.</param>
        public void Put (T poolObject)
        {
            poolObject.transform.position = _PoolHolder.position;
            poolObject.gameObject.SetActive (false);

            _ActivePool.Remove (poolObject);
            _InactivePool.Add (poolObject);
        }
    }
}
