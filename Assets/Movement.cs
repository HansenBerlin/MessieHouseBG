using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float gridSize = 1f;
    public List<GameObject> _cubes;
    public bool canRotate;
    private Camera _mainCamera;
    private bool _isAttached;
    private bool _canGetDetached = true;
    private GameObject _text;
    private float _y;
    private List<BoxCollider> _collider;
    private readonly List<MaterialController> _controller = new();

    private void Start()
    {
        _mainCamera = Camera.main;
        _y = transform.position.y;

        if (canRotate == false)
        {
            return;
        }

        foreach (var c in _cubes)
        {
            var ct = c.GetComponent<MaterialController>();
            _controller.Add(ct);
        }
        foreach (Transform t in transform.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("piecetext"))
            {
                _text = t.gameObject;
            }
        }
    }
    
    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Piece") && _isAttached)
        {
            _canGetDetached = false;
            foreach (var ct in _controller)
            {
                ct.SwitchColor(230, 81, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Piece") && _isAttached)
        {
            _canGetDetached = false;
            foreach (var ct in _controller)
            {
                ct.SwitchColor(230, 81, 0);
            }
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Piece") && _isAttached)
        {
            _canGetDetached = true;
            foreach (var ct in _controller)
            {
                ct.SwitchColor(63, 81, 181);
            }        
        }
    }

    private void Update()
    {
        if (_isAttached)
        {
            bool hit = Physics.Raycast (_mainCamera.ScreenPointToRay (Input.mousePosition), out var hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Water")); 
            if (hit)
            {
                transform.position = new Vector3(RoundToNearestGrid(hitInfo.point.x), _y + 0.01F, RoundToNearestGrid(hitInfo.point.z));
            }  
            if (Input.GetMouseButtonDown(1) && canRotate)
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

    private void OnMouseDown()
    {
        if (_isAttached == false)
        {
            _isAttached = true;
        }
        else if (_isAttached && _canGetDetached)
        {
            _isAttached = false;
            var position = transform.position;
            position = new Vector3(RoundToNearestGrid(position.x), _y, RoundToNearestGrid(position.z));
            transform.position = position;
        }
    }

    public void AttachToCursor()
    {
        _isAttached = true;
    }

    
}
