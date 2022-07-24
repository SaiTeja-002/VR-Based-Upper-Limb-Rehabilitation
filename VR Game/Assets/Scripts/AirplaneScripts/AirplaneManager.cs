using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AirplaneManager : MonoBehaviour
{
    public static Action ResetTimerAction = null;
    
    private int lives;
    public GameObject[] livesIndicator;

    public Text counterText;
    public float counter=0.5f;
    
    public Material[] skyboxMaterials;
    
    public Canvas gameOverCanvas;

    private int repeatCount, prevIndex, materialIndex;

    public CameraShake cameraShake;

    public static Action WarpEffectAction = null;

    public float warpDuration, shakeMagnitude, shakeBuffer;

    private bool isWarpingSet = false;
    private float randomWarpTime;


    void OnEnable()
    {
        ParticlesManager.WarpEndAction += ChangeSkybox;
        ChildAirplaneController.AsteroidCollissionAction += AsteroidCollission;
        GameOverCanvasScript.ExitAction += ExitToMenu;
        GameOverCanvasScript.RetryAction += Retry;
    }

    void OnDisable()
    {
        ParticlesManager.WarpEndAction -= ChangeSkybox;
        ChildAirplaneController.AsteroidCollissionAction -= AsteroidCollission;
        GameOverCanvasScript.ExitAction -= ExitToMenu;
        GameOverCanvasScript.RetryAction -= Retry;
    }

    // Start is called before the first frame update
    void Start()
    {
        BluetoothService.CreateBluetoothObject();
        BluetoothService.StartBluetoothConnection("HC-05");

        repeatCount = 1;
        materialIndex = 0;
        prevIndex = materialIndex;

        gameOverCanvas.enabled = false;

        lives = livesIndicator.Length;
    }

    // Update is called once per frame
    void Update()
    {
        // counter += 0.5f;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(WarpEffectAction != null)
            {
                WarpEffectAction();
                Invoke("Rand", shakeBuffer/2f);
            }
        }

        if(!isWarpingSet)
        {
            randomWarpTime = UnityEngine.Random.Range(60f, 120f);
            isWarpingSet = true;
            Invoke("Rand", randomWarpTime);
        }
        
        // counterText.text = counter.ToString();
    }

    void ChangeSkybox()
    {
        materialIndex = UnityEngine.Random.Range(0, skyboxMaterials.Length);

        if(prevIndex == materialIndex)
            repeatCount += 1;
        else
        {
            repeatCount = 1;
            prevIndex = materialIndex;
        }

        while(repeatCount >= 3)
        {
            materialIndex = UnityEngine.Random.Range(0, skyboxMaterials.Length);

            if(prevIndex != materialIndex)
                repeatCount = 1;

            prevIndex = materialIndex;
        }

        
        RenderSettings.skybox = skyboxMaterials[materialIndex];
    }

    void Rand()
    {
        if(WarpEffectAction != null)
        {
            WarpEffectAction();
        }

        StartCoroutine(cameraShake.Shake(shakeMagnitude, warpDuration+shakeBuffer));
        isWarpingSet = false;
    }

    private void AsteroidCollission()
    {   
        if(lives > 0)
        {
            lives -= 1;

            livesIndicator[lives].SetActive(false);
        }
        else
        {
            gameOverCanvas.enabled = true;

            // Invoke("DisableCanvas", 5f);
        }
    }

    void DisableCanvas()
    {
        gameOverCanvas.enabled = false;
    }

    void Retry()
    {
        DisableCanvas();
        lives = livesIndicator.Length;

        for(int i=0; i<livesIndicator.Length; i++)
        {
            livesIndicator[i].SetActive(true);
        }
    }   

    void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    } 
}