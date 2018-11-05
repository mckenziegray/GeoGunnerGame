using UnityEngine;
using UnityEditor;

public class DeletePlayerPrefsScript : EditorWindow
{
	[MenuItem("File/Reset PlayerPrefs")]
	static void ResetPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}
}
