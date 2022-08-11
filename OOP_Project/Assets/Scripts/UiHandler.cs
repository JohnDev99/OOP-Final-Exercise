using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UiHandler : MonoBehaviour
{
    [SerializeField] InputField playerInput;
    [SerializeField] Text warningInfo;
    bool hasName;
    [SerializeField] TextMeshProUGUI bestScoreTxt;


    private void Awake()
    {
        GameManager.instance.LoadData();
    }
    private void Start()
    {
        hasName = false;
        DisplayBestScore();

    }

    private void DisplayBestScore()
    {
        if (GameManager.instance.bestPlayerName != "")
        {
            bestScoreTxt.gameObject.SetActive(true);
            bestScoreTxt.text = $"BEST SCORE: {GameManager.instance.bestPlayerName} {GameManager.instance.bestScore}pts";
        }
        else
        {
            bestScoreTxt.gameObject.SetActive(false);
        }
    }

    public void SavePlayerTagBtn()
    {
        GameManager.instance.actualPlayer = playerInput.text;
        GameManager.instance.SaveData();
        hasName = true;

       

    }

    public void LoadBtn()
    {
        GameManager.instance.LoadData();
        playerInput.text = GameManager.instance.actualPlayer;
        hasName = true;
    }

    public void StartGameBtn()
    {
        if (!hasName)
        {
            warningInfo.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        



    }

    public void QuitGame()
    {
        GameManager.instance.SaveData();

//Condicional de compilaçao
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
