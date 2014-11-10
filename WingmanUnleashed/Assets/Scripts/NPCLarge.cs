using UnityEngine;
using System.Collections;

public class NPCLarge : MonoBehaviour {
    public LargeCharacters CharacterType = LargeCharacters.Gamer;
    public PartyTheme OutfitTheme = PartyTheme.Formal;
    public bool RandomIdleDialogue = true;
    public bool UseNavMesh = false;
    public bool Wander = false;

    void Start()
    {
        SetupTexture();
        if (!UseNavMesh)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
        if (!Wander)
        {
            gameObject.GetComponent<Wanderer>().enabled = false;
        }
        if (RandomIdleDialogue)
        {
            SetupRandomDialogue();
        }
    }

    private void SetupRandomDialogue()
    {
        Interactable interactable = gameObject.AddComponent<Interactable>();
        interactable.Action = "Talk to";
        interactable.name = "Default";
        RandomConversible randomConversible = gameObject.AddComponent<RandomConversible>();
        Correspondence correspondance = gameObject.AddComponent<Correspondence>();
        correspondance.Conversations = new Conversation[5];

        Conversation conversation1 = gameObject.AddComponent<Conversation>();
        Dialog dialog1a = gameObject.AddComponent<Dialog>();
        conversation1.Beginning = dialog1a;
        correspondance.Conversations[0] = conversation1;

        Conversation conversation2 = gameObject.AddComponent<Conversation>();
        Dialog dialog2a = gameObject.AddComponent<Dialog>();
        conversation2.Beginning = dialog2a;
        correspondance.Conversations[1] = conversation2;

        Conversation conversation3 = gameObject.AddComponent<Conversation>();
        Dialog dialog3a = gameObject.AddComponent<Dialog>();
        conversation3.Beginning = dialog3a;
        correspondance.Conversations[2] = conversation3;

        Conversation conversation4 = gameObject.AddComponent<Conversation>();
        Dialog dialog4a = gameObject.AddComponent<Dialog>();
        conversation4.Beginning = dialog4a;
        correspondance.Conversations[3] = conversation4;

        Conversation conversation5 = gameObject.AddComponent<Conversation>();
        Dialog dialog5a = gameObject.AddComponent<Dialog>();
        conversation5.Beginning = dialog5a;
        correspondance.Conversations[4] = conversation5;

        switch (CharacterType)
        {
            case LargeCharacters.Gamer:
                dialog1a.NPCDialog = "I have like 30 followers on my Switch Stream.";
                dialog2a.NPCDialog = "The latest Hat Man movie was insufferabley incorrect in regards to Cane's backstory. Typical mainstream media.";
                dialog3a.NPCDialog = "Wanna see my level 9999 CyberWhale on YoY?";
                dialog4a.NPCDialog = "Did you get invited to the exclusive beta test of - who am I kidding, of course you didn't.";
                dialog5a.NPCDialog = "I hear there's this new 'wingman' game in development. Sounds like a game for filthy casuals.";
                break;
            case LargeCharacters.RichMan:
                dialog1a.NPCDialog = "<i>Pretentiously jolly laughter</i>";
                dialog2a.NPCDialog = "I paid for a large portion of this party. Only the very best for my closest - wait, who are you?";
                dialog3a.NPCDialog = "Some say that my greatest weakness is that I am just far too humble.";
                dialog4a.NPCDialog = "I hope this party ends soon. I have five more to attend tonight!";
                dialog5a.NPCDialog = "Sometimes I toss money off this rooftop and watch the people down below. It's like the ancient ducks with bread.";
                break;
            case LargeCharacters.RichWoman:
                dialog1a.NPCDialog = "Hmmmmmmmmmph.";
                dialog2a.NPCDialog = "What do you want? Never mind, I don't care.";
                dialog3a.NPCDialog = "I should think a party of this caliber would warrant more care in outfit planning. Evidently not.";
                dialog4a.NPCDialog = "I presume you have a good reason for staring at me?";
                dialog5a.NPCDialog = "<i>Pretends not to see you</i>";
                break;
        }
    }

    private void SetupTexture()
    {
        Material mat;
        string matName = "";
        switch (CharacterType)
        {
            case LargeCharacters.Gamer:
                matName = "CharLarge_Gamer";
                break;
            case LargeCharacters.RichMan:
                matName = "CharLarge_RichMan";
                break;
            case LargeCharacters.RichWoman:
                matName = "CharLarge_RichWoman";
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
