using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class OreListCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _canvasHolder;
    //[SerializeField] private Image _joystickImg;
    private Button _button;
    private RecyclingStorage _Recycling;

    [SerializeField]
    private List<OreListElement> _oreListElements;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);

        _Recycling.OreListChangedEvent -= UpdateElementsWhileMenuOpen;
    }

    [Inject]
    private void Constructor(RecyclingStorage washer)
    {
        _Recycling = washer;
        _Recycling.OreListChangedEvent += UpdateElementsWhileMenuOpen;
    }

    private void SetListElements() 
    {
        OreListElement tempElement;
          var dictionary = _Recycling.GetOreCounts();
        foreach (var item in dictionary)
        {

            tempElement = GetElement(item.Key);
            if (item.Value < 1)
            {
                tempElement.Text.gameObject.SetActive(false);
                continue;
            }
            if(tempElement.Text.gameObject.activeInHierarchy == false)
                tempElement.Text.gameObject.SetActive(true);


            tempElement.Text.text = $"{item.Key} {item.Value}";
        }
    }

    private OreListElement GetElement(OreSettings.OreType type) 
    {
        foreach (var item in _oreListElements)
        {
            if (item.Type == type) return item;
        }
        return null;
    }

    private void UpdateElementsWhileMenuOpen() 
    {
        if (_canvasHolder.activeInHierarchy == false) return;

            SetListElements();
    }

    private void OnButtonClick() 
    {
        if (_canvasHolder.activeInHierarchy)
        {
            _canvasHolder.SetActive(false);

            //_joystickImg.enabled = true;
        }
        else
        {
            SetListElements();
            //_joystickImg.enabled = false;
            _canvasHolder.SetActive(true);
        }
    }
}

[System.Serializable]
public class OreListElement 
{
    public OreSettings.OreType Type;
    public TextMeshProUGUI Text;
}
