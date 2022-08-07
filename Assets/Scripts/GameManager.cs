using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject[] targets;
    private float spawnRate = 1.0f;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    [SerializeField] private TextMeshProUGUI gameOverText;

    public bool isGameActive;

    [SerializeField] private Button restartButton;

    [SerializeField] private GameObject titleScreen;


    public void StartGame(int difficulty) {
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }

    private IEnumerator SpawnTarget() {
        while (isGameActive) {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Length);
            Instantiate(targets[index]);

            UpdateScore(5);//每过一段时间 +5分
        }

    }

    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
    }

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
