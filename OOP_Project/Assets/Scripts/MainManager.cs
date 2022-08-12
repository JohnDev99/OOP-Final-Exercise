using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerTagTxt;
    [SerializeField] TextMeshProUGUI pointsTxt;
    PlayerController player;

    [SerializeField]Image lifeBar;

    [SerializeField] GameObject gameOverPanel;
    bool isGameRunning;
    public bool IsGameRunning { get { return isGameRunning; } }


    private void Awake()
    {
        GameManager.instance.LoadData();
        isGameRunning = true;
        player = FindObjectOfType<PlayerController>();
        gameOverPanel = GameObject.FindGameObjectWithTag("gameOverPanel");
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTagTxt.text = GameManager.instance.actualPlayer;
        gameOverPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        pointsTxt.text = $"Points: {player.PlayerPoints.ToString()}";

        LifeBar();
    }

    private void LifeBar()
    {
        lifeBar.fillAmount = player.Life / 100;

        if(lifeBar.fillAmount <= 0)
        {
            GameOver();
        }
    }

    //Metodo de GameOver
    void GameOver()
    {
        isGameRunning = false;
        player.gameObject.SetActive(false);
        DeleteAll();

        //Ativar Painel
        gameOverPanel.SetActive(true);
    }

    private void NewScore()
    {
        if(player.PlayerPoints > GameManager.instance.bestScore)
        {
            GameManager.instance.bestPlayerName = playerTagTxt.text;
            GameManager.instance.bestScore = player.PlayerPoints;
            GameManager.instance.SaveData();
        }

    }

    public void MenuBtn()
    {
        NewScore();
        GameManager.instance.SaveData();
        SceneManager.LoadScene(0);
    }

    public void RestartBtn()
    {
        NewScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DeleteAll()
    {
        GameObject[] fishsInScene = GameObject.FindGameObjectsWithTag("fish");
        int fishNumber = fishsInScene.Length;
        for(int i = 0; i < fishNumber; i++)
        {
            Destroy(fishsInScene[i]);
        }
    }
}
