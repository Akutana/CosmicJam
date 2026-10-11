// using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private GameObject asteroid0;
    [SerializeField] private GameObject asteroid1;
    [SerializeField] private GameObject asteroid2;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private float screenBuffer = 0.05f;
    [SerializeField] private float spawnRate;
    [SerializeField] private float asteroidSize;
    private float timer = 0;

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else 
        {
            spawnAsteroid();
            timer = 0;
        }
    }

    void spawnAsteroid()
    {
        Vector3 viewportPosition = Vector3.zero;
        
        switch (Random.Range(0, 4))
        {
            case 0: viewportPosition = new Vector3(-screenBuffer, Random.value, 0f); break;
            case 1: viewportPosition = new Vector3(1f + screenBuffer, Random.value, 0f); break;
            case 2: viewportPosition = new Vector3(Random.value, -screenBuffer, 0f); break;
            case 3: viewportPosition = new Vector3(Random.value, 1f + screenBuffer, 0f); break;
        }
        
        float cameraDepth = Mathf.Abs(cam.transform.position.z);
        Vector3 spawnPosition = cam.ViewportToWorldPoint(
            new Vector3(viewportPosition.x, viewportPosition.y, cameraDepth));
        spawnPosition.z = 0f;
        
        GameObject[] asteroids = { asteroid0, asteroid1, asteroid2 };
        GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];


        
        //change direction at somepoint
        GameObject spawnedAsteroid = Instantiate(asteroid, spawnPosition, Quaternion.identity);
        spawnedAsteroid.GetComponent<asteroidMover>().SetTarget(player.transform);
    }
}
