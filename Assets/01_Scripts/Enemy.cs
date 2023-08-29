using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [Header("Enemy config")]
    public float speed = 10;
    public float timeToDestroy = 10;
    public float hp = 100;

    public EnemyType enemyType = EnemyType.Normal;

    [Header("Bullets")]
    public GameObject enemyBullet;
    public float timer = 0;
    public float timeBtwSpawn = 0.2f;

    [Header("Player")]
    public float inRange = 20.0f;
    Transform target;
    Quaternion directionForward;

    [Header("Powers")]
    public float dropChance = 50;
    public List<PowerUp> powerUpList;

    public Animator anim;
    void Start()
    {
        if (enemyType == EnemyType.Kamikase || enemyType == EnemyType.Sniper)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        directionForward = transform.rotation;
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Normal:
                normalMovement();
                break;
            case EnemyType.NormalShoot:
                normalShoot();
                break;
            case EnemyType.Kamikase:
                kamikase();
                break;
            case EnemyType.Sniper:
                Sniper();
                break;
        }
    }

    void normalMovement()
    {
        transform.Translate(-1 * Vector3.forward * speed * Time.deltaTime);
    }

    void normalShoot()
    {
        normalMovement();
        SpawnBullet();
    }

    void Destroyer()
    {
        int chance = Random.Range(0, 101); // 0 -> 100
        if (chance >= dropChance)
        {
            Instantiate(powerUpList[Random.Range(0, powerUpList.Count)],
                transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    void kamikase()
    {
        float distancia = Vector3.Distance(target.position, transform.position);

        if (distancia < inRange)
        {
            anim.SetBool("Kamikase", true);
            Rotation();
            transform.Translate(-1 * Vector3.forward * 20 * Time.deltaTime);
        }

        normalMovement();
    }

    void Sniper()
    {
        float distancia = Vector3.Distance(target.position, transform.position);
        if (distancia < inRange)
        {
            anim.SetBool("walk", false);
            Rotation();
            SpawnBulletSniper();
        }
        else
        {
            anim.SetBool("walk", true);
            transform.rotation = directionForward;
            transform.Translate(-1 * Vector3.forward * speed * Time.deltaTime);
        }
    }

    void Rotation()
    {
        Vector3 dir = target.position - transform.position;
        float angleY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg - 180;
        transform.rotation = Quaternion.Euler(0, angleY, 0);
    }

    void SpawnBulletSniper()
    {
        timer += Time.deltaTime;
        if (timer >= timeBtwSpawn)
        {
            timer = 0;
            Instantiate(enemyBullet,
                new Vector3(transform.position.x-2, transform.position.y-1, transform.position.z), transform.rotation);
        }
    }

    void SpawnBullet()
    {
        timer += Time.deltaTime;
        if (timer >= timeBtwSpawn)
        {
            if(enemyType == EnemyType.NormalShoot)
            {
                anim.SetTrigger("shoot");
            }

            timer = 0; 
            Instantiate(enemyBullet,
                new Vector3(transform.position.x-3, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }


    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroyer();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.TakeDamage();
            Destroy(gameObject);
        }
    }


}

public enum EnemyType
{
    Normal,
    NormalShoot,
    Kamikase,
    Sniper
}
