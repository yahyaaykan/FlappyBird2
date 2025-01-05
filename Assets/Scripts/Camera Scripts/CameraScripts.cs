using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    // Kameranın x eksenindeki pozisyonunu ayarlamak için bir statik değişken
    public static float setX;

    // Oyun başladığında bir kez çağrılır
    void Start()
    {
        // Şu anda boş bir işlem, başlangıç için herhangi bir şey tanımlanmamış
    }

    // Her karede bir kez çağrılır
    void Update()
    {
        // Eğer BirdScript sınıfının bir örneği mevcutsa
        if (BirdScript.instance != null)
        {
            // Eğer kuş yaşıyorsa
            if (BirdScript.instance.isAlive == true)
            {
                // Kamerayı hareket ettir
                MoveTheCamera();
            }
        }

        // Kamerayı hareket ettiren fonksiyon
        void MoveTheCamera()
        {
            // Kameranın mevcut pozisyonunu bir değişkene ata
            Vector3 temp = transform.position;

            // Kuşun x eksenindeki pozisyonunu al ve kameranın x pozisyonuna ata
            temp.x = BirdScript.instance.GetPositionX();

            // Kameranın yeni pozisyonunu uygula
            transform.position = temp;
        }
    }
}
