using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMain : MonoBehaviour
{
    public Player player = null;
    public Player_Spawn playerspawn;
    private void Awake()
    {
    }
    public void Play()
    {
        GameManager.instance.Scene_Name = "Level1-1";
        GameManager.instance.posx = 0f;
        GameManager.instance.posy = 5f;

        playerspawn.gameObject.SetActive(true);
        GameManager.instance.playerPrefab.transform.position = new Vector3(GameManager.instance.posx, GameManager.instance.posy, 0);
        GameManager.instance.Goto_Scene("Level1-1");
        GameManager.instance.currentHealth = GameManager.instance.maxHealth;
        GameManager.instance.healthUI.fillAmount = GameManager.instance.currentHealth / GameManager.instance.maxHealth;
        Instantiate(GameManager.instance.playerPrefab);
    }

    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}