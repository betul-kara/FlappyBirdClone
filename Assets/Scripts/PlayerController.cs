using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float gravity;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;
    public static bool isGameOver;
    public static bool isScoreTriggered;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreAreaText;
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] TextMeshProUGUI newText;
    int score = 0;
    int currentScore = 0;
    int highscore = 0;


    [SerializeField] GameObject gameOver;
    [SerializeField] AudioSource jumpSound;


    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2d.gravityScale = gravity;
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpSound = GetComponent<AudioSource>();
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        UpdateScoreText();

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameOver)
        {
            jumpSound.Play();
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Score"))
        {
            score++;
            scoreText.text = score.ToString();
        }
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2d.velocity = speed * Vector3.down;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        gameOver.SetActive(true);
        scoreText.gameObject.SetActive(false);
        UpdateScore(score);
    }
    public void UpdateScore(int score)
    {
        // Skoru güncelle
        currentScore += score;
        scoreAreaText.text = "Score : " + score.ToString();

        // Eğer şu anki skor, kaydedilmiş yüksek skordan büyükse, yüksek skoru güncelle
        if (currentScore > highscore)
        {
            highscore = currentScore;

            // Yüksek skoru PlayerPrefs'e kaydet
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save(); // PlayerPrefs verilerini diskte hemen kaydet
            newText.gameObject.SetActive(true);

            UpdateScoreText(); // UI'da yüksek skoru güncelle
        }
    }
    private void UpdateScoreText()
    {
        highscoreText.text = "Highscore : " + highscore.ToString();
    }
}
