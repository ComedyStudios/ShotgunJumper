using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    private void Start()
    {
        scoreText.text = $"Score: {Score.Instance.score}";
    }
}