using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Teste
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(0);
        }
    }

    
}


[System.Serializable]
class DataInfo
{
    string playerTag;
    int bestScore;



    public void SaveData()
    {
        DataInfo info = new DataInfo();
        info.playerTag = playerTag;

        //Incorreto
        string json = JsonUtility.ToJson(Application.persistentDataPath + "/dataInfo.json");
    }
}

