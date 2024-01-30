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
    GameObject ControlFood;

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
        //Eger koymaktan vazgecerse
        if (isGameObjectOneEmpty == false && ControlFood.transform.position != ControllerPositions[0].transform.position)
        {
            addedFood.Remove(ControlFood.gameObject); //koyudugun itemi sil.
            isGameObjectOneEmpty = true; //yer artik bosaldi.
        }
        else if (isGameObjectTwoEmpty == false && ControlFood.transform.position != ControllerPositions[0].transform.position)
        {
            addedFood.Remove(ControlFood.gameObject); //koyudugun itemi sil.
            isGameObjectTwoEmpty = true; //yer artik bosaldi.
        }
    }


    //OnTriggerStay
    //OnTriggerEnter


    private void OnTriggerEnter(Collider other)
    {
        ControlFood = other.gameObject;

        if (other != null && !other.CompareTag("Plane"))  
        {
             
            if (isGameObjectOneEmpty)
            {
                food.isItOkForDragging = false; // suruklemeyi kapat
                other.transform.position = Vector3.Lerp(other.transform.position, ControllerPositions[0].transform.position, Time.deltaTime *  lerpSpeed);
                isGameObjectOneEmpty = false; //Konum 1 doldu.
            }
            else
            {
                food.isItOkForDragging = false; // suruklemeyi kapat
                other.transform.position = Vector3.Lerp(other.transform.position, ControllerPositions[1].transform.position, Time.deltaTime * lerpSpeed);
                isGameObjectTwoEmpty = false;
            }

                //obje sabitlenmeli ki yeri degismesin
                foodRB = other.gameObject.GetComponent<Rigidbody>();
                foodRB.constraints = RigidbodyConstraints.FreezePositionX;
                foodRB.constraints = RigidbodyConstraints.FreezePositionZ;

                //objeyi tutmam lazim bir degerin icinde bekletmem lazim yok etmek icin
                addedFood.Add(other.gameObject); // obje eklendi
                food.isItOkForDragging = true; // suruklemeyi ac


            if (isGameObjectOneEmpty == false && isGameObjectTwoEmpty == false) // IKI ALANDA DOLDU ISE.
            {
                //iki objeyide yok et iki yeride online yap.

                if (CheckTags())
                {
                    foreach (var item in addedFood)
                    {
                        Debug.Log("sa");
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
            if (addedFood[i].tag == firstTag && 1<addedFood.Count)
            {
                return true;
            }
        }

        // Tüm objelerin tag'leri aynıysa true döndür

        return false;
    }

    void ClearList()
    {
        addedFood.Clear();
        Debug.Log("listtemizlendi");
    }

}
