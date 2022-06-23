using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelPieceBaseSetup : ScriptableObject
{

    public ArtManager.ArtType artType;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPieceStart;
    public List<LevelPieceBase> levelPieceBases;
    public List<LevelPieceBase> levelPieceEnd;

    public int startPiecesNumber = 1;
    public int piecesNumber = 1;
    public int endPiecesNumber = 1;
}
