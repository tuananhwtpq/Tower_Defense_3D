using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;

    //Header => Dễ nhìn hơn trong Inspector 
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 5f;

    public GameObject bulletPrefab;
    //Điểm bắn
    public Transform firePoint;
    

    private void Start()
    {
        //Gọi hàm UpdateTarget 2 lần / s
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        //Tạo 1 mảng dùng để lưu các gameObject được tìm  bằng tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //Khoảng cách ngắn nhất
        float shortestDistance = Mathf.Infinity;

        //GameObject gần nhất
        GameObject nearestEnemy = null;

        //Duyệt mảng các object được duyệt bằng tag
        foreach (GameObject enemy in enemies)
        {
            //Tính toán khoảng cách giũa tarret và êenemy
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            //Nếu kc < kc ngắn nhất
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //Thỏa mãn enemy và distance <= range
        if (nearestEnemy != null && shortestDistance <= range)
        {
            //Gán target = enemy 
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        //Khoảng cách giữa đối tượng và enemy
        Vector3 dir = target.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            //vd fireRate = 2 => Bắn 2 phát trong 1s
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
