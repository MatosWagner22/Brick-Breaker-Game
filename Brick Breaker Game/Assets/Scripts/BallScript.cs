using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Force;
    public bool inPlay;
    public Transform paddle;
    public Transform[] Explosion;
    public GameManager gm;
    public Transform[] powerUPS;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
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
            BrickScript brick = other.gameObject.GetComponent<BrickScript>();

            if (brick.hitsToBreak > 1)
            {
                brick.BreakBrick();
            }
            else
            {
                int randomChance1 = Random.Range(1, 101);
                int randomChance2 = Random.Range(1, 101);

                if (randomChance1 < 10)
                {
                    Instantiate(powerUPS[0], other.transform.position, other.transform.rotation);
                }

                if (randomChance2 < 10)
                {
                    Instantiate(powerUPS[1], other.transform.position, other.transform.rotation);
                }

                Transform newExplosion = Instantiate(Explosion[brick.ExplosionColor], other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);

                gm.UpdateScore(brick.points);
                gm.UpdateNumberOfBricks();

                Destroy(other.gameObject);
            }

            audio.Play();
        }
    }

}
