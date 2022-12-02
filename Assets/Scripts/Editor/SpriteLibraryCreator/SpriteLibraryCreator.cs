using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D.Animation;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using System.Linq;

public class SpriteLibraryCreator : EditorWindow {

   
    public SpriteLibraryAsset CreateSpriteLibrary(string name, string folder) {
        var asset = ScriptableObject.CreateInstance<SpriteLibraryAsset>();
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath($"{name}/{folder}.asset"));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(asset);
        return asset;
    }
}
