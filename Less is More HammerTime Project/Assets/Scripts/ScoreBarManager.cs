using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreBarManager : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] TextMeshProUGUI scoreText;

    public void ScoreUpdate(int amount)
    {
        transform.DOShakePosition(0.5f,6f);
        string placeholderScore = "0000000";
        scoreText.text = placeholderScore.Substring(0, (placeholderScore.Length - (score + amount).ToString().Length) + 1) + (score + amount).ToString();
        score += amount;
    }

}
