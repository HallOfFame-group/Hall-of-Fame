using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        nodeSpawner = Instantiate<GameObject>(nodeSpawnerObject, spawnLocation, new Quaternion(), nodePanel).GetComponent<NodeSpawner>();
        beatLine = Instantiate<GameObject>(beatLineObject, beatLineLocation, new Quaternion(), nodePanel).GetComponent<BeatLine>();

        // By default, hides the UI elements
        container.gameObject.SetActive(false);
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

    }

    /// <summary>
    /// Display the rhythm combo on screen
    /// </summary>
    public void Display()
    {
        container.gameObject.SetActive(true);
    }

    /// <summary>
    /// End the rhythmcombo
    /// </summary>
    public void End()
    {

    }
    #endregion
}
