using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject[] ControllerPositions;
    private bool isGameObjectOneEmpty = true;
    private bool isGameObjectTwoEmpty = true;
    private bool shouldTrigger = true;
    [SerializeField] float lerpSpeed = 5f;
    private BoxCollider boxCollider;

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

    private void OnTriggerEnter(Collider other)
    {
        if (shouldTrigger)
        {
            // food.isItOkForTrigger = false;
            
            if (isGameObjectOneEmpty)
            {
                other.transform.position = Vector3.Lerp(other.transform.position, ControllerPositions[0].transform.position, Time.deltaTime *  lerpSpeed);
                isGameObjectOneEmpty = false;
            }
            else if(isGameObjectTwoEmpty)
            {
                other.transform.position = Vector3.Lerp(other.transform.position, ControllerPositions[1].transform.position, Time.deltaTime * lerpSpeed);
                isGameObjectTwoEmpty = false;
            }

            if(isGameObjectOneEmpty == false && isGameObjectOneEmpty == false)
            {

                shouldTrigger = false;
               //  food.isItOkForTrigger = true;
            }
            else
            {
                shouldTrigger = true;
            }
        }

       
    }

}
