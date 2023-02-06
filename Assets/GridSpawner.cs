using UnityEngine;
using UnityEngine.Serialization;

public class GridSpawner : MonoBehaviour
{
    public GameObject horizontalPrefab;
    public GameObject verticalPrefab;
    public GameObject planePrefab;
    public GameObject wallPrefab;
    public GameObject settingsInstance;
    private int _roomSize;
    private int _houseSize;
    private const float OffsetCorrection = 0.5F;
    private const float Margin = 1F;
    

    
    private void Start()
    {
        var settings = settingsInstance.GetComponent(typeof(Settings)) as Settings;
        _roomSize = settings.roomSize;
        _houseSize = settings.houseSize;
        float correction = _roomSize % 2 == 0 ? 0 : 0.5F;
        int z = 0;
        int x = 0;
        while (z < _houseSize)
        {
            x = x == _houseSize ? 0 : x;
            var offset = new Vector3(x * (_roomSize + Margin) + correction, 0, z * (_roomSize + Margin) + correction);
            var go = AddPlane(offset);
            AddGrid(go, offset);
            AddWalls(go, offset, x == _houseSize - 1, z == _houseSize - 1);
            x++;
            z = x == _houseSize ? z + 1 : z;
        }
    }

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
    }
    /*
    private void AddWall(GameObject parent, Vector3 offset, bool isLastIteration)
    {
        float offsetMargin = (float) _roomSize / 2 - _roomSize;
        var horizontalWall = Instantiate(wallPrefab, 
            new Vector3(offset.x, 0, offset.z + offsetMargin), Quaternion.identity);
        var horizontalScale = horizontalWall.transform.localScale;
        horizontalScale = new Vector3(_roomSize + 1, horizontalScale.y, horizontalScale.z);
        horizontalWall.transform.localScale = horizontalScale;
        var verticalWall = Instantiate(wallPrefab, 
            new Vector3(offset.x + offsetMargin, 0, offset.z + 1), Quaternion.identity);
        var verticalScale = verticalWall.transform.localScale;
        verticalScale = new Vector3(verticalScale.x, verticalScale.y, _roomSize + 1);
        verticalWall.transform.localScale = verticalScale;
        horizontalWall.transform.parent = parent.transform;
        verticalWall.transform.parent = parent.transform;
    }*/
    
    private void AddWalls(GameObject parent, Vector3 offset, bool isRowEnd, bool isColEnd)
    {
        float offsetMargin = (float) _roomSize / 2 - _roomSize;
        AddWall(parent, offset.x, offset.z + offsetMargin, _roomSize + 1, 0);
        AddWall(parent, offset.x + offsetMargin, offset.z + 1, 0, _roomSize + 1);
        if (isRowEnd)
        {
            //AddWall(parent, offset.x, offset.z + offsetMargin + _roomSize, _roomSize + 1, 0);
            AddWall(parent, offset.x + offsetMargin + _roomSize + 1, offset.z, 0, _roomSize + 1);
        }
        if (isColEnd)
        {
            AddWall(parent, offset.x + 1, offset.z + offsetMargin + _roomSize + 1, _roomSize + 1, 0);
            //AddWall(parent, offset.x + offsetMargin + _roomSize, offset.z + 1, 0, _roomSize + 1);
        }
    }

    private void AddWall(GameObject parent, float xPos, float zPos, float xSc, float zSc)
    {
        var verticalWall = Instantiate(wallPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
        var verticalScale = verticalWall.transform.localScale;
        verticalScale = new Vector3(xSc == 0 ? verticalScale.x : xSc, verticalScale.y, zSc == 0 ? verticalScale.z : zSc);
        verticalWall.transform.localScale = verticalScale;
        verticalWall.transform.parent = parent.transform;
    }
}
