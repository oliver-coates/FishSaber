using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{

    private Vector3 upPos;
    private Vector3 downPos;

    public int hits = 0;

    public TextMeshPro missesText;

    public SoundManager soundManager;

    public GameObject startGameSword;
    public Transform startGameSwordParent;
    private Vector3 startGameSwordPosition;

    public GameObject saberLeft;
    public GameObject saberRight;
    public XRInteractorLineVisual xrLine;

    public PlayableDirector mainTimeline; 

    public GameObject healthAndSafetyWarning;

    public LeaderBoard leaderBoard;

    public GameObject startGame2Sword;
    
    public GameObject startGame3Sword;
    public SeaManager seaManager;
    public BeatSpawner beatSpawner;

    // Start is called before the first frame update
    void Start()
    {
        upPos = healthAndSafetyWarning.transform.position;
        downPos = transform.position + Vector3.down * 6f;

        startGameSwordPosition = startGameSword.transform.localPosition;

        StartCoroutine(showHealthWarning());
    }

    public void GameStart()
    {
        StartCoroutine(moveTo(gameObject, upPos, downPos, 1f));

        mainTimeline.Play();

        startGame2Sword.SetActive(false);
        startGame3Sword.SetActive(false);

        seaManager.stormRunning = true;
        
    }

    public void GameEnd()
    {
        leaderBoard.AddScore(hits);

        seaManager.stormRunning = false;

        hits = 0;
        missesText.text = hits + " Sushi Made";
        

        soundManager.StopPlayingSong();
        StartCoroutine(moveTo(gameObject, downPos, upPos, 1f));

        startGameSword.transform.parent = startGameSwordParent;
        startGameSword.transform.localPosition = startGameSwordPosition;
        
        startGameSword.SetActive(true);

        saberLeft.SetActive(false);
        saberRight.SetActive(false);

        startGame2Sword.SetActive(true);
        startGame3Sword.SetActive(true);

        xrLine.enabled = true;

        mainTimeline.Stop();
    }

    public void Hit()
    {
        hits += 1;        
        missesText.text = hits + " Sushi Made";
    }

    IEnumerator moveTo(GameObject obj, Vector3 from, Vector3 to, float time)
    {
        float timer = 0f;

        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            obj.transform.position = Vector3.Lerp(from, to, timer / time);
        }
    }

    IEnumerator showHealthWarning()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(moveTo(healthAndSafetyWarning, upPos, downPos, 0.4f));

        yield return new WaitForSeconds(1f);

        StartCoroutine(moveTo(gameObject, downPos, upPos, 1f));
    }
}
