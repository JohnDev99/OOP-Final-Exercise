using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UiHandler : MonoBehaviour
{
    [SerializeField] InputField playerInput;


    public void SavePlayerTagBtn()
    {
        GameManager.instance.actualPlayer = playerInput.text;
       

    }

    public void LoadBtn()
    {

    }

    public void StartGameBtn()
    {
        //Booleano isGameStart = true(variavel por criar)

    }

    public void QuitGame()
    {

//Condicional de compilaçao
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
