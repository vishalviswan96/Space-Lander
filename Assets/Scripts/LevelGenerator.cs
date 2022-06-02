using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    /// <summary>
    /// 
    /// This Script is responsible for Spawning the platforms and collectibles. Each Platform are randomly generated at intervals
    /// As the player moves forward the level keeps generating consedring the distance from the player to the last platform
    /// 
    /// </summary>

    private const float PLAYER_DISTANCE_SPAWN_PLATFORM = 30f;

    [SerializeField] private Transform platform1;
    [SerializeField] private Transform coin;
    [SerializeField] private Transform start_Platform_Location;
    [SerializeField] private PlayerController player;

    private Vector3 last_End_Position;

    private void Awake()
    {
        last_End_Position = start_Platform_Location.position;
        SpawnNextPlatform();
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, last_End_Position) < PLAYER_DISTANCE_SPAWN_PLATFORM)
        {
            SpawnNextPlatform();
            
        }
    }

    private void SpawnNextPlatform()
    {
        SpawnPlatform(last_End_Position);
    }

    private void SpawnPlatform(Vector3 spawnPosition)
    {

        Transform levelpos = Instantiate(platform1, new Vector3(spawnPosition.x + Random.Range(8f, 10f), Random.Range(-3f, 3f)), Quaternion.identity);

        Vector2 midPosition = last_End_Position - levelpos.position;

        Instantiate(coin, new Vector3(spawnPosition.x + Random.Range(3f, 7f), midPosition.y + Random.Range(2f, 3f)), Quaternion.identity);
        last_End_Position = levelpos.position;
    }

    
}
