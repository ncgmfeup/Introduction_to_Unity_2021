using UnityEngine;

public class CollectiblesAudioSource : MonoBehaviour
{
    // Singleton Pattern: https://refactoring.guru/design-patterns/singleton
    #region Singleton Pattern
    public static CollectiblesAudioSource instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    #endregion

    private AudioSource _as;

    [SerializeField] private AudioClip collectibleClip = null;

    private void Start()
    {
        _as = GetComponent<AudioSource>();
    }

    public void PickCollectible()
    {
        _as.PlayOneShot(collectibleClip, 1f);
    }
}
