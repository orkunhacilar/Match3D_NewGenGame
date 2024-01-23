using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void Update()
    {
        // Sol fare tuşuna basıldığında
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Raycast kullanarak fare pozisyonuna tıklanan objeyi kontrol et
            if (Physics.Raycast(ray, out hit))
            {
                // Tıklanan nesne bu scriptin bağlı olduğu nesne mi kontrol et
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;

                    // Fare pozisyonu ve nesnenin başlangıç pozisyonu arasındaki ofseti hesapla
                    offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }

        // Sol fare tuşu basılı tutulduğunda
        if (isDragging && Input.GetMouseButton(0))
        {
            // Fare pozisyonunu dünya koordinatlarına dönüştürme
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Y ekseni üzerinde takip etmemek için sadece x ekseni pozisyonunu güncelle
            transform.position = new Vector3(mousePosition.x + offset.x, transform.position.y, transform.position.z);
        }

        // Sol fare tuşu bırakıldığında
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
