using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntry : MonoBehaviour
{
    public static DoorEntry instance;

    public float value_x;
    public float value_y;
    public string scene_name;
    public bool allowSpawn = true;

    public Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (collision.gameObject.tag == "Player")//allows player to use portal. 
            {
                GameManager.instance.posx = value_x;//spawns player in position when entering a portal/door. 
                GameManager.instance.posy = value_y;
                Player_Spawn.instance.coordinates = new Vector3(GameManager.instance.posx, GameManager.instance.posy, 0);//spawns player at given coordinates. 
                collision.gameObject.transform.position = Player_Spawn.instance.coordinates;//works with rest of code to grab and spawn player when it goes through door and then drop player in next area on spawn point. 

                if (scene_name != null)
                {
                    GameManager.instance.Scene_Name = scene_name;//putes player in next scene. 
                    GameManager.instance.Goto_Scene(scene_name);//grabs scene and holds for spawn.
                    RecordLastScene.records.Record(value_x, value_y, scene_name);//grabs and records position and scene. 
                }
                else
                    Debug.LogWarning("scene_name is currently null. Scene transition will be ignored.");//helps with finding issues. 
            }
        }
    }
}
