using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bugPrefab;
    [SerializeField] private Transform bugParent; // For hierarchy organization
    [SerializeField] private int baseSpawnDistance, startSpawnAmount;
    [SerializeField] private Sprite[] bugSprites;
    private Queue<Bug> bugs;
    private int latestSpawnDistance; // Record in case multiple bugs are spawned in a single turn

    private void Awake()
    {
        bugs = new Queue<Bug>();
        latestSpawnDistance = 0;
    }

    private void Start()
    {
        int temp = baseSpawnDistance; 

        // Temporary disable baseSpawnDistance to spawn closer to player
        baseSpawnDistance = 0; 
        latestSpawnDistance = 0;
        SpawnBug(startSpawnAmount); 
        baseSpawnDistance = temp; 
    }

    // Decrement the latestSpawnDistance since the last bug stepped forward
    public void OnStepForward()
    {
        latestSpawnDistance--;
    }

    // Return the top bug's direction
    // Assuming that bug is already right next to the player
    public Direction GetCurrentBugDirection()
    {
        if (bugs.Count == 0)
            return Direction.NONE;

        Bug bug = bugs.Peek();
        if (!bug.IsReachableByPlayer())
            return Direction.NONE;

        return bug.direction;
    }

    public Bug GetCurrentBug()
    {
        if (bugs.Count == 0)
            return null;

        return bugs.Peek();
    }

    // Spawn n bugs
    public void SpawnBug(int n)
    {
        for (int i = 0; i < n; i++)
            SpawnBug();
    }

    // Spawn a bug in random direction
    public void SpawnBug()
    {
        Direction randomDirection = (Direction)Random.Range(0, 4);
        SpawnBug(randomDirection);
    }

    private void SpawnBug(Direction direction)
    {
        // Determine spawn location
        Vector3 spawnDirection = (Vector3)GetDirection(direction);
        int spawnDistance = Mathf.Max(baseSpawnDistance, latestSpawnDistance + 1);
        Vector3 spawnPosition = spawnDirection * spawnDistance;

        latestSpawnDistance = spawnDistance;

        // Create Bug
        Bug bug = Instantiate(bugPrefab, spawnPosition, Quaternion.identity, bugParent).GetComponent<Bug>();
        int randomSpriteIndex = (int)Random.Range(0f, bugSprites.Length);
        bug.Initialize(spawnPosition, direction, bugSprites[randomSpriteIndex]);
        bugs.Enqueue(bug);
    }


    // Remove the first bug in queue
    public void OnBugHitPlayer()
    {
        bugs.Dequeue();
    }

    private Vector2 GetDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                return Vector2.up;

            case Direction.DOWN:
                return Vector2.down;

            case Direction.LEFT:
                return Vector2.left;

            case Direction.RIGHT:
                return Vector2.right;

            default:
                return Vector2.zero;
        }
    }
}
