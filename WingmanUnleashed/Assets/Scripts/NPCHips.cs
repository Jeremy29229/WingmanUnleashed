using UnityEngine;
using System.Collections;

public class NPCHips : MonoBehaviour 
{
    public HipsCharacters CharacterType = HipsCharacters.BlondeGirl;
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
            case HipsCharacters.AgingWoman:
                dialog1a.NPCDialog = "Harumph.";
                dialog2a.NPCDialog = "They say the drinks at this party are imported.";
                dialog3a.NPCDialog = "My niece invited me here. She's a doctor you know.";
                dialog4a.NPCDialog = "<i>Passive agressive cough</i>";
                dialog5a.NPCDialog = "Did you know my son in law is a chef? Why they don't have him cater <i>every</i> party is beyond me.";
                break;
            case HipsCharacters.BlondeGirl:
                dialog1a.NPCDialog = "You see that guy? He's totes checking me out. NO don't look! Play it cool!";
                dialog2a.NPCDialog = "O.M.G. My selfie in this outfit already has like 50 ups.";
                dialog3a.NPCDialog = "#party #datmusictho #someweirdokeepstalkingtome #whoevenisthisguy";
                dialog4a.NPCDialog = "<i>Whispers:</i>Did you hear the Shenheizzer's here? Like. I can't even. I can't. I can. Not.";
                dialog5a.NPCDialog = "Sometimes I feel like the people at these parties know, like, two whole dances. It's super weird.";
                break;
            case HipsCharacters.DarkHairWoman:
                dialog1a.NPCDialog = "That economy huh?";
                dialog2a.NPCDialog = "Do you always just walk up to people, nothing to say, and wait for them to talk to you?";
                dialog3a.NPCDialog = "I'm pretty sure you're not supposed to be here. But then, I don't really <i>want</i> to be here. So whatever.";
                dialog4a.NPCDialog = "The food at this party is so good. I'm thinking about stuffing some in my pockets for later.";
                dialog5a.NPCDialog = "I'm so glad they created those bigger-on-the-inside-pockets. Where else would I put my purse?";
                break;
            case HipsCharacters.GingerWoman:
                dialog1a.NPCDialog = "Did you see that guy over there? In the glasses? So cute. So cute.";
                dialog2a.NPCDialog = "The day they outlawed laser eye surgery was historic day.";
                dialog3a.NPCDialog = "Can you believe? There was a guest using the <i>p word!</i>";
                dialog4a.NPCDialog = "So I was reading this book and the characters were at a party like this and they all died. I hope we don't all die.";
                dialog5a.NPCDialog = "Excuse me, I seem to have lost my phone number, can I please borrow yours? ... That was a joke. Ha.";
                break;
            case HipsCharacters.GothWoman:
                dialog1a.NPCDialog = "<i>Self loathing intensifies</i>";
                dialog2a.NPCDialog = "You ever hike up in the mountains and just howl alone to the party moon?";
                dialog3a.NPCDialog = "Ever seen someone carry around their own dismembered leg?";
                dialog4a.NPCDialog = "This party is alright. You know, if you're into dancing in groupings of random strangers.";
                dialog5a.NPCDialog = "I know you. I could tell the bouncers about you right now. But I like rebels.";
                break;
        }
    }

    private void SetupTexture()
    {
        Material mat;
        string matName = "";
        switch (CharacterType)
        {
            case HipsCharacters.AgingWoman:
                matName = "CharHip_AgingWoman";
                //switch (OutfitTheme)
                //{
                //    case PartyTheme.Casual:

                //        break;
                //    case PartyTheme.Formal:

                //        break;
                //    case PartyTheme.Swim:

                //        break;
                //}
                break;
            case HipsCharacters.BlondeGirl:
                matName = "CharHip_BlondeGirl";
                //switch (OutfitTheme)
                //{
                //    case PartyTheme.Casual:

                //        break;
                //    case PartyTheme.Formal:

                //        break;
                //    case PartyTheme.Swim:

                //        break;
                //}
                break;
            case HipsCharacters.DarkHairWoman:
                matName = "CharHip_GreenSweater";
                //switch (OutfitTheme)
                //{
                //    case PartyTheme.Casual:

                //        break;
                //    case PartyTheme.Formal:

                //        break;
                //    case PartyTheme.Swim:

                //        break;
                //}
                break;
            case HipsCharacters.GingerWoman:
                matName = "CharHip_Turtleneck";
                //switch (OutfitTheme)
                //{
                //    case PartyTheme.Casual:

                //        break;
                //    case PartyTheme.Formal:

                //        break;
                //    case PartyTheme.Swim:

                //        break;
                //}
                break;
            case HipsCharacters.GothWoman:
                matName = "CharHip_Goth";
                //switch (OutfitTheme)
                //{
                //    case PartyTheme.Casual:

                //        break;
                //    case PartyTheme.Formal:

                //        break;
                //    case PartyTheme.Swim:

                //        break;
                //}
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
