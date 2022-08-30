using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private int _blockedKeysCount = 3;
    [SerializeField]
    private List<Item> _items;
    [SerializeField]
    private List<Color> _colors;    
    [SerializeField]
    private Transform _draggingParent;    
    [SerializeField]
    private Transform _originalParent;    
    [SerializeField]
    private GameObject _chestPanel;    
    [SerializeField]
    private Lock _lock;

    private Color _lockColor;

    public void Reset()
    {        
        for (int i = 0, length = _items.Count; i < length; i++)
        {            
            _items[i].gameObject.SetActive(true);
            _items[i].transform.SetParent(_originalParent);
        }
    }

    public void Open()
    {        
        _lockColor = _colors[Random.Range(0, _colors.Count)];        
        _lock.Init(_blockedKeysCount, new Color(_lockColor.r, _lockColor.g, _lockColor.b));
        
        SetUpMinKeysCount();

        for (int i = 0, length = _items.Count; i < length; i++)
        {            
            if (!_items[i].IsBlocked)
            {                
                _items[i].SetUp(_draggingParent, _colors[Random.Range(0, _colors.Count)]);
            }
        }

        _chestPanel.SetActive(true);
    }

    private void SetUpMinKeysCount()
    {
        for (int i = 0; i <= _blockedKeysCount; i++)
        {            
            int itemInd = Random.Range(0, _items.Count);                        
            _items[itemInd].SetUp(_draggingParent, _colors[Random.Range(0, _colors.Count)], true);
        }
    }
}
