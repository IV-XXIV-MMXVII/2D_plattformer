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
        GameManager.instance.Scene_Name = RecordLastScene.records.lastscene;//Sends player to position on level 1. 
        GameManager.instance.posx = RecordLastScene.records.lastposx;
        GameManager.instance.posy = RecordLastScene.records.lastposy;

        playerspawn.gameObject.SetActive(true);
        GameManager.instance.GUI_ACTIVE = true;
        GameManager.instance.playerPrefab.transform.position = new Vector3(GameManager.instance.posx, GameManager.instance.posy, 0);
        GameManager.instance.Goto_Scene(GameManager.instance.Scene_Name);
        GameManager.instance.currentHealth = GameManager.instance.maxHealth;
        GameManager.instance.healthUI.fillAmount = GameManager.instance.currentHealth / GameManager.instance.maxHealth;
        GameManager.instance.playerPrefab.SetActive(true);
    }//above sends player through portal to new scene. This is the code that allows player to transition from one scene to the next. 

    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene(0);//loads scene. 
    }

    public void Quit()
    {
        Application.Quit();//quit. 
    }
}