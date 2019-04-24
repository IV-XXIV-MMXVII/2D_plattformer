using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//all the code on this page allows for the scene to be recoded so that the checkpoints save the player in a position and spawn you there when you die. 
public class RecordLastScene : MonoBehaviour
{
    public static RecordLastScene records;

    public float lastposx;//position x
    public float lastposy;//position y
    public string lastscene;// previous scene. 

    private void Awake()
    {
        records = this;//records. 
    }

    public void Record(float x, float y, string scene)
    {
        x = GameManager.instance.posx;//position x. 
        y = GameManager.instance.posy;//position y. 
        scene = GameManager.instance.Scene_Name;//record scene. 

        lastposx = x;//last position = x
        lastposy = y;//last position = y
        lastscene = scene;//last scene = scene. 
    }
}
