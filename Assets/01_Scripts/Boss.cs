using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float minWaitTime = 1;
    public float maxWaitTime = 2;
    public float minBulletCount = 10;
    public float maxBulletCount = 30;
    public float timeBtwShoot = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootWhitRandAngle()
    {
        float angle = Random.Range(-30f, 30f);
        GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        b.transform.Rotate(0, angle, 0);
    }
}
