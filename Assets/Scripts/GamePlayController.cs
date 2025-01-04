using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GamePlayController : MonoBehaviour
{
    public static GamePlayController ornek;
    // Start is called before the first frame update
    [SerializeField]
    private Text scoreText, endscoreText, bestScoreText, gameOverText;
    public Image HelpIcon;
    [SerializeField]
    private Button restartButton, PauseButton;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private Sprite[] medalIcon;
    [SerializeField]
    private Image medal;

    public bool pauseActive=false;
    private void Awake()
    {
        MakeInstance();
        HelpIcon.gameObject.SetActive(true);
        Time.timeScale = 0;
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() => RestartGame());
    }
    void MakeInstance()
    {
        if (ornek == null)
        {
            ornek = this;
        }
    }
    public void PauseGame()
    {
        if (BirdScript.instance != null)
        {
            if (BirdScript.instance.isAlive)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                endscoreText.text = "" + BirdScript.instance.score;
                bestScoreText.text = "" + DataController.ornek.getHighScore();
                pauseActive = true;
              //  restartButton.onClick.RemoveAllListeners();
              //  restartButton.onClick.AddListener(() => ResumeGame());
            }
        }
    }

  
    public void goToMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

 
    public void RestartGame()
    {
        //SceneManager.LoadScene("SampleScene");
        Time.timeScale = 0;
        pausePanel.SetActive(false);
       // gameOverText.gameObject.SetActive(false);
       // scoreText.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("SampleScene");             
         gameOverText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
      
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        HelpIcon.gameObject.SetActive(false);
    }
    public void SetScore(int score)
    {
        scoreText.text = "" + score;
    }
    public void SkoruGoster(int score)
    {
        pausePanel.SetActive(true);
        endscoreText.text = "" + BirdScript.instance.score;

        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);


        if (score > DataController.ornek.getHighScore())
        {
            DataController.ornek.setHighScore(score);
        }
        bestScoreText.text = "" + DataController.ornek.getHighScore();
        if (score <= 10)
        {
            medal.sprite = medalIcon[0];
        }
        else if (score > 10 && score <= 30)
        {
            medal.sprite = medalIcon[1];
        }
        else
        {
            medal.sprite = medalIcon[2];

        }
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() => ResumeGame());
    }
}
