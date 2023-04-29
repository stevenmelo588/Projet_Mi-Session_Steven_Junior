using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPickUp : MonoBehaviour
{
    public enum pickUpObject{COIN};
    public pickUpObject currentObject;
    public int pickupQuantity;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            if(currentObject == pickUpObject.COIN)
            PlayerController.playerStats.coins += pickupQuantity;
            Debug.Log(PlayerController.playerStats.coins);
        }
        Destroy(gameObject);
    }
}
