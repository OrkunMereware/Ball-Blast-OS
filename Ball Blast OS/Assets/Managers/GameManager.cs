using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Render Settings")]
    [SerializeField] private int framePerSecond = 30;
    [Header("Game Settings")]
    [SerializeField] public int currentLevel = 0;
    [SerializeField] public int furtherLevelMaxHp;
    [System.NonSerialized] public JSONParser.GameProperties gameProperties;
    [System.NonSerialized] public int totalScore, currentScore;

    [Header("Element References")]
    [SerializeField] public BallGenerator ballGenerator;
    [SerializeField] public Character character;

    [Header("UI References")]
    [SerializeField] public Button playButton;
    [SerializeField] public Button replayButton;
    [SerializeField] public TextMeshProUGUI sceneStateText;
    [SerializeField] public TextMeshProUGUI scoreProgressText;

    [Header("Game States")]
    [System.NonSerialized] public bool PLAYING = false;
    [System.NonSerialized] public int sceneState = 0;
    [System.NonSerialized] private bool startOver = false;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = framePerSecond;
    }

    void Start()
    {
        JSONParser.Read(ref gameProperties, "JSON/level");
        Physics.gravity = Vector3.up * gameProperties.gravity;
        sceneState = 0;
    }

    public void NextLevel()
    {
        currentLevel++;
        character.currentWeapon.SetPropeties((1.0f / (currentLevel * gameProperties.bullet_count_increase)), gameProperties.bullet_damage_increase * currentLevel);
        if (currentLevel <= 5)
        {
            ballGenerator.SpawnBalls(gameProperties.levels[currentLevel - 1].balls);
        }
        else
        {
            ballGenerator.SpawnBallsByHp(furtherLevelMaxHp, Random.Range(0.8f, 1.2f));
        }
    }

    public void CheckLevelCompletion()
    {
        scoreProgressText.SetText(currentScore + " / " + totalScore);
        if(GameManager.instance.currentScore == GameManager.instance.totalScore){
            WonLevel();
        }
    }

    public void Play()
    {
        playButton.gameObject.SetActive(false);
        scoreProgressText.gameObject.SetActive(true);
        PLAYING = true;
        NextLevel();
        scoreProgressText.SetText(currentScore + " / " + totalScore);
    }

    public void Replay()
    {
        if (startOver)
        {
            currentLevel = Mathf.Clamp(currentLevel - 1, 0, int.MaxValue);
            ballGenerator.Reset();
        }
        SwipeManager.instance.SWIPE_PERCENTAGE = 0.5f;
        replayButton.gameObject.SetActive(false);
        sceneStateText.SetText("");
        Play();
    }

    public void WonLevel()
    {
        replayButton.gameObject.SetActive(false);
        PLAYING = false;
        scoreProgressText.SetText(currentScore.ToString());
        sceneStateText.SetText("YOU WON");
        replayButton.gameObject.SetActive(true);
        startOver = false;
    }

    public void GameOver()
    {
        PLAYING = false;
        replayButton.gameObject.SetActive(true);
        sceneStateText.SetText("YOU LOST");
        scoreProgressText.SetText(currentScore.ToString());
        startOver = true;
    }

    /*public void OutputGameState()
    {
        switch (sceneState)
        {
            case 0:
                sceneState = 1;
                break;
            case 1:
                //sceneStateText.SetText("");
                break;
            case 2:
                sceneStateText.SetText("YOU LOST\nREPLAY");
                PLAYING = false;
                break;
            case 3:
                //sceneStateText.SetText("REPLAY");
                break;
        }
    }

    public void ProcessSceneStateAfterClick()
    {
        switch (sceneState)
        {
            case 0:
                sceneStateText.SetText("PLAY");
                break;
            case 1:
                PLAYING = true;
                NextLevel();
                sceneStateText.SetText("");
                break;
            case 2:
                currentLevel = 0;
                sceneStateText.SetText("");
                NextLevel();
                break;
            case 3:
                sceneStateText.SetText("REPLAY");
                break;
        }
    }*/


}
