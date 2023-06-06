using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BeatSpawner : MonoBehaviour
{
    [SerializeField]
    public BeatStream beatStream;

    [SerializeField]
    private Transform[] spawnLocations;

    [SerializeField]
    private GameObject beatPrefab;

    [SerializeField]
    private float beatsPerMinute;

    public SoundManager soundManager;
    float nextBeatToSpawnNote = 0.0f;

    private float timer;

    // Target Locations:
    [SerializeField]
    private Transform playerCenter;
    [SerializeField]
    private Transform playerUp;
    [SerializeField]
    private Transform playerLeft;
    [SerializeField]
    private Transform playerRight;


    public GameManager gameManager;

    private void Update() 
    {
        if (soundManager.songPositonInBeats >= nextBeatToSpawnNote)
        {
            BeatInfo nextBeatInfo = beatStream.getNextBeat();

            if (nextBeatInfo == null)
            {
                // Game over
                gameManager.GameEnd();
                nextBeatToSpawnNote = 0;
                return;
            }

            SpawnBeat(nextBeatInfo);
            nextBeatToSpawnNote += nextBeatInfo.beatDelayAfter;
        }


        timer += Time.deltaTime;
    }

    void SpawnBeat(BeatInfo info)
    {
        Beat beat =  Instantiate(beatPrefab, spawnLocations[0]).GetComponent<Beat>();

        switch (info.targetArea)
        {
            case BeatInfo.beatTargetArea.Left:
                beat.setTargetPosition(playerLeft);
                break;
            
            case BeatInfo.beatTargetArea.Right:
                beat.setTargetPosition(playerRight);
                break;

            case BeatInfo.beatTargetArea.Up:
                beat.setTargetPosition(playerUp);
                break;

            case BeatInfo.beatTargetArea.Center:
                beat.setTargetPosition(playerCenter);
                break;
        }
    }

}
