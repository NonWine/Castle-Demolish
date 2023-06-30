using System.Collections;
using UnityEngine;

public class sphereVar : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Person"))
        {
            Person person = other.GetComponent<Person>();
            if (person.Alive)
                person.Noticed = true;
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Person"))
            other.GetComponent<Person>().Noticed = false;
    }

}
