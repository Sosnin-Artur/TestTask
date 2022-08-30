using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // [SerializeField]
    // private ItemData _data;

    // public ItemData Data
    // {
    //     get => _data;
    //     set => _data = value;
    // }

    [SerializeField]
    private Image _image;
    [SerializeField]
    private Canvas _mainCanvas;
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private RectTransform _rectTransform;
    
    private Transform _draggingParent;
    private Transform _originalParent;

    private bool _isBlocked;
    private int _siblingIndex;
    private Color _color;    

    public Color Color
    {
        get => _color;        
    }

    public bool IsBlocked
    {
        get => _isBlocked;        
    }    

    public void SetUp(Transform draggingParent, Color color, bool isBlocked = false)
    {        
        _originalParent = transform.parent;
        _draggingParent = draggingParent;
        _siblingIndex = _originalParent.GetSiblingIndex();

        _color = color;        
        _image.color = new Color(color.r, color.g, color.b);                
        _isBlocked = isBlocked;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {        
        transform.SetParent(_draggingParent);  
        _canvasGroup.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {                
        transform.SetParent(_originalParent);
        transform.SetSiblingIndex(_siblingIndex);        
        _canvasGroup.blocksRaycasts = true;
    }
}
