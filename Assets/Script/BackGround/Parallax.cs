using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPosX;
    private float length;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        // Simpan posisi awal dan panjang sprite
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        // Hitung jarak kamera
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float distance = cam.transform.position.x * parallaxEffect;

        // Geser posisi layer
        transform.position = new Vector3(startPosX + distance, transform.position.y, transform.position.z);

        // Loop background agar tidak putus ketika kamera bergerak
        if (temp > startPosX + length)
        {
            startPosX += length;
        }
        else if (temp < startPosX - length)
        {
            startPosX -= length;
        }
    }
}
