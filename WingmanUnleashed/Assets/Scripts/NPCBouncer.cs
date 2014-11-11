using UnityEngine;
using System.Collections;

public class NPCBouncer : MonoBehaviour {
    public BouncerType BouncerType = BouncerType.Angry;
    public BouncerVariant Variant = BouncerVariant.One;
    public bool RandomIdleDialogue = true;
    public bool UseNavMesh = true;

    void Start()
    {
        SetupTexture();
        if (!UseNavMesh)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CharacterAnimator>().ResetToIdle();
        }
        if (RandomIdleDialogue)
        {
            SetupRandomDialogue();
        }
    }

    private void SetupRandomDialogue()
    {
        GameObject conversationInformation = gameObject.transform.FindChild("ConversationInformation").gameObject;
        conversationInformation.transform.FindChild("NPCOverheadDisplay").gameObject.SetActive(true);

        RandomConversible randomConversible = conversationInformation.AddComponent<RandomConversible>();
        randomConversible.enabled = false;

        Correspondence correspondance = conversationInformation.AddComponent<Correspondence>();
        correspondance.Conversations = new Conversation[5];
        randomConversible.enabled = true;

        Conversation conversation1 = conversationInformation.AddComponent<Conversation>();
        Dialog dialog1a = conversationInformation.AddComponent<Dialog>();
        conversation1.Beginning = dialog1a;
        correspondance.Conversations[0] = conversation1;

        Conversation conversation2 = conversationInformation.AddComponent<Conversation>();
        Dialog dialog2a = conversationInformation.AddComponent<Dialog>();
        conversation2.Beginning = dialog2a;
        correspondance.Conversations[1] = conversation2;

        Conversation conversation3 = conversationInformation.AddComponent<Conversation>();
        Dialog dialog3a = conversationInformation.AddComponent<Dialog>();
        conversation3.Beginning = dialog3a;
        correspondance.Conversations[2] = conversation3;

        Conversation conversation4 = conversationInformation.AddComponent<Conversation>();
        Dialog dialog4a = conversationInformation.AddComponent<Dialog>();
        conversation4.Beginning = dialog4a;
        correspondance.Conversations[3] = conversation4;

        Conversation conversation5 = conversationInformation.AddComponent<Conversation>();
        Dialog dialog5a = conversationInformation.AddComponent<Dialog>();
        conversation5.Beginning = dialog5a;
        correspondance.Conversations[4] = conversation5;

        switch (BouncerType)
        {
            case BouncerType.Angry:
                dialog1a.NPCDialog = "Let me guess, someone stole your mint.";
                dialog2a.NPCDialog = "My cousin's out bouncing fight clubs and what do I get? Party duty.";
                dialog3a.NPCDialog = "No loitering. Oh, wait, this is a party. Never mind.";
                dialog4a.NPCDialog = "Hands to yourself, sneak thief.";
                dialog5a.NPCDialog = "People keep calling us Tigger or something. I don't get the joke. It isn't funny.";
                break;
            case BouncerType.Happy:
                dialog1a.NPCDialog = "They say The Shenheizzer broke the sound barrier... With his voice!";
                dialog2a.NPCDialog = "I need to ask you to stop partying so hard. It's making people nervous.";
                dialog3a.NPCDialog = "This party is under my protection. You have a good time, now.";
                dialog4a.NPCDialog = "I don't know about you, but I am having a blast. A blast!";
                dialog5a.NPCDialog = "These party guests are so polite. I love you guys.";
                break;
            case BouncerType.Stoic:
                dialog1a.NPCDialog = "I used to be a partier like you... What, did you expect me to say more?";
                dialog2a.NPCDialog = "I mostly deal with petty party crashers and drunken fisticuffs.";
                dialog3a.NPCDialog = "Heard about you and your flowered words.";
                dialog4a.NPCDialog = "Pibs think we need their laws. Pffft.";
                dialog5a.NPCDialog = "Don't have money for drinks? You can always pay with your blood.";
                break;
        }
        conversationInformation.GetComponent<Interactable>().enabled = true;
        conversationInformation.GetComponent<Interactable>().InteractableName = "Bouncer";
        conversationInformation.GetComponent<Interactable>().Action = "Talk to";
    }

    private void SetupTexture()
    {
        Material mat;
        string matName = "";
        switch (BouncerType)
        {
            case BouncerType.Angry:
                matName = "Bouncer_Angry";
                break;
            case BouncerType.Happy:
                matName = "Bouncer_Happy";
                break;
            case BouncerType.Stoic:
                matName = "Bouncer_Stoic";
                break;
        }

        switch (Variant)
        {
            case BouncerVariant.One:
                matName += "1";
                break;
            case BouncerVariant.Two:
                matName += "2";
                break;
            case BouncerVariant.Three:
                matName += "3";
                break;
            case BouncerVariant.Four:
                matName += "4";
                break;
        }
        mat = (Material)Resources.Load(matName, typeof(Material));
        //gameObject.renderer.material = mat;
        transform.FindChild("Mesh").GetComponent<Renderer>().material = mat;
        //gameObject.GetComponentsInChildren<Renderer>().FirstOrDefault(r => r.transform.parent.name == "Mesh").material = mat;
    }

    void Update()
    {

    }
}
