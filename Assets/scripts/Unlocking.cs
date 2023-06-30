using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
public class Unlocking : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private float slowLerp;
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private GameObject[] _visualWeapons;
    [SerializeField] private Image[] _images;
    private int currentUnlock, visualUnlcok =1;
    private int allUnlock;
    private float value,speed;
    private void Awake()
    {
        allUnlock = PlayerPrefs.GetInt("allUnlock");
        value = PlayerPrefs.GetFloat("value");
        currentUnlock = PlayerPrefs.GetInt("currentUnlock",1);
        visualUnlcok = PlayerPrefs.GetInt("visualUnlcok",1);
    }
    private void Start()
    {
      
   
        Debug.Log(visualUnlcok);
        _images[visualUnlcok].fillAmount = PlayerPrefs.GetFloat("_images[visualUnlcok].fillAmount");
        if (visualUnlcok < _visualWeapons.Length)
        _visualWeapons[visualUnlcok].SetActive(true);
    }
    public async void Fill(int percent)
    {

         value = (percent / 100f)  + _images[visualUnlcok].fillAmount;
        PlayerPrefs.SetFloat("value", value);
        speed =0;
        Debug.Log(value);
        while(_images[visualUnlcok].fillAmount != value) 
        {
            speed += Time.deltaTime;
           _images[visualUnlcok].fillAmount = Mathf.Lerp(_images[visualUnlcok].fillAmount, value, speed / slowLerp );
            PlayerPrefs.SetFloat("_images[visualUnlcok].fillAmount", _images[visualUnlcok].fillAmount);
            await UniTask.Yield();
        }
        
    }
    public void Unlock()
    {
            value = 0;
       

        _weapons[currentUnlock].SetActive(true);
            _visualWeapons[visualUnlcok].SetActive(false);
            visualUnlcok++;
            currentUnlock++;
        PlayerPrefs.SetFloat("value", value);
        PlayerPrefs.SetInt("visualUnlcok", visualUnlcok);
            PlayerPrefs.SetInt("currentUnlock", currentUnlock);

        if (visualUnlcok >= _visualWeapons.Length)
        {
            allUnlock = 1;
            visualUnlcok = 0;
            currentUnlock = 0;
            PlayerPrefs.SetInt("allUnlock", allUnlock);
            return;
        }
        _visualWeapons[visualUnlcok].SetActive(true);
       
    }
    public int IsAllUnlock()
    {
        return allUnlock;
    }
    public float Value()
    {
        return value;
    }
    //public void addIndex(bool x)
    //{
    //    if (x)
    //    {
    //        visualUnlcok++;
    //        currentUnlock++;
    //    }
    //    else if(x )

    //}
}
