using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour {
	public static AppController Instance;
	public ScreenData ScreenData;
	
	public int ReferencePixelRatio = -1;
	public Vector2Int ReferenceResolution;
	public event Action<ScreenData> PixelRatioChanged;
	public event Action ScreenSizeChanged;
	
	private void Awake(){
		AppController.Instance = this;
	}

	private void LateUpdate(){
		this.CheckScreenSizeChanged();
		this.ChechPixelRatioChanged();
	}

	private void CheckScreenSizeChanged(){
		if(Screen.width == this.ReferenceResolution.x && Screen.height == this.ReferenceResolution.y){
			return;
			
		}
		this.ReferenceResolution = new Vector2Int(Screen.width, Screen.height);
		this.ScreenData = ScreenData.Create();
		this.OnScreenSizeChanged();
	}
	
	private void ChechPixelRatioChanged(){
		if(this.ReferencePixelRatio == CameraLocator.Instance.PixelRatio){
			return;
		}
		this.ReferencePixelRatio = CameraLocator.Instance.PixelRatio;
		this.ScreenData = ScreenData.Create();
		this.OnPixelRatioChanged(this.ScreenData);
	}
	
	public void OnPixelRatioChanged(ScreenData screenData){
		this.PixelRatioChanged?.Invoke(screenData);
	}
	
	public void OnScreenSizeChanged(){
		this.ScreenSizeChanged?.Invoke();
	}
}
