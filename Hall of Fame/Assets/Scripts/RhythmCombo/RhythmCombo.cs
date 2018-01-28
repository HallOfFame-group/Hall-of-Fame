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
        // Obtain all private references
        container = transform.Find("UIContainer");
        icon = container.Find("Icon").GetComponent<Image>();
        title = container.Find("Title").Find("MusicTitleText").GetComponent<Text>();

        Transform nodePanel = container.Find("NodePanel");
        beatLineLocation = nodePanel.Find("BeatLine").position;
        spawnLocation = nodePanel.Find("Spawner").position;

        nodeSpawner = Instantiate<GameObject>(nodeSpawnerObject, spawnLocation, new Quaternion()).GetComponent<NodeSpawner>();
        nodeSpawner.callbackFunc = SpawnFinished;
        beatLine = Instantiate<GameObject>(beatLineObject, beatLineLocation, new Quaternion()).GetComponent<BeatLine>();
        beatLine.callbackFunc = NodeProcessed;

        // By default, hides the UI elements
        Activate(false);
    }

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
    private void NodeProcessed()
    {
        // Only care when the spawning process is finished as well
        // Invoke callback function when beatline has processed all spawned nodes
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
