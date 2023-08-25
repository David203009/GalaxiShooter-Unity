using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float speed = 7;
    public int damage = 50;
    public float timeToDestroy = 10;

    public BulletType bulletType = BulletType.Player;


    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {


        switch (bulletType)
        {
            case BulletType.Player:
                transform.Translate(0, 0, speed * Time.deltaTime);    
                break;
            case BulletType.SniperEnemy:
                transform.Translate(-1 * Vector3.forward * speed * Time.deltaTime);
                break;
            case BulletType.NormalEnemy:
                transform.Translate(-1 * Vector3.forward * speed * Time.deltaTime);
                break;
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && bulletType == BulletType.Player)
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player") && (bulletType == BulletType.NormalEnemy || bulletType == BulletType.SniperEnemy))
        {
            Player p = other.gameObject.GetComponent<Player>();
            p.TakeDamage();
            Destroy(gameObject);
        }

    }
}


public enum BulletType
{
    Player,
    NormalEnemy,
    SniperEnemy
}
