using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.ThreeDimensional;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCycleController : MonoBehaviour
{
    public List<GameObject> threeDInstances;
    public List<string> itemNames;
    public Button back;
    public Button forward;
    private int _currentlySelectedIndex;
    private ButtonEventHandler _buttonEventHandler;

    private void Start()
    {
        _buttonEventHandler = transform.GetComponent<ButtonEventHandler>();
        if (threeDInstances != null)
        {
            foreach (var i in threeDInstances)
            {
                i.SetActive(false);
            }
            _currentlySelectedIndex = 0;
            UpdateCurrentlySelectedRotator();
            //threeDInstances[_currentlySelectedIndex].SetActive(true);
        }
        back.onClick.AddListener(() => Toggle(true));
        forward.onClick.AddListener(() => Toggle(false));
    }

    private void UpdateCurrentlySelectedRotator()
    {
        threeDInstances[_currentlySelectedIndex].SetActive(true);
        var rotator = threeDInstances[_currentlySelectedIndex].GetComponent<RotateUIObject3D>();
        _buttonEventHandler.rotator = rotator;
        _buttonEventHandler.text = itemNames[_currentlySelectedIndex];
    }
    
    private void Toggle(bool isBack)
    {
        threeDInstances[_currentlySelectedIndex].SetActive(false);
        var uiObject = threeDInstances[_currentlySelectedIndex].GetComponent<UIObject3D>();
        var v = uiObject.TargetRotation;
        v.y = 45F;
        
        if (isBack)
        {
            _currentlySelectedIndex = _currentlySelectedIndex == 0 ? threeDInstances.Count - 1 : _currentlySelectedIndex -= 1;
        }
        else
        {
            _currentlySelectedIndex = _currentlySelectedIndex == threeDInstances.Count - 1 ? 0 : _currentlySelectedIndex += 1;
        }
        UpdateCurrentlySelectedRotator();
    }
}
