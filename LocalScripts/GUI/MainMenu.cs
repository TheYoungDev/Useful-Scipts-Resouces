using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{


    //private Toggle m_MenuToggle;
    private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    //public bool GamePaused = false;
    public GameObject BaseMenu;
    public GameObject PlayMenu;
    public GameObject BrowseMenu;
    public GameObject OptionMenu;
    public GameObject LobbyMenu;
    public GameObject ReturnButton;
    public GameObject LoadScreen;
    public GameObject EverThing;
    //public GameObject CreditBox;
    //public GameObject GameManger;
    //public GameObject Player;

    void Awake()
    {
        if (BaseMenu != null)
            BaseMenu.SetActive(true);
        //GamePaused = false;
        //m_MenuToggle = GetComponent <Toggle> ();
    }
    public void RestartLevel()
    {
       // GamePaused = false;
        Application.LoadLevel(Application.loadedLevelName);
    }
    public void QuitGame()
    {
        //GamePaused = false;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
    }
    public void ShowOptionsMenu()
    {

        if (BaseMenu != null)
            BaseMenu.SetActive(false);
        BrowseMenu.SetActive(false);
        PlayMenu.SetActive(false);
        OptionMenu.SetActive(true);

    }
    public void CloseOptionsMenu()
    {
        OptionMenu.SetActive(false);
        if (BaseMenu != null)
            BaseMenu.SetActive(true);


    }
    public void ShowLobbyMenu()
    {

        if (BaseMenu != null)
            BaseMenu.SetActive(false);
        BrowseMenu.SetActive(false);
        PlayMenu.SetActive(false);
        OptionMenu.SetActive(false);
        LobbyMenu.SetActive(true);
        if(ReturnButton != null)
           ReturnButton.SetActive(false);

    }
    public void CloseLobbyMenu()
    {
        LobbyMenu.SetActive(false);
        ReturnButton.SetActive(false);
        if (BaseMenu != null)
            BaseMenu.SetActive(true);


    }
    public void ShowPlayMenu()
    {
        if(BaseMenu !=null)
            BaseMenu.SetActive(false);
        if (BrowseMenu != null)
            BrowseMenu.SetActive(false);
        if (OptionMenu != null)
            OptionMenu.SetActive(false);
        PlayMenu.SetActive(true);
        

    }

    public void ClosePlayMenu()
    {
        PlayMenu.SetActive(false);
        if (BaseMenu != null)
            BaseMenu.SetActive(true);
        

    }
    public void ShowLevelMenu()
    {
        if (BaseMenu != null)
            BaseMenu.SetActive(false);
        BrowseMenu.SetActive(true);
        PlayMenu.SetActive(false);


    }
    public void CloseLevelMenu()
    {
        if (BaseMenu != null)
            BaseMenu.SetActive(true);
        BrowseMenu.SetActive(false);
        PlayMenu.SetActive(false);


    }
    public void ResumeGame()
    {
        /*Cursor.visible = false;
        GamePaused = false;
        BaseMenu.SetActive(false);
        OptionMenu.SetActive(false);
        CreditBox.SetActive(false);
        GameManger.GetComponent<CrossHair>().enabled = true;
        Player.GetComponent<playerNormalMovement>().PausedGame = false;*/
    }
    public void PlayGame(int index)
    {
        if (LoadScreen != null)
            LoadScreen.SetActive(true);
        if (BaseMenu != null)
            BaseMenu.SetActive(false);
        if (BrowseMenu != null)
            BrowseMenu.SetActive(false);
        if (PlayMenu != null)
            PlayMenu.SetActive(false);
        SceneManager.LoadScene(index);
    }
    public void LevelEditor(int index)
    {
        LoadScreen.SetActive(true);
        if (BaseMenu != null)
            BaseMenu.SetActive(false);
        BrowseMenu.SetActive(false);
        PlayMenu.SetActive(false);
        SceneManager.LoadScene(index);
    }
    public void MultiPlayerLobby(int index)
    {
        LoadScreen.SetActive(true);
        if (BaseMenu != null)
            BaseMenu.SetActive(false);
        BrowseMenu.SetActive(false);
        PlayMenu.SetActive(false);
        SceneManager.LoadScene(index);
    }
    void OnLevelWasLoaded(int level)
    {
        if (EverThing != null)
            EverThing.SetActive(false);//Do Something
    }

}



