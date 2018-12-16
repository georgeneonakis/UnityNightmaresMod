using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
    public class ScoreManager : MonoBehaviour
    {
        public EnemyManager bunnyManager;       // EnemyManager script for spawning ZomBunnies.
        public EnemyManager bearManager;        // EnemyManager script for spawning ZomBears.
        public EnemyManager elephantManager;    // EnemyManager script for spawning Hellephants.

        public PlayerHealth playerHealth;       // Reference to the player's health.

        public Animator anim;           // Reference to the HUDCanvas animator.

        public static int score;        // The player's score.
        public int level;               // The current level.

        public Text scoreText;          // Reference to the Score Text.
        public Text levelText;          // Reference to the Level Text. 
        public Text levelUpText;        // Reference to the Level Up Text.
        public Text gameOverText;       // Reference to the Game Over text.


        void Awake ()
        {
            // Reset the score and level.
            score = 0;
            level = 0;

            // Get a reference to the player's health.
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        }


        void Update()
        {
            // Set the displayed text to be the word "Score" followed by the score value.
            scoreText.text = "Score: " + score;
            // Level up when the player finishes the previous wave (or initialize level 1 if they are just starting).
            if (score == 0 && level == 0 || score == 60 && level == 1 || score == 280 && level == 2 || 
                score == 760 && level == 3 || score == 1550 && level == 4 || score == 2850 && level == 5)
            {
                LevelUp();
            }
        }

        // Level Up function; called at the beginning of the game and at the end of each wave.
        void LevelUp()
        {
            // Reset the player to maximum health.
            playerHealth.currentHealth = playerHealth.startingHealth;
            playerHealth.healthSlider.value = playerHealth.currentHealth;
            // Increment the level and update level text.
            level++;
            levelText.text = "Level: " + level;
            // Update Level Up text (and color) based on the upcoming wave.
            // Wave 1
            if (level == 1)
            {
                levelUpText.text = "Survive the Night!" + System.Environment.NewLine + "Stage 1: Light Sleep";
                levelUpText.color = Color.cyan;
            }
            // Wave 2
            else if (level == 2)
            {
                levelUpText.text = "Stage 2: Deep Sleep";
                levelUpText.color = Color.blue;
            }
            // Wave 3
            else if (level == 3)
            {
                levelUpText.text = "Stage 3: Slow-Wave Sleep";
                levelUpText.color = new Color(100.0f/255, 0, 152.0f/255);
            }
            // Wave 4
            else if (level == 4)
            {
                levelUpText.text = "Stage 4: REM Sleep";
                levelUpText.color = new Color(73.0f/255, 0, 21.0f/255);
            }
            // Wave 5 -- Final Wave
            else if (level == 5)
            {
                levelUpText.text = "Stage 5: Nightmare!";
                levelUpText.color = Color.red;
            }
            // Player wins -- finish the game using the Win coroutine.
            else if (level == 6)
            {
                StartCoroutine(Win());
                // Exit this function to prevent unnecessary activity while the game ends.
                return;
            }
            // Trigger the Level Up text animation.
            anim.SetTrigger("LevelUp");
            // Call the Spawn coroutines on each enemy type.
            // Level is passed as an argument -- each wave contains a set amount of each enemy.
            StartCoroutine(bunnyManager.Spawn(level));
            StartCoroutine(bearManager.Spawn(level));
            StartCoroutine(elephantManager.Spawn(level));
        }

        // Win coroutine -- called when the player beats the game.
        IEnumerator Win()
        {
            // Change the Game Over text to victory text (displayed by GameOverManager script).
            gameOverText.text = "You Survived! Waking Up...";
            // Wait a moment, then reload the scene using RestartLevel function.
            yield return new WaitForSeconds(5.0f);
            playerHealth.RestartLevel();
        }
    }
}