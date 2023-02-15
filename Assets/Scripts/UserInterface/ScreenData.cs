using UnityEngine;

public class ScreenData {
	public float Width;
	public float Height;
	public float TargetWidth;
	public float TargetHeight;
	public Vector2 ViewportUnits;
	public float PixelRatio;

	public static ScreenData Create()
	{
		var width = Screen.width;
		var height = Screen.height;
		var num = CameraLocator.Instance.PixelPerfectCamera.pixelRatio * 16;
		Vector2 viewPortUnits = new Vector2()
		{
			x = width / (float)num,
			y = height /  (float)num
		};
		return new ScreenData() {
			Width = width,
			Height = height,
			TargetWidth = CameraLocator.Instance.PixelPerfectCamera.refResolutionX,
			TargetHeight = CameraLocator.Instance.PixelPerfectCamera.refResolutionY,
			ViewportUnits = viewPortUnits,
			PixelRatio = CameraLocator.Instance.PixelPerfectCamera.pixelRatio
		};
	}
}
