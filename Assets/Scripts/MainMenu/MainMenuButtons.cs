using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu; 
    [SerializeField] private GameObject PlayMenu; 
    [SerializeField] private GameObject EditMenu;  

    private void Start()
    {
        ShowMainMenu();
    }

    public void OpenMainMenu()
    {
        ShowMainMenu();
    }

    public void OpenMapEditor()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        EditMenu.SetActive(true);
    }


    public void OpenPlayMenu()
    {
        MainMenu.SetActive(false);
        EditMenu.SetActive(false);
        PlayMenu.SetActive(true); 
    }


    public void ExitTheGame()
    {
        Debug.Log("Exiting the game...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #else
        Application.Quit();
        #endif
    }

    private void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        EditMenu.SetActive(false); 
    }
}
