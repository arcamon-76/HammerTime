using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<Transform> objects = new();
    [SerializeField] List<Transform> targets = new();

    public void Start()
    {
        MenuAnimation();
    }

    void MenuAnimation()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].DOMove(targets[i].position, UnityEngine.Random.Range(1f, 2f));
        }
    }

    public void StartButton()
    {
        LaunchLevel(1);
    }

    public void LaunchLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

}
