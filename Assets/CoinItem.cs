using UnityEngine;

public class CoinItem : MonoBehaviour
{
    [Header("Pengaturan Animasi")]
    public float spinSpeed = 100f; // Kecepatan berputar

    // Fungsi Update berjalan setiap frame
    void Update()
    {
        // Membuat koin berputar pada sumbu Y (putar samping)
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }

    // Fungsi ini terpanggil otomatis saat ada object lain masuk ke area koin
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang menabrak adalah Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Koin berhasil diambil!");

            // Menghilangkan koin dari layar
            Destroy(gameObject);
        }
    }
}