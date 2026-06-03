using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Drag objek Player ke sini
    public float smoothSpeed = 0.125f;
    public Vector3 offset;         // Isi Z dengan -10

    [Header("Batas Kamera")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void LateUpdate()
    {
        if (target != null)
        {
            // Menghitung posisi tujuan berdasarkan offset
            Vector3 desiredPosition = target.position + offset;

            // Membatasi posisi (Inilah yang mencegah kamera naik/keluar background)
            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // MEMASUKKAN HASIL BATAS KE POSISI KAMERA
            // Pastikan pakai clampedX dan clampedY!
            transform.position = new Vector3(clampedX, clampedY, -10f);
        }
    }
}