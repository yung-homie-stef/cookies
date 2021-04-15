using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMT_Table : Interactable
{
    public Animator pitchBlackAnimator;
    public GameObject technician;
    public GameObject circlet;
    public GameObject DMT;

    public Cult_Leader leader;

    public override void InteractAction()
    {
        pitchBlackAnimator.SetBool("faded", true);
        StartCoroutine(UnfadeBlack(4.0f));
        gameObject.tag = "Untagged";
    }

    private IEnumerator UnfadeBlack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DMT.SetActive(true);
        technician.SetActive(false);
        circlet.SetActive(true);
        pitchBlackAnimator.SetBool("faded", false);

        leader._dialogueValue++;
    }
}
