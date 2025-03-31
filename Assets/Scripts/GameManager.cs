using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] float slowness = 2;

    bool isGameEnded = false;
    void Update()
    {
        if (!isGameEnded) {
            float score = Time.timeSinceLevelLoad * 10f;
            scoreText.SetText(score.ToString("0"));
        }
    }

    public void EndGame()
    {
        if (!isGameEnded)
        {
            isGameEnded = true;
            StartCoroutine(RestartLevel());
        }
    }

    IEnumerator RestartLevel()
    {
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime /= slowness;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime *= slowness;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
