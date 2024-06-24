using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private bool _spawnOnAwake = true;

    [SerializeField, Required]
    private GameObject _prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
