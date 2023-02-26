using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using DG.Tweening;
using System.Linq;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<CollectableCoin> itens;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float timeBetweenPieces = .2f;
    public Ease ease = Ease.OutBack;

    void Start()
    {
        itens = new List<CollectableCoin>();
    }


    public void RegisterCoin(CollectableCoin c)
    {
        if(!itens.Contains(c))
        {
            itens.Add(c);
            c.transform.localScale = Vector3.zero;
        }
    }

    public void ClearRegisteredCoins()
    {
        itens.Clear();
    }
    public void StartAnimation()
    {
        StartCoroutine(ScalePiecesByTime());
    }
    IEnumerator ScalePiecesByTime()
    {
        foreach (var p in itens)
        {
            p.transform.localScale = Vector3.zero;
        }

        Sort();

        yield return null;

        for (int i = 0; i < itens.Count; i++)
        {
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }

    private void Sort()
    {
        itens = itens.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
