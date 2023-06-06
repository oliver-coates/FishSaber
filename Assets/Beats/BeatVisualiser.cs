using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public class BeatStream
{
    public List<BeatInfo> beats;
    public int beatInteration = 0;

    public BeatInfo getNextBeat()
    {
        if (beatInteration == beats.Count)
        {
            beatInteration = 0;
            return null;
        }

        beatInteration++;
        return beats[beatInteration-1];
    }

}

[Serializable]
public class BeatInfo
{
    public enum beatTargetArea { Center, Up, Left, Right};
    public int beatDelayAfter;
    public beatTargetArea targetArea;
}

public class BeatVisualiser : MonoBehaviour
{
    // The time for the beat to arrive at the player
    [SerializeField]
    public float timeForBeatToArrive = 0f;

    // Jonathan's beatSpawner script
    [SerializeField]
    private BeatSpawner beatSpawner;

    public float songBpm;
    private float secPerBeat;

    [SerializeField]
    public Vector3 origin;

    private GameObject beatVisualiser;

    private GameObject beatLine;

    [SerializeField]
    private Mesh cubeMesh;

    private bool playing;

    public TextMeshPro beatText;

    // Start is called before the first frame update
    void Start()
    {
        secPerBeat = 60f / songBpm;

        SpawnBeatLine();

        SetupBeatVisualiser();

        SpawnBeats();
    }

    void SpawnBeatLine()
    {
        // Spawns the beat line at the origin
        beatLine = new GameObject("BeatLine");
        beatLine.AddComponent<MeshRenderer>();
        beatLine.AddComponent<MeshFilter>();
        
        beatLine.GetComponent<MeshFilter>().mesh = cubeMesh;

        beatLine.transform.position = origin;
        beatLine.transform.localScale = new Vector3(0.2f, 2f, 0.2f);
    }

    void SetupBeatVisualiser()
    {
        // Spawns a beatVisualiser, for the others to copy
        beatVisualiser = new GameObject("Beat Visualiser");
        beatVisualiser.AddComponent<MeshRenderer>();
        beatVisualiser.AddComponent<MeshFilter>();
        
        beatVisualiser.GetComponent<MeshFilter>().mesh = cubeMesh;

        // Spawn it wayyy of the screen
        beatVisualiser.transform.position = origin + Vector3.down * 1000f;

        beatVisualiser.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    
    public void SpawnBeats()
    {
        List<BeatInfo> songBeats = beatSpawner.beatStream.beats;

        float timeCounter = timeForBeatToArrive;
        timeCounter += beatSpawner.soundManager.firstBeatOffset * secPerBeat;

        foreach (BeatInfo beatInfo in songBeats)
        {
            Vector3 spawnPosition = origin;

            spawnPosition += new Vector3(-1, 0, 0) * timeCounter;

            timeCounter += beatInfo.beatDelayAfter * secPerBeat;

            SpawnBeat(spawnPosition);
        }
    }

    void SpawnBeat(Vector3 position)
    {
        Instantiate(beatVisualiser, position, Quaternion.identity);
    }

    public void StartVisualisation()
    {
        beatText.gameObject.SetActive(true);
        playing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing == true)
        {
            beatLine.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
            beatText.transform.position = beatLine.transform.position + Vector3.forward + Vector3.up;
            beatText.text = Mathf.Round(beatSpawner.soundManager.songPositonInBeats).ToString();
        }
    }
}
