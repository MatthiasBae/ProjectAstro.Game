using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraLocator : MonoBehaviour {
	public static CameraLocator Instance;
	
	public Camera MainCamera;
	public PixelPerfectCamera PixelPerfectCamera;

	public int PixelRatio{
		get => this.PixelPerfectCamera.pixelRatio;
	}
	
	public float PixelsPerUnit{
		get => this.PixelPerfectCamera.assetsPPU;
	}
	
	private void Awake(){
		CameraLocator.Instance = this;
	}
}
