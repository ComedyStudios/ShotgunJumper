using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Score scoreScript;
    private void Start()
    {
        scoreText.text = $"Score: {scoreScript.score}";
    }
}