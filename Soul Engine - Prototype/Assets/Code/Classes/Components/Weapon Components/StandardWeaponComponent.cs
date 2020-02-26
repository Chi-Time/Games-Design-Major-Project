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
                    //TODO: Store bullet anchor position.
                    bullet.gameObject.SetActive (true);
                    bullet.position = transform.position;
                    bullet.rotation = Quaternion.Euler (0, 0, _Transform.rotation.eulerAngles.z);
                }
            }
        }
    }
}
