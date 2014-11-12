using UnityEngine;
using System;

public class DialogResponse : MonoBehaviour
{
	public Dialog Resulting;
	public Conversation NewCurrent;
	public string Text = "";
	public bool IsOneTimeOption = false;
	public int NumTimesSelected = 0;
	public ItemRequirement[] Items;
	public string[] DisguiseNames;
	public float RequiredInterested = 0.0f;
	public float RequiredConfidence = 0.0f;
	public float AddedInterested = 0.0f;
	public float AddedConfidence = 0.0f;
	public ConvoObjectiveHelper[] RequiredObjectives;
	public float AddedSuspicion = 0.0f;
    public bool visitAfterward;
    public bool followAfterward;
    public GameObject destinationGameObject;
}
