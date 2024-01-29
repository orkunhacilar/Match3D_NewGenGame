using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool isDragging = false;
    private Rigidbody rb;
    public bool isItOkForDragging = true;


    void Awake()
    {
        // Eğer nesnenin üzerinde Rigidbody bileşeni yoksa ekleyelim.
        if (!TryGetComponent(out rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
            // rb.isKinematic = false; // Nesnenin fiziksel davranışını etkinleştirir.
        }
    }


    void Start()
    {

        
    }   

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Fare tıklandığında objenin üzerinde mi kontrol et.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Eğer tıklanan nesne bu script'i içeriyorsa, sürükleme modunu başlat.
                isDragging = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Fare bırakıldığında sürükleme modunu kapat.
            isDragging = false;
        }

        if (isDragging && isItOkForDragging)
        {
            // Fare pozisyonunu ekran koordinatlarından dünya koordinatlarına dönüştür.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            // Nesnenin yeni konumunu güncelle.
            rb.MovePosition(new Vector3(mousePosition.x, transform.position.y, mousePosition.z));
        }
    }
}
