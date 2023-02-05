using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpawnType
{
    TPiece,
    LPieceLeft,
    JunkToken,
    CoinToken
}

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> piecesPrefabs;
    public List<GameObject> tokenPrefabs;

    public Button spawnTPiece;
    public Button spawnLPieceLeft;
    public Button spawnJunkToken;
    public Button spawnMoneyToken;

    private void Start()
    {
        spawnTPiece.onClick.AddListener(() => Spawn(SpawnType.TPiece));
        spawnLPieceLeft.onClick.AddListener(() => Spawn(SpawnType.LPieceLeft));
        spawnJunkToken.onClick.AddListener(() => Spawn(SpawnType.JunkToken));
        spawnMoneyToken.onClick.AddListener(() => Spawn(SpawnType.CoinToken));
    }

    private void Spawn(SpawnType type)
    {
        foreach (var p in piecesPrefabs)
        {
            if (p.name == type.ToString())
            {
                var go = Instantiate(p);
                var movementControl = go.GetComponent<Movement>();
                movementControl.AttachToCursor();
            }
        }
        
    }

    

/*
    private GameObject AddPlane(Vector3 offset)
    {
        var go = Instantiate(planePrefab, offset, Quaternion.identity);
        var plane = go.transform.GetChild(0);
        float adjustedPlaneDimension = plane.transform.localScale.z * _roomSize;
        plane.localScale = new Vector3(adjustedPlaneDimension, adjustedPlaneDimension, adjustedPlaneDimension);
        return go;
    }

    private void AddGrid(GameObject parent, Vector3 offset)
    {
        //int lineCount = roomDimension + 1;
        for (int i = 0; i <= _roomSize; i++)
        {
            float offsetMargin = (float) _roomSize / 2 - _roomSize + OffsetCorrection;
            var horizontalLine = Instantiate(horizontalPrefab, 
                new Vector3(offset.x + OffsetCorrection, 0, i + offset.z + offsetMargin), Quaternion.identity);
            var horizontalScale = horizontalLine.transform.localScale;
            horizontalScale = new Vector3(_roomSize, horizontalScale.y, horizontalScale.z);
            horizontalLine.transform.localScale = horizontalScale;
            var verticalLine = Instantiate(verticalPrefab, 
                new Vector3(i + offset.x + offsetMargin, 0, offset.z + OffsetCorrection), Quaternion.identity);
            var verticalScale = verticalLine.transform.localScale;
            verticalScale = new Vector3(verticalScale.x, verticalScale.y, _roomSize);
            verticalLine.transform.localScale = verticalScale;
            horizontalLine.transform.parent = parent.transform;
            verticalLine.transform.parent = parent.transform;
        }
    }*/
}
