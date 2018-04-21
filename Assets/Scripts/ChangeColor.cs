using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour {

	public void setColor (float r, float g, float b)
	{
		GetComponent<Image> ().color = new Color(r, g, b);
	}

	public void setRed(float value)
	{
		float green = GetComponent<Image> ().color.g;
		float blue = GetComponent<Image> ().color.b;
		GetComponent<Image> ().color = new Color(value, green, blue);
	}

	public void setGreen(float value)
	{
		float red = GetComponent<Image> ().color.r;
		float blue = GetComponent<Image> ().color.b;
		GetComponent<Image> ().color = new Color(red, value, blue);
	}

	public void setBlue(float value)
	{
		float red = GetComponent<Image> ().color.r;
		float green = GetComponent<Image> ().color.g;
		GetComponent<Image> ().color = new Color(red, green, value);
	}
}
