using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [Header("Setting UI")]
    public GameObject victoryPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // LOG untuk cek di Console apakah ada benda yang nempel
        Debug.Log("Sesuatu menabrak FinishZone: " + collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            // Cek apakah koin sudah habis
            int sisaKoin = GameObject.FindGameObjectsWithTag("Coin").Length;

            if (sisaKoin <= 0)
            {
                Menang();
            }
            else
            {
                Debug.Log("Koin belum habis! Sisa: " + sisaKoin);
            }
        }
    }

    void Menang()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f; // Menghentikan game
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("PANEL VICTORY MUNCUL!");
        }
    }
}