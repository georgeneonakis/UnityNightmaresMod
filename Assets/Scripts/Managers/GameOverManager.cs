using UnityEngine;

namespace CompleteProject
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.
        public ScoreManager scoreManager;       // Reference to the score manager.

        Animator anim;                          // Reference to the animator component.


        void Awake ()
        {
            // Set up the reference.
            anim = GetComponent <Animator> ();
        }


        void Update ()
        {
            // If the player has run out of health, or beaten the game...
            if(playerHealth.currentHealth <= 0 || scoreManager.level == 6)
            {
                // ... tell the animator the game is over.
                anim.SetTrigger ("GameOver");
            }
        }
    }
}