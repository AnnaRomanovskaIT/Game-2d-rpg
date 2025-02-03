using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public WorldTime worldTime;
   public PlantManager plantManager; 
   private void Awake()
    {
        instance = this;
        worldTime = GetComponent<WorldTime>();
        plantManager = GetComponent<PlantManager>();
    }

}
