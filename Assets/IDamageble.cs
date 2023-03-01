using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble 
{
    public float Health { get; set; }

    public bool Invicible { get; set; }

    public bool Target { get; set; }

   public void OnHit(float damage, Vector2 knockBack);

    public void Onhit(float damage);

    public void OnObjectDestroy();
}
