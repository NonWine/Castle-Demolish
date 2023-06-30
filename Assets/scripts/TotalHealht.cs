
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class TotalHealht : MonoBehaviour
{
    [SerializeField] private Image _FilledImage, WhiteFill;
    [SerializeField] private float SlowLerp;
    private static event System.Action<float> OnHealth;
    private float _AmountPart = 0f;
    private  float health = 1f;
    private void Awake()
    {
        OnHealth += FillHouse;
    }

    public async void FillHouse(float count)
    {
         float speed = 0;
        _AmountPart = count / 1000f;
        _FilledImage.fillAmount -= _AmountPart;
        health -= _AmountPart;
        if (health <= 0)
        {
            GameManager.Instance.GameLose();
            ResetHp();
        }
        while (WhiteFill.fillAmount != _FilledImage.fillAmount)
        {
            speed += Time.deltaTime;
            WhiteFill.fillAmount = Mathf.Lerp(WhiteFill.fillAmount, _FilledImage.fillAmount, speed / SlowLerp);
            await Task.Yield();
        }
     
        
    }

    public static void AmountDestroyed(float count) { OnHealth?.Invoke(count); }
    public void ResetHp() 
    {
        _FilledImage.fillAmount = 1f;
        WhiteFill.fillAmount = 1f;
        health = 1f;
    } 
}
