using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float hp = 10;
    public int speed = 10;
    bool canShoot = true;
    float timer = 0;
    public float timeBtwShoot = 0.5f;

    [Header("Power Ups")]
    public float bulletCount = 250;
    
    //daño doble
    public float doubleDamageDuration = 7;
    public bool doubleDamageActive = false;
    public float damage = 30;

    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckIfCanShoot();
        Shoot();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(x * speed, 0f, z * speed);
        anim.SetFloat("Speed", Mathf.Abs(x + z));
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && canShoot && bulletCount > 0)
        {
            anim.SetTrigger("Shoot");
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            canShoot = false;
        }
        
    }

    void CheckIfCanShoot()
    {
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= timeBtwShoot)
            {
                timer = 0;
                canShoot = true;
            }
        }
    }

    public void TakeDamage()
    {
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void ActivePower(PowerType type)
    {
        switch (type)
        {
            case PowerType.Life:
                PowerUpLife();
                break;
            case PowerType.Portection: 
                break;
            case PowerType.DoubleDamage: 
                break;
            case PowerType.Bullets:
                PowerUpBullets();
                break;
        }
    }

    void PowerUpBullets()
    {
        bulletCount += 7;
        if (bulletCount > 250)
        {
            bulletCount = 250;
        }
    }

    void PowerUpLife()
    {
        hp += 1;
        if (hp > 10)
        {
            hp = 10;
        }
    }

    


}
