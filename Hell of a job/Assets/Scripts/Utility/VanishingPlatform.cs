using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    Vector2 startPos;

    public bool respawns = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.isKinematic = false;
            StartCoroutine(RespawnDelay());



        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bounds" && respawns)
        {
            rb.isKinematic = true;
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = startPos;
        }
    }

    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(5);
        rb.isKinematic = true;
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = startPos;
    }
}
