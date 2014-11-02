using UnityEngine;
using System;

public class DialogResponse : MonoBehaviour
{
	public Dialog Resulting; //
	public Conversation NewCurrent; //
	public string Text = ""; //
	public bool IsOneTimeOption = false; //
	public int NumTimesSelected = 0; //
	public ItemRequirement[] Items; // but not removal
	public string[] DisguiseNames; //
	public float RequiredInterested = 0.0f; //
	public float RequiredConfidence = 0.0f; //
	public string[] RequiredObjectiveNames; //
}
