using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class RobotCounter : MonoBehaviour
{

    public static RobotCounter Instance { get; private set; }

    private TMP_Text robotCounter;

    [HideInInspector] public int robotCount;

    [HideInInspector] public int fixedRobots = 0;

    public bool finalLevel;

    public bool LevelComplete => fixedRobots >= robotCount;

    void Awake()
    {
        robotCounter = GetComponent<TMP_Text>();

        Instance = this;

        UpdateRobotCount();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixRobot()
    {
        fixedRobots++;
        UpdateRobotCount();

        if (fixedRobots >= robotCount)
        {
            if (finalLevel)
            {
                Debug.Log($"RC: {GameOverText.Instance}");
                GameOverText.Instance.GameWin();
            }
            else GameOverText.Instance.LevelWin();
        }

    }

    private void UpdateRobotCount()
    {
        CountRobots();
        robotCounter.text = $"Fixed Robots: {fixedRobots} / {robotCount}";
    }

    public void CountRobots()
    {
        robotCount = GameObject.FindGameObjectsWithTag("Robot").Count();
        }



}
