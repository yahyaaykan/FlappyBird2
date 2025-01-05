using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    // GamePlayController sınıfının singleton (tekil) örneği
    public static GamePlayController ornek;

    // Skor ve oyun durumları için kullanılan UI metin bileşenleri
    [SerializeField]
    private Text scoreText, endscoreText, bestScoreText, gameOverText;

    // Oyunun başında gösterilen yardım ikonu
    public Image HelpIcon;

    // Oyunu yeniden başlatma ve duraklatma düğmeleri
    [SerializeField]
    private Button restartButton, PauseButton;

    // Oyunu duraklatmak için kullanılan panel
    [SerializeField]
    private GameObject pausePanel;

    // Skor seviyelerine göre kullanılacak madalya ikonlarının dizisi
    [SerializeField]
    private Sprite[] medalIcon;

    // Skora bağlı olarak gösterilen madalya görseli
    [SerializeField]
    private Image medal;

    // Oyunun duraklatılıp duraklatılmadığını takip eden değişken
    public bool pauseActive = false;

    // Oyun başladığında çalıştırılır
    private void Awake()
    {
        // Singleton örneğini oluştur
        MakeInstance();

        // Yardım ikonunu aktif hale getir
        HelpIcon.gameObject.SetActive(true);

        // Oyunun zaman ölçeğini durdur (oyun duraklatılmış gibi)
        Time.timeScale = 0;

        // Yeniden başlatma düğmesinin önceki tıklama olaylarını temizle
        restartButton.onClick.RemoveAllListeners();

        // Yeniden başlatma düğmesine yeni bir tıklama olayı ekle
        restartButton.onClick.AddListener(() => RestartGame());
    }

    // Singleton örneğini oluşturur
    void MakeInstance()
    {
        if (ornek == null)
        {
            ornek = this;
        }
    }

    // Oyunu duraklatır
    public void PauseGame()
    {
        if (BirdScript.instance != null) // Kuş karakteri varsa
        {
            if (BirdScript.instance.isAlive) // Kuş karakteri hala yaşıyorsa
            {
                // Duraklatma panelini aktif hale getir
                pausePanel.SetActive(true);

                // Zaman ölçeğini durdur
                Time.timeScale = 0;

                // Şu anki skoru ve en iyi skoru güncelle
                endscoreText.text = "" + BirdScript.instance.score;
                bestScoreText.text = "" + DataController.ornek.getHighScore();

                // Oyunun duraklatıldığını belirt
                pauseActive = true;
            }
        }
    }

    // Menüye geri dönmek için kullanılan fonksiyon
    public void goToMenuButton()
    {
        SceneManager.LoadScene("Menu"); // Menü sahnesini yükler
    }

    // Oyunu yeniden başlatır
    public void RestartGame()
    {
        Time.timeScale = 0; // Zaman ölçeğini durdur
        pausePanel.SetActive(false); // Duraklatma panelini kapat
    }

    // Oyunu devam ettirir (örneğin, yeniden başlatıldığında)
    public void ResumeGame()
    {
        Time.timeScale = 0; // Zaman ölçeğini durdur
        SceneManager.LoadScene("SampleScene"); // Sahneyi yeniden yükler
        gameOverText.gameObject.SetActive(false); // "Oyun Bitti" metnini gizle
        scoreText.gameObject.SetActive(true); // Skor metnini göster
    }

    // Oyunu başlatır
    public void PlayGame()
    {
        Time.timeScale = 1; // Zaman ölçeğini başlat
        HelpIcon.gameObject.SetActive(false); // Yardım ikonunu gizle
    }

    // Skoru günceller
    public void SetScore(int score)
    {
        scoreText.text = "" + score; // Skor metnini günceller
    }

    // Oyunun sonunda skoru gösterir
    public void SkoruGoster(int score)
    {
        // Duraklatma panelini aktif hale getir
        pausePanel.SetActive(true);

        // Şu anki skoru günceller
        endscoreText.text = "" + BirdScript.instance.score;

        // "Oyun Bitti" metnini gösterir
        gameOverText.gameObject.SetActive(true);

        // Skor metnini gösterir
        scoreText.gameObject.SetActive(true);

        // Eğer skor en iyi skoru geçerse, en iyi skoru günceller
        if (score > DataController.ornek.getHighScore())
        {
            DataController.ornek.setHighScore(score);
        }

        // En iyi skoru günceller
        bestScoreText.text = "" + DataController.ornek.getHighScore();

        // Skora göre uygun madalya ikonunu seçer
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

        // Yeniden başlatma düğmesinin tıklama olayını günceller
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() => ResumeGame());
    }
}
