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
    }

    private void Start()
    {
        currentHealth = maxHealth; //Have current health be initiated with maxHealth
        //healthUI.fillAmount = currentHealth / maxHealth;

    }

    private void Update()
    {
        //Set our GUI value to our GUI_ACTIVE boolean
        healthUIParent.gameObject.SetActive(GUI_ACTIVE);

        //Testing to see if AdjustHealth works
        if (Input.GetKey(KeyCode.Backspace)) AdjustHealth(-1, 1);
        if (Input.GetKey(KeyCode.Return)) AdjustHealth(1, 1);

        
    }

    public void Goto_Scene(string scene_name) {
        scene_name = Scene_Name;
        if (scene_name != null) SceneManager.LoadScene(scene_name);
        
    }

    void Die()
    {
        healthUIParent.gameObject.SetActive(GUI_ACTIVE);
        SceneManager.LoadScene("Lose_Screen");
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
            GUI_ACTIVE = false;
            Die();
        }

    }
}
