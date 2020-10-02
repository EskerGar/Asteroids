using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthText;

    private void Start()
    {
        scoreText.text = "Score: 0";
        healthText.text = "Health: " + playerStats.PlayerHealth;
        playerStats.OnScoreChange += ChangeScoreText;
        playerStats.SubscribeOnHealthChange(ChangeHealthText);
    }

    private void OnDestroy()
    {
        playerStats.OnScoreChange -= ChangeScoreText;
        playerStats.UnSubscribeOnHealthChange(ChangeHealthText);
    }

    private void ChangeScoreText(int value) => ChangeText(scoreText, "Score: ", value);

    private void ChangeHealthText(int value) => ChangeText(healthText, "Health: ", value);

    private void ChangeText(Text text, string startText, int value) => text.text = startText + value;
}
