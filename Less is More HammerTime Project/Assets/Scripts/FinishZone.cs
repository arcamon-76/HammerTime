using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class FinishZone : MonoBehaviour
{
    [SerializeField] BlackScreenManager blackScreenManager;
    [SerializeField] GameObject ScoreBar;
    [SerializeField] GameObject ScoreText;
    [SerializeField] GameObject text;
    [SerializeField] GameObject ScoreTextTarget;
    [SerializeField] MenuManager menuManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            blackScreenManager.FadeToBlack();
            text.GetComponent<TextMeshProUGUI>().DOColor(Color.white, 1f);
            ScoreText.transform.DOMove(ScoreTextTarget.transform.position, 0.6f);
            StartCoroutine(Restart());
        }
    }
    IEnumerator Restart() 
    {
        yield return new WaitForSeconds(10f);
        menuManager.LaunchLevel(1);
    }
}
