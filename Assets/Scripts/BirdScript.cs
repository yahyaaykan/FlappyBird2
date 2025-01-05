using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    // Kuş nesnesine global erişim için statik bir örnek
    public static BirdScript instance;

    // Kuşun fiziksel hareketlerini kontrol etmek için Rigidbody2D bileşeni
    [SerializeField]
    private Rigidbody2D MyRigidBody;

    // Kuş animasyonlarını kontrol etmek için Animator bileşeni
    [SerializeField]
    private Animator Anim;

    // Kuşun ileri hareket hızı
    private float Speed = 3f;

    // Kuşun zıplama (uçma) hızı
    private float BounceSpeed = 4f;

    // Kuşun zıplayıp zıplamadığını kontrol eden değişken
    private bool didFlap;

    // Kuşun hayatta olup olmadığını kontrol eder
    public bool isAlive;

    // Ses oynatma için AudioSource bileşeni
    [SerializeField]
    private AudioSource audioSource;

    // Ses klipleri
    [SerializeField]
    private AudioClip flapclip, dieclip, pointclip;

    // Oyuncunun puanı
    public int score;

    // Kuş sahneye eklendiğinde (başlatılmadan önce) bir kez çalışır
    void Awake()
    {
        // BirdScript örneğini oluştur
        if (instance == null)
        {
            instance = this;
        }

        // Kuşun başlangıçta hayatta olduğunu ayarla
        isAlive = true;

        // Kamera ile kuş arasındaki başlangıç mesafesini ayarla
        setCameraX();
    }

    // Oyun başladığında bir kez çağrılır
    void Start()
    {
        // Şu anda boş
    }

    // Fiziksel işlemleri kare başına bir kez günceller
    void FixedUpdate()
    {
        // Eğer kuş hayattaysa
        if (isAlive)
        {
            // Kuşun x ekseninde ileri doğru hareket etmesini sağlar
            Vector3 temp = transform.position;
            temp.x += Speed * Time.deltaTime;
            transform.position = temp;

            // Eğer kuş zıplarsa
            if (didFlap)
            {
                didFlap = false;

                // Kuşun y eksenindeki hızını ayarla (zıplama hızı)
                MyRigidBody.velocity = new Vector2(0, BounceSpeed);

                // Zıplama sesini oynat
                audioSource.PlayOneShot(flapclip);

                // Zıplama animasyonunu tetikle
                Anim.SetTrigger("Flap");
            }

            // Kuşun y eksenindeki hızına bağlı olarak dönüş açısını ayarla
            if (MyRigidBody.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); // Düz bakış
            }
            else
            {
                float angle = Mathf.Lerp(0, -90, -MyRigidBody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle); // Aşağıya dönüş
            }
        }

        // Kuş çok yukarı çıkarsa (örn. y ekseni 4.5'in üzerindeyse)
        if (transform.position.y >= 4.5)
        {
            if (isAlive)
            {
                isAlive = false;

                // Ölüm animasyonunu tetikle
                Anim.SetTrigger("Bluedied");

                // Oyun sonu ekranını göster
                GamePlayController.ornek.SkoruGoster(score);

                // Ölüm sesini oynat
                audioSource.PlayOneShot(dieclip);
            }
        }
    }

    // Kuşun x eksenindeki pozisyonunu döndürür
    public float GetPositionX()
    {
        return transform.position.x;
    }

    // Kamera ile kuş arasındaki başlangıç mesafesini ayarlar
    void setCameraX()
    {
        CameraScripts.setX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    // Kuşun uçma (zıplama) işlemini gerçekleştirir
    public void Uc()
    {
        // Eğer oyun duraklatılmışsa
        if (GamePlayController.ornek.pauseActive == true)
        {
            // Duraklamayı devre dışı bırak ve oyunu devam ettir
            GamePlayController.ornek.pauseActive = false;
            Time.timeScale = 1;
        }

        // Zıplama işlemini tetikle
        didFlap = true;
    }

    // Kuş bir yüzeye çarptığında çalışır
    void OnCollisionEnter2D(Collision2D hedef)
    {
        // Eğer çarpılan yüzey yer veya boru ise
        if (hedef.gameObject.tag == "Groung" || hedef.gameObject.tag == "Pipe")
        {
            if (isAlive)
            {
                isAlive = false;

                // Ölüm animasyonunu tetikle
                Anim.SetTrigger("Bluedied");

                // Oyun sonu ekranını göster
                GamePlayController.ornek.SkoruGoster(score);

                // Ölüm sesini oynat
                audioSource.PlayOneShot(dieclip);
            }
        }
    }

    // Kuş bir tetikleyici bölgeye girdiğinde çalışır
    void OnTriggerEnter2D(Collider2D hedef)
    {
        // Eğer tetiklenen nesne bir boru tutucu ise
        if (hedef.gameObject.tag == "PipeHolder")
        {
            // Puanı artır
            score++;

            // Güncellenen puanı ekranda göster
            GamePlayController.ornek.SetScore(score);

            // Puan sesini oynat
            audioSource.PlayOneShot(pointclip);
        }
    }
}
