using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    public float gridSize = 1f;
    private Camera _mainCamera;
    private Vector3 _screenPoint;
    private Vector3 _offset;
    private bool _isAttached;
    private GameObject _text;

    private void Start()
    {
        _mainCamera = Camera.main;
 
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("piecetext"))
            {
                _text = t.gameObject;
            }
        }
    }
    
    private void Update()
    {
        if (_isAttached)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            Vector3 cursorPosition = _mainCamera.ScreenToWorldPoint(cursorPoint) + _offset;
            transform.position = new Vector3(RoundToNearestGrid(cursorPosition.x), 0.15F, RoundToNearestGrid(cursorPosition.z));
            if (Input.GetMouseButtonDown(1))
            {
                Rotate();
            }
        }
    }

    private void Rotate()
    {
        transform.Rotate(0, 90, 0);
        _text.transform.Rotate(0,0, 90);

    }

    private float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }

    private void OnMouseDown(){
        _isAttached = !_isAttached;
        if (_isAttached)
        {
            var position = gameObject.transform.position;
            _screenPoint = _mainCamera.WorldToScreenPoint(position);
            _offset = position - _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        }
    }
}
