
using UnityEngine;
using UnityEngine.InputSystem;
public class TouchManager : MonoBehaviour
{
    [SerializeField] private InputActionReference actionReference;

    private PlayerInput playerInput;
    private InputAction TouchPrees;
    private bool trig;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        TouchPrees = playerInput.actions.FindAction("TouchPress");
    }
    private void Start()
    {
        TouchPrees.started += TouchPrees_performed;
        TouchPrees.performed += TouchPrees_performed;
        TouchPrees.canceled += TouchPrees_performed;
    }
    private void OnEnable()
        
    {
        actionReference.action.Enable();
       
    }

    private void TouchPrees_performed(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.action.name);
        Debug.Log("Tap");
        Debug.Log(obj.action.triggered);
        Debug.Log(obj.interaction);
        Debug
            .Log(obj.action.IsPressed());

    }

    private void OnDisable()
    {
        actionReference.action.Disable();
        //TouchPrees.started -= TouchPrees_performed;
        //TouchPrees.performed -= TouchPrees_performed;
        //TouchPrees.canceled -= TouchPrees_performed;
    }
    private void Update()
    {
        
       


    }
}
