using UnityEngine;
using System.Collections;
using System.Linq;

public class NPCBasic : MonoBehaviour
{
    public BasicCharacters CharacterType = BasicCharacters.GingerMan;
    public PartyTheme OutfitTheme = PartyTheme.Formal;
    public bool RandomIdleDialogue = true;
    public bool UseNavMesh = true;
    public bool Wander = false;

	void Start () 
    {
        SetupTexture();
        if (!UseNavMesh)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            Wander = false;
            GetComponent<CharacterAnimator>().ResetToIdle();
        }
        if (!Wander)
        {
            gameObject.GetComponent<Wanderer>().enabled = false;
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

        conversationInformation.GetComponent<Interactable>().enabled = true;
        switch (CharacterType)
        {
            case BasicCharacters.Bartender:
                conversationInformation.GetComponent<Interactable>().InteractableName = "Bartender";
                dialog1a.NPCDialog = "I'm not judging your outfit. I'm just giving you constructive criticism. Silently.";
                dialog2a.NPCDialog = "I'm just a bartender. I tend the bar. Or end the bart, it really depends.";
                dialog3a.NPCDialog = "It took me three years to perfect the art of silently washing cups while feigning sympathy towards your problems.";
                dialog4a.NPCDialog = "<i>Silently cleans a glass with an old cloth</i>";
                dialog5a.NPCDialog = "I may look bored, but I'm actually racking up points on my people watching score.";
                break;
            case BasicCharacters.GingerMan:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameMasculine();
                dialog1a.NPCDialog = "Mmhm.";
                dialog2a.NPCDialog = "No. I'm not going to steal your soul. Not yet.";
                dialog3a.NPCDialog = "It's a shame steel trees went extinct. Nice workout.";
                dialog4a.NPCDialog = "What a party.";
                dialog5a.NPCDialog = "Everyone's always too busy to get their own drinks. Lazy.";
                break;
            case BasicCharacters.Jock:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameMasculine();
                dialog1a.NPCDialog = "Eeeeeey ballin' threads brah!!";
                dialog2a.NPCDialog = "Didja catch the game last night?! Saawwweeeeeet!";
                dialog3a.NPCDialog = "Look at all these <b>GIIIIRLS</b> duuuude!";
                dialog4a.NPCDialog = "CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG!!!!";
                dialog5a.NPCDialog = "Breath check. One. Two. Awhh yeah I am HAWT.";
                break;
            case BasicCharacters.PlaidMan:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameMasculine();
                dialog1a.NPCDialog = "<i>Adjusts mustache</i>";
                dialog2a.NPCDialog = "I have thirty-five shirts. All of them plaid. All of them different colors.";
                dialog3a.NPCDialog = "My plaid game is top.";
                dialog4a.NPCDialog = "I've heard that the devil wears plaid.";
                dialog5a.NPCDialog = "Don't get mad. Get plaid.";
                break;
            case BasicCharacters.PoliceMan:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameMasculine();
                dialog1a.NPCDialog = "STOP RIGHT THERE CRIMINAL SCUM! Just kidding. I'm off duty right now.";
                dialog2a.NPCDialog = "I can't really compete with these bouncers here. But my taser can.";
                dialog3a.NPCDialog = "I've been on the force for over forty years now. It can be rough sometimes.";
                dialog4a.NPCDialog = "It's good to take a break and have a drink with friends every once in a while.";
                dialog5a.NPCDialog = "These are some good drinks, must have been expensive.";
                break;
            case BasicCharacters.Shenheizzer:
                conversationInformation.GetComponent<Interactable>().InteractableName = "The Shenheizzer";
                dialog1a.NPCDialog = "Dr-dr-dr-dr-DROP the <b>BASS</b>";
                dialog2a.NPCDialog = "Awwwwhaha yeah man these beats are HOT!";
                dialog3a.NPCDialog = "The Shenheizzer doesn't like to brag.... but the Shenheizzer is absolutely KILLING IT RIGHT NOW.";
                dialog4a.NPCDialog = "Why is the Shenheizzer THE Shenheizzer?! Why is the bass so PUMPIN'???";
                dialog5a.NPCDialog = "The Shenheizzer's favorite season is spring. I LOVE those adorable baby birds.";
                break;
        }

        conversationInformation.GetComponent<Interactable>().Action = "Talk to";
    }

    private void SetupTexture()
    {
        Material mat;
        string matName = "";
        switch (CharacterType)
        {
            case BasicCharacters.Bartender:
                matName = "CharBasic_Bartender";
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
            case BasicCharacters.GingerMan:
                matName = "CharBasic_Ginger";
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
            case BasicCharacters.Jock:
                matName = "CharBasic_Jock";
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
            case BasicCharacters.PlaidMan:
                matName = "CharBasic_PlaidShirt";
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
            case BasicCharacters.PoliceMan:
                matName = "CharBasic_PoliceMan";
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
            case BasicCharacters.Shenheizzer:
                matName = "CharBasic_Schenheizzer";
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
	
	void Update () 
    {
	    
	}
}
