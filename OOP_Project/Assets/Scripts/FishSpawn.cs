using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] fishPrefabs;
    [SerializeField] float spawnMaxY, spawnMinY;

    public float time = 1.5f, repeatRate = 2f;

    MainManager mainManager;
    
    // Start is called before the first frame update

    private void Awake()
    {
        mainManager = FindObjectOfType<MainManager>();
    }
    void Start()
    {
        InvokeRepeating("SpawnFish", time, repeatRate);
    }


    void SpawnFish()
    {
        if (mainManager.IsGameRunning)
        {
            Instantiate(fishPrefabs[RandomGenerate(0, fishPrefabs.Length)], RandomPos(spawnMinY, spawnMaxY), fishPrefabs[RandomGenerate(0, fishPrefabs.Length)].transform.rotation);
        }
    }



    //Aplicaçao de sobrecarga de Metodo
    float RandomGenerate(float min, float max)
    {
        return Random.Range(min, max);
        
    }

    int RandomGenerate(int min, int max)
    {
        return Random.Range(min, max);
    }

    Vector3 RandomPos(float minY, float maxY)
    {
         float y = Random.Range(minY, maxY);
        return new Vector3(9f, y, 87.2f);
    }

}
