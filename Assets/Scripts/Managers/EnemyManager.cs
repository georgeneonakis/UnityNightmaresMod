using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.
        public GameObject enemy;                // The enemy prefab to be spawned.
        public float spawnTime = 3f;            // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

        public int[] spawnNumbers;              // Indicates how many of this enemy type to spawn per level.

        // Coroutine for spawning enemies -- called at the start of each level.
        public IEnumerator Spawn (int level)
        {
            // Uses the current level to determine the number of enemies to spawn.
            int spawnsRemaining = spawnNumbers[level - 1];
            // Coroutine will terminate when all enemies have been spawned.
            while (spawnsRemaining > 0)
            {
                // Wait a set amount of time before each spawn.
                yield return new WaitForSeconds(spawnTime);
                // If the player has no health left...
                if (playerHealth.currentHealth <= 0f)
                {
                    // ... exit the function.
                    break;
                }

                // Randomly choose a spawn point.
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                spawnsRemaining--;
            }
        }
    }
}