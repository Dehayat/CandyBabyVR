using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float angrySpeed = 5f;
    [SerializeField]
    private int health = 1;
    [SerializeField]
    private ProjectileType requiredCandyType;
    [SerializeField]
    private GameObject happySound;

    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        transform.LookAt(Vector3.zero);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            if (requiredCandyType == projectile.GetCandyType())
            {
                TakeDamage(1);
            }
            else
            {
                Anger();
            }
            Destroy(projectile.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Waves.instance.GameOver();
        }
    }

    private void Anger()
    {
        if (anim != null)
        {
            anim.SetBool("Angry", true);
        }
        speed = angrySpeed;
    }

    private void TakeDamage(int dmg)
    {
        if (health <= 0) return;
        health -= dmg;
        if (health <= 0)
        {
            speed = 0;
            GetComponentInChildren<Collider>().isTrigger = true;
            gameObject.layer = 11;
            if (happySound != null)
            {
                var sound = Instantiate(happySound, transform.position,Quaternion.identity);
                Destroy(sound, 3);
            }
            if (GetComponent<GhostDie>())
            {
                GetComponent<GhostDie>().Die();
            }
            else if (GetComponent<BabyDie>())
            {
                GetComponent<BabyDie>().Die();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
