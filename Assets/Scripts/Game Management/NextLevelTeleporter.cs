using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTeleporter : Interactable
{
    [Tooltip("The integer representing the target level to go to.")]
    public int destinationLevel;
    [Tooltip("If true, then the destination is current level number plus 1.")]
    public bool nextLevel;
    
    public override void Interact()
    {
        if (nextLevel)
        {
            if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(destinationLevel);
        }
    }
}
