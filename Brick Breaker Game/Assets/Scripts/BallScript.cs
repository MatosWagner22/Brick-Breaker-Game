﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Force;
    public bool inPlay;
    public Transform paddle;
    public Transform Explosion;
    public GameManager gm;
    public Transform powerUP;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }

        if (!inPlay)
        {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * Force);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bottom"))
        {
            gm.UpdateLives(-1);
            rb.velocity = Vector2.zero;
            inPlay = false;
        }
    } 

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Brick"))
        {
            int randomChance = Random.Range(1, 101);

            if (randomChance < 50)
            {
                Instantiate(powerUP, other.transform.position, other.transform.rotation);
            }

            Transform newExplosion = Instantiate(Explosion, other.transform.position, other.transform.rotation);
            Destroy(newExplosion.gameObject, 2.5f);

            gm.UpdateScore(other.gameObject.GetComponent<BrickScript>().points);
            gm.UpdateNumberOfBricks();

            Destroy(other.gameObject);
        }
    }

}
