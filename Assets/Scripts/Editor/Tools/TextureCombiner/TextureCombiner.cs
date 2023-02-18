using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextureCombiner : EditorWindow {
	
	public string SavePath = Application.dataPath;
	public List<Texture2D> Textures;
	
	
	[MenuItem("Window/Tools/Texture Combiner")]
	static void Init() {
		// Get existing open window or if none, make a new one:
		TextureCombiner window = (TextureCombiner)EditorWindow.GetWindow(typeof(TextureCombiner));
		window.Show();
	}

	void OnGUI() {
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		this.SavePath = EditorGUILayout.TextField("Text Field", this.SavePath);
		//this.Textures = EditorGUILayout.ObjectField(this.Textures, typeof(List<Texture2D>), true);
	}
}
