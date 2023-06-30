using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float Radius;
    public float _Force;
    void Start()
    {
        Explode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Explode()
    {
        Collider[] OverLapColliders = Physics.OverlapSphere(transform.position, Radius);
        for (int i = 0; i < OverLapColliders.Length; i++)
        {
            Rigidbody RB = OverLapColliders[i].attachedRigidbody;
            if (RB)
            {
                RB.AddExplosionForce(_Force, transform.position, Radius);
            }
        }
    }
}
