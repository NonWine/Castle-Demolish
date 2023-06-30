using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public static CameraMoving Instance { get; private set; }
    [SerializeField] private GameObject Obj;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private Vector3 endPosBattle;
    [SerializeField] private AnimationCurve curve;
    private Vector3 startPos;
    private Quaternion startRotation;
    private bool onBattle;
    private float time;
    private void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        
        if (!onBattle)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPosBattle, curve.Evaluate(time));
            
        }

        if (Vector3.Distance(endPosBattle, transform.position) < 0.5f)
        {
            onBattle = true;
            time = 0;
        }
           
        if (onBattle)
            transform.RotateAround(Obj.transform.position, new Vector3(0, 1, 0), RotationSpeed * Time.deltaTime);
        
        
    }
    public void ResetPositionCamera()
    {
        Debug.Log(startPos);
       // Debug.Log(time);
        transform.position = startPos;
        transform.rotation = startRotation;
        onBattle = false;
       
    }
    public void SetLevelObjectRotation(GameObject obj)
    {
        Obj = obj;
    }
}
