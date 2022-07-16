using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticlesManager : MonoBehaviour
{
    public static Action WarpEndAction = null;

    [SerializeField]
    private ParticleSystem innerWarp;
    
    [SerializeField]
    private ParticleSystem outerWarp;
    
    private ParticleSystem.MainModule innerWarpMain, outerWarpMain;

    public AirplaneManager airplaneManager;

    // public float warpTime;

    void OnEnable()
    {
        innerWarpMain = innerWarp.main;
        outerWarpMain = outerWarp.main;

        AirplaneManager.WarpEffectAction += PlayParticles;
    }

    void OnDisable()
    {
        AirplaneManager.WarpEffectAction -= PlayParticles;
    }

    void Start()
    {
        innerWarp.Stop();
        outerWarp.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayParticles()
    {
        innerWarp.Play();
        outerWarp.Play();

        // StartCoroutine(CameraShake.Shake(0.4f, warpTime));

        Invoke("StopParticles", airplaneManager.warpDuration);
    }

    void StopParticles()
    {
        innerWarp.Stop();
        outerWarp.Stop();

        if(WarpEndAction != null)
        {
            WarpEndAction();
        }
    }
}
