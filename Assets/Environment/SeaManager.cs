using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaManager : MonoBehaviour
{
    public bool stormRunning;

    [Header("Status:")]
    public float stormIntensity;

    [Header("Settings:")]
    public float gameTime;
    private float timeElapsed;

    public float waveHeightStart;
    public float waveHeightEnd;

    [Header("Skybox")]
    private Material skyboxMat;
    public Shader skyboxShader;
    public Material brightSky;
    public Material darkSky;

    public Texture sun;
    public Texture storm;

    [Header("References")]
    public Waves waves;
    public AudioSource audioSource;

    private void Start() {
        // skyboxMat = new Material(skyboxShader);
        // RenderSettings.skybox.SetFloat("_Blend", 0f);
        // RenderSettings.skybox.SetTexture("_MainTex", sun);
        // RenderSettings.skybox.SetTexture("_Texture2", storm);
    }

    private void Update()
    {
        if (stormRunning)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed -= Time.deltaTime * 20f;
        }

        timeElapsed = Mathf.Clamp(timeElapsed, 0, gameTime);

        stormIntensity = (timeElapsed / gameTime);

        //skyboxMat.Lerp(brightSky, darkSky, (timeElapsed / 100f));
        
        //skyboxMat = darkSky;

        //skyboxMat.Lerp(brightSky, darkSky, 1f);

        //RenderSettings.skybox = skyboxMat;

        // RenderSettings.skybox.SetFloat("_Blend", (timeElapsed / 100f));

        //RenderSettings.skybox.Lerp(brightSky, darkSky, (timeElapsed / 100f));

        waves.octaves[0].height = Mathf.Lerp(waveHeightStart, waveHeightEnd, stormIntensity); 

        audioSource.volume = 0.2f + (stormIntensity * 0.55f);
    }

}
