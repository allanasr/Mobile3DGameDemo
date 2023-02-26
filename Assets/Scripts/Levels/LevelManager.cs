using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    public List<LevelPieceBaseSetup> pieceBaseSetups;

    [SerializeField] private int index;
    private GameObject currentLevel;

    private List<LevelPieceBase> spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBaseSetup levelPieceBaseSetup;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float timeBetweenPieces = .2f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        CreateLevelPieces();
    }

    private void SpawnNextLevel()
    {
        if(currentLevel)
        {
            Destroy(currentLevel);
            index++;

            if (index >= levels.Count)
            {
                index = 0;
            }

            currentLevel = Instantiate(levels[index], container);
            currentLevel.transform.localPosition = Vector3.zero;
        }
    }

    private void CreateLevelPieces()
    {
        CleanSpawnedPieces();
        if(levelPieceBaseSetup)
        {
            index++;
            
            if(index >= pieceBaseSetups.Count)
            {
                index = 0;
            }
        }

        levelPieceBaseSetup = pieceBaseSetups[index];

        for(int i = 0; i < levelPieceBaseSetup.startPiecesNumber; i++)
        {
            CreateLevelPiece(levelPieceBaseSetup.levelPieceStart);
        }
        
        for(int i = 0; i < levelPieceBaseSetup.piecesNumber; i++)
        {
            CreateLevelPiece(levelPieceBaseSetup.levelPieceBases);
        }

        for(int i = 0; i < levelPieceBaseSetup.endPiecesNumber; i++)
        {
            CreateLevelPiece(levelPieceBaseSetup.levelPieceEnd);
        }

        ColorManager.Instance.ChangeColorByType(levelPieceBaseSetup.artType);
        CoinsAnimationManager.Instance.ClearRegisteredCoins();

        StartCoroutine(ScalePiecesByTime());
    }

    IEnumerator ScalePiecesByTime()
    {
        foreach(var p in spawnedPieces)
        {
            p.transform.localScale = Vector3.zero;
        }

        yield return null;

        for(int i = 0; i < spawnedPieces.Count; i++)
        {
            spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(timeBetweenPieces);
        }

        CoinsAnimationManager.Instance.StartAnimation();
    }

    private void CleanSpawnedPieces()
    {
        for(int i = spawnedPieces.Count -1 ; i >= 0; i--)
        {
            Destroy(spawnedPieces[i].gameObject);
        }

        spawnedPieces.Clear();
    }
    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if(spawnedPieces.Count > 0)
        {
            var lastPiece = spawnedPieces[spawnedPieces.Count - 1];
            spawnedPiece.transform.localPosition = lastPiece.endPiece.position;
        }
        else
        {
            spawnedPiece.transform.localPosition = Vector3.zero;
        }
        foreach(var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(levelPieceBaseSetup.artType).gameObject);
        }
        spawnedPieces.Add(spawnedPiece);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            CreateLevelPieces();
        }
    }
}
