using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct RhythmResult
{
    public int perfectCount;
    public int goodCount;
    public int badCount;
    public int missCount;
};

public class RhythmCombo : MonoBehaviour
{
    #region Private Members
    // Singleton reference to rhythm combo script
    private static RhythmCombo rhythmCombo;

    // RhythmCombo container reference
    private static Transform container;

    // Musician profile icon image
    private static Image icon;

    // Music title and artist
    private static Text title;

    // NodeSpanwer location translated from screen location to gameworld location
    [SerializeField] private GameObject nodeSpawnerObject;
    private NodeSpawner nodeSpawner;
    private Vector3 spawnLocation;

    // Beat detection line location translated from screen location to gameworld location
    [SerializeField] private GameObject beatLineObject;
    private BeatLine beatLine;
    private Vector3 beatLineLocation;

    // GUI reference to node press result
    private GameObject [] displayText;

    // Contains combo result obtained from beat line object
    public RhythmResult comboResult;

    private bool spawnFinishedFlag = false;
    #endregion

    #region Public Members
    public static RhythmCombo instance
    {
        get
        {
            if (!rhythmCombo)
            {
                rhythmCombo = FindObjectOfType(typeof(RhythmCombo)) as RhythmCombo;

                // If rhythmCombo still cannot be found, raise error
                if (!rhythmCombo)
                {
                    Debug.LogError("There needs to be one active RhythmCombo script on a GameObject in the scene.");
                }
                else
                {
                    // Initialize rhythmCombo
                    rhythmCombo.Init();
                }
            }

            return rhythmCombo;
        }
    }

    // Using delegate as function callback method
    // This will be called when rhythm combo has finished, and returns the result to caller
    public delegate void RhythmComboCallback();
    public RhythmComboCallback callbackFunc;

    #endregion

    #region Non-Public Methods
    protected void Init()
    {
        // Obtain private references to GUI elements under Canvas
        container = transform.Find("UIContainer");
        icon = container.Find("Icon").GetComponent<Image>();
        title = container.Find("Title").Find("MusicTitleText").GetComponent<Text>();

        // Obtain references to gaming GUI elements
        Transform nodePanel = container.Find("NodePanel");
        Transform beatline = nodePanel.Find("BeatLine");
        beatLineLocation = beatline.position;
        spawnLocation = nodePanel.Find("Spawner").position;

        // Spawn Rhythm Combo non-UI prefabs for processing in-game events
        nodeSpawner = Instantiate<GameObject>(nodeSpawnerObject, spawnLocation, new Quaternion()).GetComponent<NodeSpawner>();
        beatLine = Instantiate<GameObject>(beatLineObject, beatLineLocation, new Quaternion()).GetComponent<BeatLine>();

        // Register callback event handlers
        nodeSpawner.callbackFunc = SpawnFinished;
        beatLine.callbackFunc = NodeProcessed;

        // Provide node spawner reference of beatline for calculating traveling speed
        nodeSpawner.EndlinePosition(beatLineLocation);

        // Obtain visual feed back elements for node pressed event
        displayText = new GameObject[4];

        displayText[0] = beatline.Find("PerfectText").gameObject;
        displayText[1] = beatline.Find("GoodText").gameObject;
        displayText[2] = beatline.Find("BadText").gameObject;
        displayText[3] = beatline.Find("MissText").gameObject;

        // Deactivate all visual components
        foreach(GameObject go in displayText)
        {
            go.SetActive(false);
        }

        // By default, hides the UI elements
        Activate(false);
    }

    // Set Rhythm combo active or not
    private void Activate(bool active)
    {
        container.gameObject.SetActive(active);
        nodeSpawner.gameObject.SetActive(active);
        beatLine.gameObject.SetActive(active);
    }

    // Handling NodeSpawner callback, marks the node spawner has finished spawning process, awaiting for beatline
    private void SpawnFinished()
    {
        Debug.Log("Spawn Finished");
        spawnFinishedFlag = true;
    }

    // Handling BeatLine callback, gets called everytime a node is processed
    private void NodeProcessed(NodePressResult result)
    {
        // Disable all visual UI, then display the correct one
        foreach(GameObject go in displayText)
        {
            go.SetActive(false);
        }
        displayText[(int)result].SetActive(true);

        // Only care when the spawning process is finished as well
        // Invoke callback function when beatline has processed all spawned nodes
        Debug.Log(spawnFinishedFlag);
        Debug.Log(beatLine.nodeCount);
        Debug.Log(nodeSpawner.spawnCount);
        if (spawnFinishedFlag && beatLine.nodeCount == nodeSpawner.spawnCount)
        {
            instance.comboResult = beatLine.rhythmResult;
            callbackFunc();
        }
    }

    #endregion

    #region Public Methods
    public void Test()
    {
        Debug.Log("Testing");
    }

    /// <summary>
    /// Register rhythm combo display information using ComboPiece script
    /// </summary>
    /// <param name="combo"></param>
    public void Register(ComboPiece combo)
    {
        title.text = combo.musicName + " - " + combo.artistName;
        // icon = combo.icon
        nodeSpawner.PrepareNodes(combo.timeNodeArray);
        spawnFinishedFlag = false;
    }

    /// <summary>
    /// Display the rhythm combo on screen
    /// </summary>
    public void Display()
    {
        Activate(true);
        nodeSpawner.StartSpawning();
    }

    /// <summary>
    /// End the rhythmcombo
    /// </summary>
    public void End()
    {
        Activate(false);
    }
    #endregion
}
