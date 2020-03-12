using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
    public class StandardWeaponComponent : ProjectileWeaponComponent
    {
        protected override void Shoot ()
        {
            //Consider removing the if statement to prevent branch mispredictions and perform check elsewhere.
            if (_BulletPools.Length > 0)
            {
                var bullet = _BulletPools[0].Get ()?.transform;
            
                if (bullet != null)
                {
                    //BUG: Set transformations BEFORE setting the object as active. Use the new IPoolable interface for all BulletComponents.
                    // IF we don't do this, then we transform the object after it's OnEnable has been called and screw up any and all calculations which are being performed.
                    // This is a naff bug and a custom callback will need to be added so that we "get" bulletcomponents from the pool
                    // And then proceed to fire a function when we want them to setup for use such as a .Construct () method.
                    // This means that they can then do any and all of the calculations that they need to without fear of worrying about things moving.
                    bullet.position = transform.position;
                    //bullet.rotation = Quaternion.Euler (0, 0, _Transform.rotation.eulerAngles.z);
                    //TODO: Store bullet anchor position.
                    bullet.gameObject.SetActive (true);
                    //bullet.Activate ();
                }
            }
        }
    }
}
