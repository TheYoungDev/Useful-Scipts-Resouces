using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour {


    //private Toggle m_MenuToggle;
    private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    public bool GamePaused = false;
    public bool Escape = false;
    public GameObject BaseMenu;
    public GameObject OptionMenu;
    public GameObject CreditBox;
    public GameObject GameManger;
    public GameObject KeybindMenu;
    public GameObject Player;

    void Awake()
    {
        BaseMenu.SetActive(false);
        GamePaused = false;
        //m_MenuToggle = GetComponent <Toggle> ();
    }
    public void RestartLevel()
    {
        GamePaused = false;
        Application.LoadLevel(Application.loadedLevelName);
    }
    public void QuitGame()
    {
        GamePaused = false;
        SceneManager.LoadScene(0);
        //#if UNITY_EDITOR
       // UnityEditor.EditorApplication.isPlaying = false;
       // #else
		//    Application.Quit();
       // #endif
    }

    public void ShowOptionMenu()
    {

        CreditBox.SetActive(false);
        OptionMenu.SetActive(true);

    }
    public void CloseOptionMenu()
    {
        OptionMenu.SetActive(false);
        CreditBox.SetActive(true);

    }
    public void ShowKeybindMenu()
    {

        CreditBox.SetActive(false);
        OptionMenu.SetActive(false);
        KeybindMenu.SetActive(true);

    }
    public void CloseKeybindMenu()
    {
        OptionMenu.SetActive(false);
        KeybindMenu.SetActive(false);
        CreditBox.SetActive(true);

    }
    public void ShowMainMenu()
    {
        Cursor.visible = true;
        GamePaused = true;
        CreditBox.SetActive(true);
        BaseMenu.SetActive(true);
        GameManger.GetComponent<CrossHair>().enabled = false;
        

    }
    public void ResumeGame()
    {
        Cursor.visible = false;
        GamePaused = false;
        BaseMenu.SetActive(false);
        OptionMenu.SetActive(false);
        CreditBox.SetActive(false);
        GameManger.GetComponent<CrossHair>().enabled = true;
        if (!Escape) {
            Player.GetComponent<playerNormalMovement>().PausedGame = false;
        }
    }
    public void PlayGame()
    {
      //  SceneManager.LoadScene(1);
    }
    public void LevelEditor()
    {
       // SceneManager.LoadScene(2);
    }
    public void Slider_Changed(float sens)
    {
        Player.GetComponent<playerNormalMovement>().mouseSensitivty = Mathf.Round(sens);
    }




#if !MOBILE_INPUT
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (GamePaused)
            {
                Escape = true;
                ResumeGame();
            }
            else
            {
                Escape = false;
                ShowMainMenu();
            }
            
        }
    }
#endif

}


