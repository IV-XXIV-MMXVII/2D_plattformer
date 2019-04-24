using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerPrefab;

    private Player_Spawn player;

    public float maxHealth = 100;
    [HideInInspector] public float currentHealth;

    [Header("Destination")]
    public string Scene_Name;

    [Header("Set Position")]
    public float posx, posy;

    [Header("Game UI")]
    public Image healthUI; //This is our actual healthbar.
    public RawImage healthUIParent; // This is just the background interface
    public bool GUI_ACTIVE =  false;

    // Start is called before the first frame update
    private void Awake()
    {
        //Singleton Implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
        playerPrefab = Instantiate(playerPrefab);
        playerPrefab.SetActive(false);
       
    }

    private void Start()
    {
        currentHealth = maxHealth; //Have current health be initiated with maxHealth
        //healthUI.fillAmount = currentHealth / maxHealth;
         RecordLastScene.records.Record(-9.079158f, 0f, "Level1-1");
    }

    private void Update()
    {
        //Set our GUI value to our GUI_ACTIVE boolean
        healthUIParent.gameObject.SetActive(GUI_ACTIVE);//Will be added for final this is the UI for health. Not in project four but will be in final. 

        //Testing to see if AdjustHealth works
        if (Input.GetKey(KeyCode.Backspace)) AdjustHealth(-1, 1);
        if (Input.GetKey(KeyCode.Return)) AdjustHealth(1, 1);

        if (healthUI.fillAmount == 0)
            Die();

        
    }

    public void Goto_Scene(string scene_name) {
        scene_name = Scene_Name;
        if (scene_name != null) SceneManager.LoadScene(scene_name);//Go to scene.
        
        
    }

    void Die()
    {
        if (playerPrefab.activeInHierarchy == true)
        {
            GUI_ACTIVE = false;
            healthUIParent.gameObject.SetActive(GUI_ACTIVE);//when die takes to death screen. 
            SceneManager.LoadScene("Lose_Screen");
            playerPrefab.SetActive(false);
        }
    }

    public void AdjustHealth(int sign, float value)
    {
        switch(sign)
        {
            //If sign set to negative 1, health bar will go down by a negative value
            case -1:
                if (healthUI.fillAmount != 0)
                    healthUI.fillAmount -= value / maxHealth;
                currentHealth = healthUI.fillAmount;
                break;

            //If sign set to positive 1, health bar will go up by a positive value
            case 1:
                if (healthUI.fillAmount != maxHealth)
                    healthUI.fillAmount += value / maxHealth;
                currentHealth = healthUI.fillAmount;
                break;
        }

        if (healthUI.fillAmount == 0)
        {
            GUI_ACTIVE = false;//this code is the GUI code it allows for player health to show. 
            Die();
        }

    }
}
