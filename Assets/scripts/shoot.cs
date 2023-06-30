using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;

public class shoot : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosSpawnBullet;
    [SerializeField] private Button[] buttons;
    [SerializeField] private BulletU[] _bullets;
    [SerializeField] private Image[] _images;
    [SerializeField] private Image _defaultImage;
    [SerializeField] private GameObject _tutorPanel;
    [SerializeField] private GameObject _tutor;
    private Image filledImage,buttonImage;
    private Camera _CashedCamera;
    private bool canShoot = true, hold;
    private float timer = 0f, tutortimer;
    private int _weaponIndex =0;
    private bool trig;
    private void Start()
    {
        //filledImage = _defaultImage;
        //buttonImage = buttons[0].GetComponent<Image>();
        //buttonImage.color = new Color32(255, 255, 255, 255);
        //hold = true;
        SetDefaultWeapon();
        _CashedCamera = Camera.main;
    }

    public void SetSpawn()
    {
        
        foreach (var item in _images)
        {
            if(item.gameObject != buttonImage.gameObject)
            {

                item.color = new Color32(200, 200, 200, 255);
            }
        }
        timer = 10000f;
      //  filledImage.fillAmount = 0;
        buttonImage.color = new Color32(255, 255, 255, 255);

    }

    public void CanShootAtButton(bool flag)
    {
        canShoot = flag;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (canShoot)
           if (Input.GetMouseButton(0) && hold)
              ShootWep(_weaponIndex);
           else if (Input.GetMouseButtonDown(0) && !hold)
              ShootWep(_weaponIndex);
        if (trig)
        {
            tutortimer += Time.deltaTime;
            if(tutortimer >= 1.5f) 
            {
                
                _tutorPanel.SetActive(false);
                tutortimer = 0;
            }
        }
    }

    private void ShootWep(int weapon = 0)
    {
        BulletU bullet = _bullets[weapon];
            trig = true;

        if (timer > bullet.Stats.GetCD() && filledImage.fillAmount == 0) 
        {
            Ray ray = _CashedCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                for (int i = 0; i < bullet.Stats.GetCount(); i++)
                {
                    Vector3 spawnPos = _cameraPosSpawnBullet.position;

                    BulletU newBullet = Instantiate(bullet, spawnPos + Random.onUnitSphere * 2, Quaternion.identity);
                    newBullet.Shoot(hit.point);
                }
            }
            if (filledImage != null && bullet.Stats.GetCD() >= 1f)
                Fill(filledImage);
            timer = 0f;
        }
    }

    private async void Fill(Image im)
    {
        im.fillAmount = 1f;
        float cd = _bullets[_weaponIndex].GetComponent<BulletU>().Stats.GetCD();
        while (im.fillAmount >0f)
        {
            im.fillAmount -= Time.deltaTime / cd;
            if (PeronsList.Instance.GetCurrentCount() == 0)
            {
                im.fillAmount = 0;
                break;
            }
               
            await UniTask.DelayFrame(1);
        }
    }
    public void SetButton(Image button) => buttonImage = button;
    public void SetHold(bool Type) => hold = Type;
    public void SetWeaponIndex(int indexWeapon) 
    {
        _weaponIndex = indexWeapon;
       
            if(_tutor.activeSelf && _weaponIndex == 3)
            _tutor.SetActive(false);
        
    } 
    public void SetImage(Image sprite) => filledImage = sprite;
    public void SetDefaultWeapon()
    {
        _weaponIndex = 0;
        filledImage = _defaultImage;
        buttonImage = buttons[_weaponIndex].GetComponent<Image>();
        buttonImage.color = new Color32(255, 255, 255, 255);
        hold = true;
        canShoot = true;
        SetSpawn();
    }
}
