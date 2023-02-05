using TMPro;
using UI.ThreeDimensional;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textInstance;
    //private ButtonCycleController _buttonCycleController;
    public bool contains3dObject;
    public string text = "";
    private TMP_Text _textMeshPro;
    public RotateUIObject3D rotator;
    private Button _button;

    private void Start()
    {
        _textMeshPro = textInstance.GetComponent(typeof(TMP_Text)) as TMP_Text;
        _button = transform.GetComponent(typeof(Button)) as Button;
        _textMeshPro.text = text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (contains3dObject)
        {
            rotator.enabled = true;
        }
        _textMeshPro.color = new Color(0.9F, 0.9F, 0.9F);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (contains3dObject)
        {
            rotator.enabled = false;
        }
        _textMeshPro.color = new Color(0.05F, 0.05F, 0.05F);
    }
}
