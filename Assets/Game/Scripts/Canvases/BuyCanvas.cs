using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCanvas : MonoBehaviour
{
    public Action YesCkickEvent;
    public Action NoCkickEvent;
    [SerializeField] private GameObject _holder;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;


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

    public void Show() 
    {
        _holder.SetActive(true);
    }

    public void Hide()
    {
        _holder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
