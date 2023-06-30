using UnityEngine;

public class PeronsList : MonoBehaviour 
{
    public static PeronsList Instance;

    public int CurrentCount;

    private void Awake()
    {
        Instance = this;
        CurrentCount = transform.childCount;
        Debug.Log(CurrentCount);
    }
    public void RemoveInList()
    {
       
        CurrentCount--;
        Debug.Log(CurrentCount);
        if (CurrentCount == 0)
        {
            CurrentCount = transform.childCount;
            Invoke(nameof(EndGame), 1f);
        }
            
    }
    public int GetCurrentCount() { return CurrentCount; }
    private void EndGame() => GameManager.Instance.GameWin();


    //public static void AddPersonInList(GameObject person) => Persons.Add(person);
    //public static int GetAmount() { return Persons.Count; }
    //public static GameObject GetPerson(int index) { return Persons[index]; }

}
