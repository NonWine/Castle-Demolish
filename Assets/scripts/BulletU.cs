
using UnityEngine;
using Cysharp.Threading.Tasks;
public class BulletU : MonoBehaviour
{
    [SerializeField] private float _SpeedLerp =3f;
    [SerializeField] private bool _isDestroyBody;
    [SerializeField] private WeaponType _myType;
    [field: SerializeField]  public WeaponStats Stats { get; private set; }

    private Rigidbody myBody;
    private Vector3 _startPos, veloc;
    private bool coll,hittied;
    private void Awake()
    {
        BulletsList.AddWeaponInList(gameObject);
        myBody = GetComponent<Rigidbody>();
        veloc = myBody.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Castle"))
        {
            if (_myType == WeaponType.BigArrow)
            {
                if (!coll)
                    ParticlePool.Instance.PlayRocktHit(transform.position);
                coll = true;

                Explode(new Vector3(Random.Range(-30, 30), Random.Range(10, 20), Random.Range(-30, 30)));
                Invoke(nameof(SetActimeFalseByTime), 3f);

                //ActiveController(false, 3000);
                //Destroy(gameObject, 3f);

            }
        }

        if (other.gameObject.CompareTag("Ground"))
        {
           

            if (_myType == WeaponType.BigArrow)
            {
                coll = true;
                myBody.velocity = Vector3.zero;
                myBody.isKinematic = true;
                Vibrator.CreateShake();
                gameObject.transform.SetParent(other.transform);
                Invoke(nameof(SetActimeFalseByTime), 3f);

                //  ActiveController(false, 3000);
                //   Destroy(gameObject, 3f);
            }
        }

        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Castle") || other.CompareTag("Enemy"))
        {
            Debug.Log(other.name);
            if (_myType == WeaponType.DestroyRock && !coll)
            {
                coll = true;
                Vibrator.CreateShake();
                ParticlePool.Instance.PlayRocktHit(transform.position);
                Explode(new Vector3(Random.Range(-2, 2), Random.Range(5, 10), Random.Range(-2, 2)));
            }
        }

        if (other.gameObject.CompareTag("Person"))
        {
            
            other.gameObject.GetComponent<Person>().TakeDamage(veloc, Stats.GetForce() + 50);
  
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {

        if ((collision.gameObject.CompareTag("Ground") ||  collision.gameObject.CompareTag("Castle") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PartsBullet")) && !coll)
        {
            
            coll = true;

            if (_myType == WeaponType.Rock && !hittied)
            {
                hittied = true;
                ParticlePool.Instance.PlayRocktHit(transform.position);
                Vibrator.CreateShake();
                if(collision.transform.CompareTag("Ground"))
                    gameObject.transform.SetParent(collision.transform);
                Explode(new Vector3(Random.Range(-2, 2), Random.Range(5, 10), Random.Range(-2, 2)));
                Invoke(nameof(SetActimeFalseByTime), 3f);

                //ActiveController(false, 3000);
                //Destroy(gameObject, 3f);
            }
            else if (_myType == WeaponType.Arrow)
            {
                ParticlePool.Instance.PlayArrowHit(transform.position);
                Debug.Log(gameObject.name);
                if (collision.gameObject.CompareTag("Ground"))
                    gameObject.transform.SetParent(collision.transform);
                Hit(collision.rigidbody);
                Invoke(nameof(SetActimeFalseByTime), 0.2f);

                // ActiveController(false, 200);
                //Destroy(gameObject, 0.2f);
            }
        }
     
    }

    public void Shoot(Vector3 target)
    {
        _startPos = transform.position;
        transform.LookAt(target, Vector3.forward);
        myBody.velocity = (target - _startPos).normalized * _SpeedLerp;
    }

    public void Explode(Vector3 force)
    {
        Debug.Log("asdas");
        Collider[] OverLapColliders = Physics.OverlapSphere(transform.position, Stats.GetRadius());
        for (int i = 0; i < OverLapColliders.Length; i++)
        {
            Rigidbody rb = OverLapColliders[i].attachedRigidbody;
            if (rb && OverLapColliders[i].CompareTag("Castle") || OverLapColliders[i].CompareTag("PartsBullet"))
            {
                SupportGravity obj = OverLapColliders[i].GetComponent<SupportGravity>();
                obj.EnablePhysics(force, Stats.GetDamage());
                obj.InvokeSetActive();
                // ActiveController(false, 3000, OverLapColliders[i].gameObject);
              //  Destroy(OverLapColliders[i].gameObject, 3f);
            }
        }
    }

    public void Hit(Rigidbody hitBody)
    {
      
        if (hitBody != null)
        {
            if (hitBody.CompareTag("Castle"))
            {
                Destroy(myBody);
                
            }
            if (hitBody.CompareTag("Person"))
                Invoke(nameof(SetActimeFalseByTime), 0.2f);
                //ActiveController(false, 200);
       //     Destroy(gameObject, 0.2f);

            hitBody.GetComponent<SupportGravity>().EnablePhysics(Vector3.zero,Stats.GetDamage());
        }

       
    }
    
    private void SetActimeFalseByTime() => gameObject.SetActive(false);
}
public enum WeaponType
{
    Arrow = 0,
    Rock = 1,
    BigArrow = 2,
    DestroyRock = 3
}