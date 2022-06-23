using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    public List<LevelPieceBaseSetup> pieceBaseSetups;

    [SerializeField] private int index;
    private GameObject currentLevel;

    private List<LevelPieceBase> spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBaseSetup levelPieceBaseSetup;

    private void Awake()
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
