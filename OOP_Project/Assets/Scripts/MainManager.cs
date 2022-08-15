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
    [SerializeField] TextMeshProUGUI levelTxt;
    PlayerController player;

    [SerializeField]Image lifeBar;

    [SerializeField] GameObject gameOverPanel;
    bool isGameRunning;
    public bool IsGameRunning { get { return isGameRunning; } }

    public int level;
    FishSpawn fishSpawn;


    private void Awake()
    {
        GameManager.instance.LoadData();
        fishSpawn = FindObjectOfType<FishSpawn>();
        isGameRunning = true;
        player = FindObjectOfType<PlayerController>();
        gameOverPanel = GameObject.FindGameObjectWithTag("gameOverPanel");
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTagTxt.text = GameManager.instance.actualPlayer;
        gameOverPanel.SetActive(false);
        
        level = 1;

    }

    // Update is called once per frame
    void Update()
    {
        pointsTxt.text = $"Points: {player.PlayerPoints.ToString()}";
        levelTxt.text = $"Level: {level}";

        LifeBar();
        if (player.PlayerPoints >= level * 100)
        {
            level++;
            fishSpawn.repeatRate++;
        }
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

        //Usar uma courtina
        StartCoroutine(GameOverStat());


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

    void DeleteAll(string fishType)
    {
        GameObject[] fishsInScene = GameObject.FindGameObjectsWithTag(fishType);
        int fishNumber = fishsInScene.Length;
        for(int i = 0; i < fishNumber; i++)
        {
            Destroy(fishsInScene[i]);
        }
    }

    IEnumerator GameOverStat()
    {
        //player.playerAudioSource.PlayOneShot(player.soundEffects[0], player.soundsVolume);
        isGameRunning = false;
        player.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2f);
        DeleteAll("fish");
        DeleteAll("salmon");
        //Ativar Painel
        gameOverPanel.SetActive(true);



    }

}
