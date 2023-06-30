using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TestingInput : MonoBehaviour

{
    private bool trig;
    private PlayerInput input;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.onActionTriggered += Input_onActionTriggered;
    }

    
    // Start is called before the first frame update
    void Start()
    {
   
    }

   
    private void Input_onActionTriggered(InputAction.CallbackContext obj)
    {
        Debug.Log(obj +"1");
    }

    
    public void Jump(InputAction.CallbackContext obj)
    {

        Debug.Log(obj);
        if (obj.canceled) StartCoroutine(Touch());
        if (obj.started) StopCoroutine(Touch());
    }
    private IEnumerator Touch()
    {
        while (true)
        {
            Debug.Log("holding");
            yield return null;
        }
    }
}
