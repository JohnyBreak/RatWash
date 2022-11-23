using UnityEditor;
using UnityEngine;

public class SaveMenu_Editor : MonoBehaviour
{
    private static string _path = Application.persistentDataPath + "/saves/Save.save";

    [MenuItem("Save Menu/DeleteSave")]
    public static void DeleteSave() 
    {
        if (System.IO.File.Exists(_path) == false) return;

        System.IO.File.Delete(_path);

    }
}
