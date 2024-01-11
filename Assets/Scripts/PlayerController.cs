using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float gravity;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;
    public static bool isGameOver;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2d.gravityScale = gravity;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameOver)
        {
            rigidbody2d.bodyType = RigidbodyType2D.Dynamic;

            rigidbody2d.velocity = Vector3.up * speed;
        }
        if (isGameOver)
        {
            StartCoroutine(GameOver());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Tile")|| collision.gameObject.CompareTag("Ground"))
        {
            isGameOver = true;
            spriteRenderer.color = Color.red;
        }
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2d.velocity = speed * Vector3.down;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
