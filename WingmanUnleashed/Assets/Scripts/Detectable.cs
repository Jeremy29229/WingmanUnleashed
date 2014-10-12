using UnityEngine;

public class Detectable : MonoBehaviour
{
	/// <summary>
	/// Hidden is less than 0
	/// </summary>
	public int DetectionValue = 0;
	public Color color = new Color(1, 1, 1, 1);

	void Update()
	{
		if (DetectionValue < 0)
		{
			RemoveRed();

		}
		else
		{
			AddRed();
		}
		FixColorBounds();

		renderer.material.color = color;
	}

	void AddRed()
	{
		color += new Color(0, -0.0095f, -0.0095f, 0.0f);
	}

	void RemoveRed()
	{
		color += new Color(0, +0.0095f, +0.0905f, 0.0f);
	}

	void FixColorBounds()
	{
		color.r = (color.r > 1) ? 1 : color.r;
		color.r = (color.r < 0) ? 0.0f : color.r;

		color.g = (color.g > 1) ? 1 : color.g;
		color.g = (color.g < 0) ? 0.0f : color.g;

		color.b = (color.b > 1) ? 1 : color.b;
		color.b = (color.b < 0) ? 0.0f : color.b;

		color.a = 1;
	}
}
