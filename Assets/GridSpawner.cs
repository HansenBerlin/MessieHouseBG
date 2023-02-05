using UnityEngine;
using UnityEngine.Serialization;

public class GridSpawner : MonoBehaviour
{
    public GameObject horizontalPrefab;
    public GameObject verticalPrefab;
    public GameObject planePrefab;
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
        int z = 0;
        int x = 0;
        float correction = _roomSize % 2 == 0 ? 0 : 0.5F;
        while (z < _houseSize)
        {
            x = x == _houseSize ? 0 : x;
            var offset = new Vector3(x * (_roomSize + Margin) + correction, 0, z * (_roomSize + Margin) + correction);
            var go = AddPlane(offset);
            AddGrid(go, offset);
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
}
