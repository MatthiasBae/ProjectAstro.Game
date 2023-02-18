using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceResizer : MonoBehaviour {
	public CanvasScaler CanvasScaler;
	public int BaseScale = 1;
	public void Start(){
		AppController.Instance.PixelRatioChanged += this.OnPixelRatioChanged;
	}

	public void OnPixelRatioChanged(ScreenData screenData){
		//Debug.Log("OnPixelRatioChanged");
		var scaleFactor = screenData.PixelRatio / this.BaseScale;
		this.CanvasScaler.scaleFactor = scaleFactor;
	}
}
