using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour {
    private Button button;
    private GameManager gameManager;

    [SerializeField] private int difficulty;

    // Start is called before the first frame update
    private void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficultyButton);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void SetDifficultyButton() {
        Debug.Log(gameObject.name + " 被点击了!");
        gameManager.StartGame(difficulty);
    }
}
