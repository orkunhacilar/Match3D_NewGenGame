using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject[] ControllerPositions;
    private bool isGameObjectOneEmpty = true;
    private bool isGameObjectTwoEmpty = true;
    [SerializeField] float lerpSpeed = 5f;
    private BoxCollider boxCollider;
    private List<GameObject> addedFood = new List<GameObject>();
    Rigidbody foodRB;

    Food food;

    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent (out boxCollider)){
            boxCollider = gameObject.AddComponent<BoxCollider>();
            }

        food = gameObject.AddComponent<Food>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void OnTriggerStay(Collider other)
    {

        if (other != null)  
        {
             
            if (isGameObjectOneEmpty)
            {
                food.isItOkForTrigger = false; // suruklemeyi kapat
                other.transform.position = Vector3.Lerp(other.transform.position, ControllerPositions[0].transform.position, Time.deltaTime *  lerpSpeed);

                //obje sabitlenmeli ki yeri degismesin
                foodRB = other.gameObject.GetComponent<Rigidbody>();
                foodRB.constraints = RigidbodyConstraints.FreezePositionX;
                foodRB.constraints = RigidbodyConstraints.FreezePositionZ;

                addedFood.Add(other.gameObject); // obje eklendi
                food.isItOkForTrigger = true; // suruklemeyi ac

                //objeyi tutmam lazim bekletmem lazim yok etmek icin
                isGameObjectOneEmpty = false; //Konum 1 doldu.
            }
            else if(isGameObjectTwoEmpty)
            {
                food.isItOkForTrigger = false; // suruklemeyi kapat
                other.transform.position = Vector3.Lerp(other.transform.position, ControllerPositions[1].transform.position, Time.deltaTime * lerpSpeed);

                //obje sabitlenmeli ki yeri degismesin
                foodRB = other.gameObject.GetComponent<Rigidbody>();
                foodRB.constraints = RigidbodyConstraints.FreezePositionX;
                foodRB.constraints = RigidbodyConstraints.FreezePositionZ;

                food.isItOkForTrigger = true; // suruklemeyi ac
                addedFood.Add(other.gameObject); // obje eklendi
                //objeyi tutmam lazim bekletmem lazim yok etmek icin
                isGameObjectTwoEmpty = false; //Konum 2 doldu.
            }

            if(isGameObjectOneEmpty == false && isGameObjectOneEmpty == false) // IKI ALANDA DOLDU ISE.
            {
                //iki objeyide yok et iki yeride online yap.

                if (CheckTags())
                {
                    foreach (var item in addedFood)
                    {
                      Collider foodCollider =  item.gameObject.GetComponent<Collider>();
                      foodCollider.isTrigger = true; //Asaya dusup sanki yok oluyomus efekti. Match3D'de oldugu gibi
                    }

                    ClearList();
                    isGameObjectOneEmpty = true; //Konum 1 bosaldi.
                    isGameObjectTwoEmpty = true; //Konum 2 bosaldi.
                }

                       
                    
                

                
                

               
            }
            
        }

       
    }

    bool CheckTags()
    {
        string firstTag = addedFood[0].tag; //ilk tagi aldim
        for (int i = 1; i < addedFood.Count; i++)
        {
            // Eğer bir tag farklıysa, false döndür
            if (addedFood[i].tag != firstTag)
            {
                return false;
            }
        }

        // Tüm objelerin tag'leri aynıysa true döndür
        return true;
    }

    void ClearList()
    {
        addedFood.Clear();
        Debug.Log("listtemizlendi");
    }

}
