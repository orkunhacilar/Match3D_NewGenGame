using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject[] bornPoints;
    [SerializeField] GameObject[] Foods;
    private int RandomNumberForBornPoints;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in Foods)
        {
            for (int i = 0; i <= 3; i++)
            {
                RandomNumberForBornPoints = Random.Range(0, 32);
                Instantiate(item, bornPoints[RandomNumberForBornPoints].transform.position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
