using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    // 1. AREA VARIABEL
    public float speed = 12f;
    public float jumpForce = 18f;
    public Transform groundCheck;
    public float checkRadius = 0.3f;
    public LayerMask groundLayer;

    public int totalPoin = 0;
    public int targetPoin = 50;

    public TextMeshProUGUI teksSkorUI;
    public GameObject victoryPanel;
    public GameObject wastedPanel; // VARIABEL BARU UNTUK WASTED PANEL

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private int koinYangAdaDiLevel;
    private int koinYangDiambil = 0;

    
    public AudioClip soundCoin;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 1f;

        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (wastedPanel != null) wastedPanel.SetActive(false);

        // KODE BARU: Otomatis menghitung semua objek yang punya tag "Coin" saat game dimulai
        koinYangAdaDiLevel = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log("Total Koin yang harus diambil: " + koinYangAdaDiLevel);
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (moveInput > 0) transform.eulerAngles = Vector3.zero;
        else if (moveInput < 0) transform.eulerAngles = new Vector3(0, 180, 0);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. LOGIKA AMBIL KOIN
        if (collision.CompareTag("Coin"))
        {
            koinYangDiambil++;
            totalPoin += 10;
            if (teksSkorUI != null) teksSkorUI.text = "Skor: " + totalPoin;

            // Muncul di console buat ngecek
            Debug.Log("Koin diambil! Total: " + koinYangDiambil + " / " + koinYangAdaDiLevel);

            Destroy(collision.gameObject);
        }

        // 2. LOGIKA FINISH (Hanya untuk Player)
        if (collision.CompareTag("Finish"))
        {
            // CEK: Apakah koin sudah sama dengan total koin di level?
            if (koinYangDiambil >= koinYangAdaDiLevel)
            {
                victoryPanel.SetActive(true);
                Time.timeScale = 0f;
                Debug.Log("MENANG! Semua koin terkumpul.");
            }
            else
            {
                // Pesan kalau koin belum cukup
                int sisa = koinYangAdaDiLevel - koinYangDiambil;
                Debug.Log("Gagal Finish! Kamu masih kurang " + sisa + " koin lagi.");
            }
        }

        // 3. LOGIKA JATUH KE JURANG
        if (collision.CompareTag("DeathZone"))
        {
            wastedPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // FUNGSI BARU: JIKA NABRAK MUSUH
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            wastedPanel.SetActive(true); // Munculkan panel Wasted
            Time.timeScale = 0f; // Berhentikan waktu
        }
    }

    // FUNGSI UNTUK TOMBOL RESTART
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToLevel2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 2");
    }
    // FUNGSI UNTUK LANJUT KE LEVEL 3
    public void GoToLevel3()
    {
        Time.timeScale = 1f; // Wajib kembalikan waktu jadi normal (berjalan)
        SceneManager.LoadScene("Level 3"); // Pastikan tulisan "Level 3" ini sama persis dengan nama scene kamu
    }
    // FUNGSI UNTUK KEMBALI KE MENU / SAMPLE SCENE
    public void BackToStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); // Pastikan namanya sama persis di folder Scenes
    }   
}
