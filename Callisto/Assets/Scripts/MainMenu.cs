using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    public GameObject createGame;
    [SerializeField] public TMP_InputField savename;

    void Start()
    {
        createGame.SetActive(false);
    }
    public void CreateSave()
    {
        SceneManager.LoadScene(1);
        //SerializationManager.Save(savename.text, SaveData.current);
        //SceneManager.LoadScene(1);
        //PlayerBHV.setSave(savename.text);
    }

    public void LoadGame()
    {
        //SerializationManager.Load();
        //SaveData.current = (SaveData)SerializationManager.Load();
        //PlayerLocation playerlocation = SaveData.current.Player;
        //Debug.Log(playerlocation.location.x);
        //Debug.Log(playerlocation.location.y);
        //Debug.Log(playerlocation.location.z);
    }

    public void NewGame()
    {
        createGame.SetActive(true);
    }

    public void Setting()
    {
        SceneManager.LoadScene(2);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
