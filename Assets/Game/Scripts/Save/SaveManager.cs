using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public SaveData Save;


    private void Awake()
    {
        LoadSave();
    }

    public void LoadSave() 
    {
        Save = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");
        if (Save == null) Save = new SaveData();
    }

    public void SaveData() 
    {
        SerializationManager.Save(Save);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadSave();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.LogError(Save.MoneyAmount);
        }
    }
}
