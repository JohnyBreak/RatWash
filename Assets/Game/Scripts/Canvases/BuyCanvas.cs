using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCanvas : MonoBehaviour
{
    public Action OpenScreenEvent;
    public Action HideScreenEvent;
    public Action YesCkickEvent;
    public Action NoCkickEvent;
    [SerializeField] private GameObject _holder;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    [SerializeField] private TMPro.TextMeshProUGUI _priceText;
    [SerializeField] private string _prefix = "For ";
    // Start is called before the first frame update
    void Start()
    {
        _yesButton.onClick.AddListener(OnYesClick);
        _noButton.onClick.AddListener(OnNoClick);
        Hide();
    }

    private void OnDestroy()
    {
        _yesButton.onClick.RemoveListener(OnYesClick);
        _noButton.onClick.RemoveListener(OnNoClick);
    }

    private void OnYesClick() 
    {
        YesCkickEvent?.Invoke();
        Hide();
    }

    private void OnNoClick()
    {
        NoCkickEvent?.Invoke();
        Hide();
    }

    public void Show(int cost) 
    {
        OpenScreenEvent?.Invoke();
        _priceText.text = $"{_prefix} {cost}";
        _holder.SetActive(true);
    }

    public void Hide()
    {
        HideScreenEvent?.Invoke();
        _holder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
