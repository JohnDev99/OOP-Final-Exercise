using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    //Nome do meu atual player
    public string actualPlayer;
    public int bestScore;
    //Melhor jogador
    public string bestPlayerName;


    //Criaçao de uma propriedade para as duas variaveis
    //Variavel do playerTag(InputField){get;private set;}
    //Variavel dos meus pontos {get; custom setter;}



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


    [System.Serializable]
    class DataInfo
    {
        public string playerTag;
        public int bestScore;
    }


        public void SaveData()
        {
            DataInfo info = new DataInfo();
            info.playerTag = actualPlayer;
            info.bestScore = bestScore;

            //Cria 
            string json = JsonUtility.ToJson(info);//Cria uma representaçao dos meus dados a ser Guardados
                                                   //Escreve no ficheiro
            File.WriteAllText(Application.persistentDataPath + "/dataInfo.json", json);//Cria ficheiro com todos os meus dados
        }

        public void LoadData()
        {
            string path = Application.persistentDataPath + "/dataInfo.json";//Caminho do meu diretorio
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);//Ler primeiro
                DataInfo myData = JsonUtility.FromJson<DataInfo>(json);//Converte
                                                                       //Designa
                actualPlayer = myData.playerTag;
            }
        }

}




