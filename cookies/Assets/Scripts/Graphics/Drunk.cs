using UnityEngine;
using System.Collections;

public class Drunk : MonoBehaviour
{
    public Material material;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    public void BeginSoberCountdown()
    {
        StartCoroutine(SoberUp(10.0f));
    }

    private IEnumerator SoberUp(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        this.enabled = false;
       
    }
   
}