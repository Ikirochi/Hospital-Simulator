using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IIInteractable {

    [SerializeField] private string interactText;
    // Removed the static responseText field
    public PatientAI patientAI;
    private bool isInteracting = false;

    private Animator animator;
    private NPCHeadLookAt npcHeadLookAt;

    private void Awake() {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform interactorTransform) {

        if (isInteracting) return;
        isInteracting = true;
        Debug.Log("Interacting with NPC");

        // Calculate the direction to look at by subtracting the current position from the target's position.
        Vector3 directionToLookAt = interactorTransform.position - transform.position;
        // Make sure the rotation only affects the y-axis by zeroing out the x and z components.
        directionToLookAt.y = 0;

        // Create a rotation that looks along the calculated direction, keeping the up vector pointing upwards.
        Quaternion targetRotation = Quaternion.LookRotation(directionToLookAt);

        // Apply the rotation to the NPC instantly. For smoother rotation, you could use Quaternion.Slerp in the Update method.
        transform.rotation = targetRotation;

        npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * 1.6f);

        // Determine the response based on the minigame
        string responseText = DetermineResponseText();
        
        // based on the length of the response text, adjust the chat bubble position so that it centers above the NPC
        Vector3 bubblePosition = responseText.Length > 30 ? new Vector3(1.5f, 2.1f, 0) : new Vector3(0.5f, 2.1f, 0);

        ChatBubble3D.Create(transform.transform, bubblePosition, ChatBubble3D.IconType.Neutral, responseText);
        animator.SetTrigger("Talk");
        StartCoroutine(ResetInteraction());

    }

    private IEnumerator ResetInteraction() {
    yield return new WaitForSeconds(6f); // Wait for 1 second or the time that fits your game flow
    isInteracting = false;
    }

    private string DetermineResponseText() {

        if (patientAI.currentState == PatientState.InExamRoom || patientAI.currentState == PatientState.PlayingMinigame) {
            // Existing conditions for minigame responses
            if (patientAI.selectedMinigameIndex == 0) { // Spelling Minigame

               List<string> responses = new List<string>() {
                "I sneezed so much my nose fell off",
                "My mom said I eat like a dog",
                "I think my stomach has left my body",
                "Is it normal to hear screams when I blink",
                "My knees glow in the dark",
                "I can talk to the sun",
                "My belly button is whispering secrets to me at night",
                "My throat feels like a cheese grater",
                "I'm sweating so much from my feet",
                "My brain is on fire!",
                "I've been hiccupping for a week straight"
            };

                
                // Randomly select a response
                int index = Random.Range(0, responses.Count);
                patientAI.StartSelectedMinigame();
                patientAI.currentState = PatientState.PlayingMinigame;
                return responses[index];
                
            }
            
            if (patientAI.selectedMinigameIndex == 1) { // Fetch Minigame

            List<string> responses = new List<string>() {
                "I'm sick I swear, bring me the ",
                "I'm feeling really sad, I need a ",
                "Sometimes I can't focus, I definitely need a ",
                "I'm feeling tired, I need the ",
                "I really need the ",
                "Big party this weekend, I need a ",
                "I'm so bored, I need a ",
                "My face burns, I need the ",
                "I'm here to pick up a perscription for the ",
                "I heard pills are really great, get me the ",
                "I'll slip you a 20 if you get me the "
            };

                patientAI.StartSelectedMinigame();
                patientAI.currentState = PatientState.PlayingMinigame;
                int index = Random.Range(0, responses.Count);
                return responses[index] + patientAI.desiredPill;
                //return "Bring me the " 
            }

            else if (patientAI.selectedMinigameIndex == 2 || patientAI.selectedMinigameIndex == 4) { // Arrow Input Minigame or Vaccine Minigame
            List<string> responses = new List<string>() {
                "I hope you're not as nervous as I am",
                "Is this gonna give me superpowers?",
                "Can we do this tomorrow? I forgot to set my DVR",
                "Make sure you put everything back where you found it",
                "Tell my family I love them... and to clear my browser history",
                "Any chance we can get this done in time for happy hour?",
                "Do my bones look weird to you?",
                "Can this get me out of jury duty?",
                "Is this gonna hurt? I'm not good with pain",
                "I checked WebMD and I think I just need a Band-Aid.",
                "Want to grab dinner after this?"
            };

            int index = Random.Range(0, responses.Count);

            patientAI.StartSelectedMinigame();
            patientAI.currentState = PatientState.PlayingMinigame;
            return responses[index];

            }
            else if (patientAI.selectedMinigameIndex == 3) { // Vitals Minigame
            List<string> responses = new List<string>() {
                "Are my vitals okay, or is this a set-up for a prank?",
                "You sure this isn't a lie detector? I swear my blood pressure is innocent!",
                "Let's not make my temperature rise more than my stocks did!",
                "I bet my heart's beating faster than your last high score!",
                "My blood pressure's higher than my last phone bill!",
                "Can we adjust the thermostat, or is it just my temperature?",
                "If you find my heart racing, it’s because it’s trying to escape this gown!",
                "Tell me my heart rate, but only if it's ready for a marathon.",
                "Is my blood boiling, or am I just excited to see a doctor?"
            };

            int index = Random.Range(0, responses.Count);

            patientAI.StartSelectedMinigame();
            patientAI.currentState = PatientState.PlayingMinigame;
            return responses[index];
        }

        }

        else{
            return "I'm not feeling well";
        }
        

        return ""; // Default response if none match
    }

    public string GetInteractText() {
        return interactText;
    }

    public Transform GetTransform() {
        return transform;
    }

}
