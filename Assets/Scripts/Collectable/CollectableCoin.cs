using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : CollectableBase
{
    public Collider collider;
    public float lerp = 5f;
    public bool collect;
    public float minDistance = 1f;
    protected override void OnCollect()
    {
        base.OnCollect();
        collider.enabled = false;
        collect = true;

    }

    protected override void Collect()
    {
        OnCollect();
    }
    private void Update()
    {
        if(collect)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerp * Time.deltaTime);
            if(Vector3.Distance(transform.position,PlayerController.Instance.transform.position) < minDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}
