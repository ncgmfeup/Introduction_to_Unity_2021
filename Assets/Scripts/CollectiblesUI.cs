using TMPro;
using UnityEngine;

public class CollectiblesUI : MonoBehaviour
{
    // Singleton Pattern: https://refactoring.guru/design-patterns/singleton
    #region Singleton Pattern
    public static CollectiblesUI instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    #endregion

    [SerializeField] private TMP_Text collectiblesText = null;

    private int collectiblesNum = 0;

    public void PickCollectible()
    {
        collectiblesNum++;
        collectiblesText.text = "x " + collectiblesNum.ToString();
    }
}
