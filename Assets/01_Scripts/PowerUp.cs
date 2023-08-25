using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerType powerType;
    public float timeToDestroy = 10;
    void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player p = other.gameObject.GetComponent<Player>();
            p.ActivePower(powerType);
            Destroy(gameObject);
        }
    }
}


public enum PowerType
{
    Life,
    Bullets,
    DoubleDamage,
    Portection,
}
