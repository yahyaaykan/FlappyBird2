using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesisatci : MonoBehaviour
{
    // Boruları tutan nesnelerin referanslarını tutmak için bir dizi
    private GameObject[] Holder;

    // Boruların y eksenindeki rastgele pozisyonu için minimum ve maksimum değerler
    public float Min, Max;

    // Borular arasındaki mesafe
    private float distance = 4.5f;

    // En son borunun x pozisyonu
    private float LastPipeX;

    // Oyun başladığında bir kez çağrılır
    void Start()
    {
        // Şu an için bu fonksiyon boş
    }

    // Nesne oluşturulurken (Start'tan önce) çağrılır
    private void Awake()
    {
        // "PipeHolder" etiketiyle işaretlenmiş tüm boru tutucularını bul ve diziye ata
        Holder = GameObject.FindGameObjectsWithTag("PipeHolder");

        // Her bir borunun y eksenindeki pozisyonunu rastgele ayarla
        for (int i = 0; i < Holder.Length; i++)
        {
            Vector3 temp = Holder[i].transform.position;
            temp.y = Random.Range(Min, Max); // Rastgele bir y pozisyonu belirle
            Holder[i].transform.position = temp; // Yeni pozisyonu uygula
        }

        // İlk borunun x pozisyonunu en son boru olarak ata
        LastPipeX = Holder[0].transform.position.x;

        // Diğer borular arasında en sağdaki (x değeri en büyük olan) boruyu bul
        for (int i = 1; i < Holder.Length; i++)
        {
            if (LastPipeX < Holder[i].transform.position.x)
            {
                LastPipeX = Holder[i].transform.position.x;
            }
        }
    }

    // Bir tetikleyici bölgeye girildiğinde çağrılır
    void OnTriggerEnter2D(Collider2D hedef)
    {
        // Eğer çarpışan nesne "PipeHolder" etiketi taşıyorsa
        if (hedef.tag == "PipeHolder")
        {
            // Çarpışan borunun pozisyonunu al
            Vector3 temp = hedef.transform.position;

            // Boruyu en sağdaki borudan bir mesafe uzaklığa taşı
            temp.x = LastPipeX + distance;

            // Borunun y pozisyonunu rastgele bir değerle değiştir
            temp.y = Random.Range(Min, Max);

            // Yeni pozisyonu uygula
            hedef.transform.position = temp;

            // Yeni en sağdaki x pozisyonunu güncelle
            LastPipeX = temp.x;
        }
    }

    // Her karede bir kez çağrılır
    void Update()
    {
        // Şu an için bu fonksiyon boş
    }
}
