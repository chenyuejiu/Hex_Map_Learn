﻿using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadMenu : MonoBehaviour {

    public Text menuLabel, actionButtonLabel;
    public InputField nameInput;
    public HexGrid hexGrid;


    bool saveMode;
    
    public void Open(bool saveMode) {
        this.saveMode = saveMode;
        if (this.saveMode) {
            menuLabel.text = "Save Map";
            actionButtonLabel.text = "save";
        }
        else {
            menuLabel.text = "Load Map";
            actionButtonLabel.text = "Load";
        }

        gameObject.SetActive(true);
        HexMapCamera.Locked = true;
    }

    public void Close() {
        gameObject.SetActive(false);
        HexMapCamera.Locked = false;
    }

    string GetSelectedPath() {
        string mapName = nameInput.text;
        if (mapName.Length == 0) {
            return null;
        }
        return Path.Combine(Application.persistentDataPath, mapName + ".map");
    }

    void Save(string path) {
        //Debug.Log(Application.persistentDataPath);
        //string path = Path.Combine(Application.persistentDataPath, "Test.map");

        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))) {
            writer.Write(1);
            hexGrid.Save(writer);
        }
    }

    void Load(string path) {
        //string path = Path.Combine(Application.persistentDataPath, "Test.map");

        if (!File.Exists(path)) {
            Debug.LogError("File does not exist " + path);
            return;
        }
        using (BinaryReader reader = new BinaryReader(File.OpenRead(path))) {
            int header = reader.ReadInt32();
            if (header <= 1) {
                hexGrid.Load(reader, header);
                HexMapCamera.ValidatePosition();
            }
            else {
                Debug.LogWarning("Unknown map format " + header);
            }
        }
    }

    public void Action() {
        string path = GetSelectedPath();
        if (path == null) {
            return;
        }
        if (saveMode) {
            Save(path);
        }
        else {
            Load(path);
        }
        Close();
    }
}
