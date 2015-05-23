using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace E7Assets
{
	public static class GameObjectExtensions
	{
		public static Bounds CalculateBounds(this GameObject go)
		{
			var renderers = go.GetComponentsInChildren<Renderer>();
			var bounds = renderers[0].bounds;

			for (int i = 1; i < renderers.Length; i++)
				bounds.Encapsulate(renderers[i].bounds);

			return bounds;
		}
	}
}
