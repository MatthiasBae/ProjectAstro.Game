using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceHelper {
	public static Vector2 MousePositionToRectTransformPosition(Vector2 mousePosition, RectTransform rectTransform) {
		Vector2 localPoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, null, out localPoint);
		return localPoint;
	}
}
