using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _canvasHolder;
    //[SerializeField] private Image _joystickImg;
    private Button _button;
    private WasherStorage _washer;

    [SerializeField]
    private List<RatListElement> _ratListElements;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);

        _washer.RatListChangedEvent -= UpdateElementsWhileMenuOpen;
    }

    [Inject]
    private void Constructor(WasherStorage washer)
    {
        _washer = washer;
        _washer.RatListChangedEvent += UpdateElementsWhileMenuOpen;
    }

    private void SetListElements() 
    {
        RatListElement tempElement;
          var dictionary = _washer.GetRatCounts();
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

    private RatListElement GetElement(RatSettings.RatType type) 
    {
        foreach (var item in _ratListElements)
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
public class RatListElement 
{
    public RatSettings.RatType Type;
    public TextMeshProUGUI Text;
}
