using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_ : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private int score;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        highscoreText.text = LoadHiSocore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;

    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        board.enabled = false;

        StartCoroutine(Fade(gameOver, 1f,1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while(elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
        gameOver.interactable = true;
    }

    public void InCreaseScore(int points)
    {
        SetScore(score + points);
    }

    public void SetScore(int score)
    {
        this.score= score;

        scoreText.text = score.ToString();

        SaveHiScore();
    }

    private void SaveHiScore()
    {
        int hiscore = LoadHiSocore();

        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiSocore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }
}
