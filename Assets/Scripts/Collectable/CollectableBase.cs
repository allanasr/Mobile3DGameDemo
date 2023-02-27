using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{

    public string TagCompare = "Player";
    public ParticleSystem particle;

    [Header("Audio")]

    public AudioSource audioSource;
  
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag(TagCompare))
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
        if (particle)
        {
            particle.transform.SetParent(null);
        }
        particle.Play();
        //audioSource.Play();
    }
}
