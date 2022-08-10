using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSubjoin : MonoBehaviour
{
    public ParticleSystem PS;
    public ParticleSystem.MainModule PSMainModule;

    private Action<GameObject> onParticleCollision;
    private Action<GameObject> onParticleTrigger;
    private Action<GameObject> onParticleSystemStopped;
    private void Awake()
    {
        PS = GetComponent<ParticleSystem>();
        if (PS == null)
        {
            return;
        }
        PSMainModule = PS.main;   //获取MainModule
        PSMainModule.stopAction = ParticleSystemStopAction.Callback;  //设置结束时调用回调
    }

    public void SetOnParticleCollision(Action<GameObject> action)
    {
        onParticleCollision = action;
    }
    public void SetOnParticleTrigger(Action<GameObject> action)
    {
        onParticleTrigger = action;
    }
    public void SetOnParticleSystemStopped(Action<GameObject> action)
    {
        onParticleSystemStopped = action;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (onParticleCollision != null)
        {
            onParticleCollision(other);
        }
    }
    private void OnParticleTrigger()
    {
        if (onParticleTrigger != null)
        {
            onParticleTrigger(this.gameObject);
        }
    }
    private void OnParticleSystemStopped()
    {
        if (onParticleSystemStopped != null)
        {
            onParticleSystemStopped(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        onParticleCollision = null;
        onParticleTrigger = null;
        onParticleSystemStopped = null;
        PS = null;
    }
}
