using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Ant ant;

    public int score { get; private set; }
    public int lives { get; private set; }
    public int keys { get; private set; }

    public GameObject winText;
    public GameObject gameOverText;

    private bool gameEnded = false;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        // After winning or losing, press any key to restart
        if (gameEnded && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        // Unfreeze game
        Time.timeScale = 1f;

        gameEnded = false;

        SetScore(0);
        SetLives(3);
        keys = 0;

        // Hide WIN text
        if (winText != null)
        {
            winText.SetActive(false);
        }

        // Hide GAME OVER text
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }

        NewRound();
    }

    private void NewRound()
    {
        // Reset all ghosts
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        // Reset Ant
        ant.gameObject.SetActive(true);
        ant.ResetState();
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void AddLife()
    {
        SetLives(lives + 1);
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(score + ghost.points);
    }

    public void AntEaten()
    {
        // Don't kill Ant again if game already ended
        if (gameEnded)
            return;

        ant.gameObject.SetActive(false);

        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(NewRound), 3f);
        }
        else
        {
            GameOver();
        }
    }

    public void AddKey()
    {
        if (gameEnded)
            return;

        keys++;

        if (keys >= 3)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        gameEnded = true;

        if (winText != null)
        {
            winText.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    private void GameOver()
    {
        gameEnded = true;

        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}