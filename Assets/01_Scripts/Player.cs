using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float hp = 10;
    public float bullet = 250;

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

    [Header("Interfaz")]
    public Text txtHp;
    public Text txtBullet;

    [Header("Animacion")]
    public Animator anim;
    void Start()
    {
        txtBullet.text = "Bullets = " + bullet;
        txtHp.text = "Live = " + hp;
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
        if (Input.GetKey(KeyCode.Space) && canShoot && bulletCount > 0 && bullet > 0)
        {
            bullet--;
            txtBullet.text = "Bullets = " + bullet;
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
        txtHp.text = "Live = " + hp;
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
            txtBullet.text = "Bullets = " + bullet;
            bulletCount = 250;
        }
    }

    void PowerUpLife()
    {
        hp += 3;
        if (hp > 100)
        {
            hp = 100;
        }
        txtHp.text = "Live = " + hp;
    }

    


}
