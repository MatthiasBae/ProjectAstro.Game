using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetDatabaseUtil {
    public static List<string> GetAssetNames(string path, string assetNameBase) {
        var list = Directory.GetFiles($"{Application.dataPath}/{path}", $"*{assetNameBase}*.png", SearchOption.TopDirectoryOnly).ToList();
        var names = new List<string>();
        foreach(var item in list) {
            names.Add(Path.GetFileName(item));
        }
        return names;
    }
}
