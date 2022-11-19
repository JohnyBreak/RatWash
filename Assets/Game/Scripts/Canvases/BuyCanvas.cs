using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _holder;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    // Start is called before the first frame update
    void Start()
    {
        _yesButton.onClick.AddListener(OnYesClick);
        _noButton.onClick.AddListener(OnNoClick);
    }

    private void OnDestroy()
    {
        _yesButton.onClick.RemoveListener(OnYesClick);
        _noButton.onClick.RemoveListener(OnNoClick);
    }

    private void OnYesClick() 
    {
    }

    private void OnNoClick()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
