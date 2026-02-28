using System;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.Searcher;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private SceneManagement sceneManager;

    [SerializeField] private Material LightBlue_Transparent;
    [SerializeField] private Material LightBlue;

    private List<Arm_Base> AllArmsList;
    [SerializeField] private List<ArmSocketScript> ArmSockets;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killsText;
   
    private float timeRemaining = 180f;
    public int enemiesKilled;
    [SerializeField] private int ENEMY_KILL_GOAL = 10;
    private bool goalReached = false;
    [SerializeField] private GameObject levelGoal;
    
    public static GameManager instance;

    private void Awake()
    {
        AllArmsList = new List<Arm_Base>();
        ArmSockets = new List<ArmSocketScript>();
        instance = this;

        player = FindAnyObjectByType<PlayerController>();
        
        sceneManager = GetComponent<SceneManagement>();
    }

    private void Start()
    {
        goalReached = false;
        levelGoal.SetActive(false);
    }

    private void Update()
    {
        killsText.text = "Enemies killed: " + enemiesKilled;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = "Time Left: " + minutes.ToString("0") + ":" + seconds.ToString("00");
            
        }

        SceneManagement.enemiesKilled = enemiesKilled;

        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if(enemiesKilled >= ENEMY_KILL_GOAL && goalReached == false)
        {
            levelGoal.SetActive(true);
            killsText.color = Color.green;
            goalReached = true;
        }
        if (player.getPlayerHealth() <= 0)
        {
            sceneManager.loadScene(SceneManagement.SceneNames.GameOver);
        }
    }

    public Material GetMaterial(string materialName)
    {
        switch (materialName)
        {
            case ("LightBlue_Transparent"):
                return LightBlue_Transparent;
            case ("LightBlue"):
                return LightBlue;
            default:
                return null;
        }
    }

    public void CheckEquipStatus()
    {
        bool bothEquipped = true;

        foreach (ArmSocketScript armSocket in ArmSockets)
        {
            if (!armSocket.IsEquipped())
            {
                bothEquipped = false;
            }
        }

        if (bothEquipped)
        {
            //DisableIndicators();
        }
    }

    public void EnableUnequippedIndicators()
    {
        foreach(Arm_Base arm in AllArmsList)
        {
            if(!arm.IsEquipped())
            {
                arm.enableIndicator();
            }
        }
    }

    public void DisableIndicators()
    {
        foreach(Arm_Base arm in AllArmsList)
        {
            arm.disableIndicator();
        }
    }

    public void registerArm(Arm_Base arm)
    {
        AllArmsList.Add(arm);
    }

    public void RegisterSocket(ArmSocketScript socket)
    {
        ArmSockets.Add(socket);
    }


    public PlayerController getPlayer() { return player; }
}
