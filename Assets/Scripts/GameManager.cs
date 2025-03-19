using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // Global collectible counters

    //GameManager.instance.trashCollected
    //GameManager.instance.sprayCansCollected
    //GameManager.instance.dirtPilesCollected
    //GameManager.instance.graffCollected

    public int trashCollected = 0;
    public int sprayCansCollected = 0;
    public int dirtPilesCollected = 0;
    public int graffCollected = 0;

    public int wallDecorationsCollected = 0;
    public int groundDecorationCollected = 0;

    void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    void Start()
    {
        // Optionally initialize counters here.
    }

    void Update()
    {
        
    }
} 

//Now, from any of your scripts in Gameplay/Quests you can update or access these variables using GameManager.instance.trashCollected (or the other variables) as needed.
