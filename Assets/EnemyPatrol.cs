using System.ComponentModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // PARAMETER PATROLI
    [SerializeField] float speed = 2f; // Kecepatan gerak, biarkan kecil biar pelan
    [SerializeField] float patrolDistance = 5f; // Total jarak offset dari titik awal (misal: 2.5 ke kanan, 2.5 ke kiri)

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 startPosition;
    private int direction = 1; // 1 = Kanan, -1 = Kiri

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startPosition = transform.position; // Catat posisi awal saat game mulai
    }

    void FixedUpdate()
    {
        MoveDirection();
        CheckPatrolLimits();
    }

    void MoveDirection()
    {
        // Terapkan kecepatan horizontal saja
        rb.linearVelocity = new Vector2(direction * speed, 0);
    }

    void CheckPatrolLimits()
    {
        // Hitung jarak offset dari posisi awal
        float currentOffset = transform.position.x - startPosition.x;

        // Jika mencapai batas kanan atau batas kiri (relatif terhadap titik awal)
        if ((direction == 1 && currentOffset >= patrolDistance / 2) ||
             (direction == -1 && currentOffset <= -patrolDistance / 2))
        {
            FlipDirection();
        }
    }

    void FlipDirection()
    {
        // Balikkan arah
        direction *= -1;

        // Balikkan sprite (flip X) agar hadapnya benar
        sr.flipX = !sr.flipX;
    }
}
