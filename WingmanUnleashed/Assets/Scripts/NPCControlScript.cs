using UnityEngine;
using System.Collections;

public abstract class NPCControlScript : MonoBehaviour 
{
    public PartyTheme OutfitTheme = PartyTheme.Formal;
    public bool RandomIdleDialogue = false;
    public bool UseNavMesh = false;
    public bool Wander = false;

	// Use this for initialization
	void Start () 
    {
        StartSetup();
	}

    protected virtual void StartSetup()
    {
        SetThemeOutfit(OutfitTheme);
        if (!UseNavMesh)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            Wander = false;
            GetComponent<CharacterAnimator>().ResetToIdle();
        }
        if (!Wander)
        {
            if (gameObject.GetComponent<Wanderer>() != null)
            {
                gameObject.GetComponent<Wanderer>().enabled = false;
                GetComponent<CharacterAnimator>().ResetToIdle();
            }
        }
        if (RandomIdleDialogue)
        {
            SetupRandomDialogue();
        }
    }

	// Update is called once per frame
	void Update () 
    {
        UseNavMesh = gameObject.GetComponent<NavMeshAgent>().enabled;
        Wander = gameObject.GetComponent<Wanderer>().enabled;
	}

    protected abstract void SetThemeOutfit(PartyTheme outfitTheme);

    protected Dialog[] randomDialogs;
    protected virtual void SetupRandomDialogue()
    {
        GameObject conversationInformation = gameObject.transform.FindChild("ConversationInformation").gameObject;
        conversationInformation.transform.FindChild("NPCOverheadDisplay").gameObject.SetActive(true);

        #pragma warning disable 0219
        RandomConversible randomConversible = conversationInformation.AddComponent<RandomConversible>();
        #pragma warning restore 0219

        int numRandomDialogs = 5;

        Correspondence correspondance = conversationInformation.AddComponent<Correspondence>();
        correspondance.Conversations = new Conversation[numRandomDialogs];

        randomDialogs = new Dialog[numRandomDialogs];

        for (int i = 0; i < numRandomDialogs; i++)
        {
            Conversation conversation = conversationInformation.AddComponent<Conversation>();
            randomDialogs[i] = conversationInformation.AddComponent<Dialog>();
            conversation.Beginning = randomDialogs[i];
            correspondance.Conversations[i] = conversation;
        }
        conversationInformation.GetComponent<Interactable>().enabled = true;
    }

    public void ActivateWanderer()
    {
        if (!Wander)
        {
            Wander = true;
            if (!UseNavMesh)
            {
                ActivateNavmesh();
            }
            GetComponent<CharacterAnimator>().ResetToIdle();
            gameObject.GetComponent<Wanderer>().enabled = true;
        }
    }
    public void DeactivateWanderer()
    {
        if (Wander)
        {
            Wander = false;
            gameObject.GetComponent<Wanderer>().enabled = false;
            GetComponent<CharacterAnimator>().ResetToIdle();
        }
    }
    public void ActivateNavmesh()
    {
        if (!UseNavMesh)
        {
            UseNavMesh = true;
            GetComponent<CharacterAnimator>().ResetToIdle();
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
    public void DeactivateNavmesh()
    {
        if (UseNavMesh)
        {
            UseNavMesh = false;
            if (Wander)
            {
                DeactivateWanderer();
            }
            GetComponent<CharacterAnimator>().ResetToIdle();
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    public void ChangeIntoOutfit(PartyTheme outfitTheme)
    {
        SetThemeOutfit(outfitTheme);
    }
}
