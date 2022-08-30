using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Lock : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TextMeshProUGUI _counterText;
    [SerializeField]
    private GameObject _winningPanel;    
    [SerializeField]
    private GameObject _chestPanel;

    private int _counter = 0;
    private int _maxItemsCount;

    public Image Image => _image;
    
    public void Init(int maxItemsCount, Color color)
    {
        _counter = 0;
        _maxItemsCount = maxItemsCount;
        _image.color = color;
        _counterText.text = $"{_counter} / {_maxItemsCount}";
    }
    
    public void OnDrop(PointerEventData eventData)
    {               
        var item = eventData.pointerDrag.GetComponent<Item>();
        
        if (item)
        {
            if (_image.color.EqualsTo(item.Color))
            {
                item.gameObject.SetActive(false);
                item.OnEndDrag(eventData);
                _counter++;                
                _counterText.text = $"{_counter} / {_maxItemsCount}";

                if (_counter == _maxItemsCount)
                {
                    _winningPanel.SetActive(true);                    
                    _chestPanel.SetActive(false);
                }
            }
        }        
    }
}
