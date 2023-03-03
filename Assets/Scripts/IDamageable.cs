using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IDamageable
    {
        public void InitializeHealth(float health);
        public void OnHit(float damageAmount);
        public void OnHit(float damageAmount, GameObject attackSender);
    }
}
