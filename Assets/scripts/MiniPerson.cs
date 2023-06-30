using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPerson : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float Power;
    
    void Start()
    {

  //      gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1,1), Random.Range(-1, 1),Random.Range(-1, 1)) * Power);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("stick");
    }
}
