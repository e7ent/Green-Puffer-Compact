using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
	[SerializeField]
	private string tag;
	[SerializeField]
	private Camera camera;
	[SerializeField]
	private BoxCollider2D collider;


	private int width, height;


	public void Update()
	{
		if (width == Screen.width && height == Screen.height)
			return;

		width = Screen.width;
		height = Screen.height;
		collider.size = camera.ViewportToWorldPoint(Vector3.one) - camera.ViewportToWorldPoint(Vector3.zero);
	}


	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag(tag))
			return;

		Destroy(other.gameObject);
	}
}
