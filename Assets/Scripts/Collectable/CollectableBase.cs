using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{

    public ParticleSystem particle;

    [Header("Audio")]

    public AudioSource audioSource;
    private void Awake()
    {
        if(particle)
        {
            particle.transform.SetParent(null);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        gameObject.SetActive(false);
        OnCollect();
    }
    protected virtual void OnCollect()
    {
        //particle.Play();
        //audioSource.Play();
    }
}
