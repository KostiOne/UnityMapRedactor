using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Для работы с сценами

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;  // Главное меню
    [SerializeField] private GameObject PlayMenu;  // Меню игры
    [SerializeField] private GameObject EditMenu;  // Меню редактора

    private void Start()
    {
        // Убедимся, что начнем с главного меню
        ShowMainMenu();
    }

    // Открыть главное меню
    public void OpenMainMenu()
    {
        ShowMainMenu();
    }

    // Открыть меню редактирования карт
    public void OpenMapEditor()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        EditMenu.SetActive(true); // Показать меню редактора карт
    }

    // Открыть меню игры
    public void OpenPlayMenu()
    {
        MainMenu.SetActive(false);
        EditMenu.SetActive(false);
        PlayMenu.SetActive(true); // Показать меню игры
    }

    // Выйти из игры
    public void ExitTheGame()
    {
        // Если игра в редакторе, то закроем её
        Debug.Log("Exiting the game...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Закрыть игру в редакторе
        #else
        Application.Quit(); // Закрыть игру в сборке
        #endif
    }

    // Показать главное меню и скрыть другие меню
    private void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        EditMenu.SetActive(false); // Скрыть другие меню
    }
}
