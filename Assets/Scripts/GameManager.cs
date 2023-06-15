using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Image timerImg;
    [SerializeField]
    private float currentTime;
    [SerializeField]
    private float totalTime;
    [SerializeField]
    private bool started;

    [SerializeField]
    private TextMeshProUGUI gameOverTxt;
    [SerializeField]
    private GameObject gameOverPnl;

    [SerializeField]
    private AudioController audioController;


    // Start is called before the first frame update
    void Start()
    {
        started = true;
        gameOverPnl.SetActive(false);
        audioController.Play("Music");
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            currentTime += Time.deltaTime;
            timerImg.fillAmount = currentTime / totalTime;

            if (currentTime >= totalTime)
            {
                started = false;
                // Times up
                GameOver(false);
            }
        }
    }

    public void GameOver(bool isWin)
    {
        // Pause timer
        started = false;

        gameOverTxt.text = (isWin ? "YOU WIN" : "YOU LOSE");
        gameOverPnl.SetActive(true);

        if (isWin)
        {
            audioController.Play("Cheer");
        }
        else
        {
            audioController.Play("GameOver");
        }
    }
}
