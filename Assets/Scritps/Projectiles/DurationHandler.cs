using System.Collections;
using UnityEngine;

public class DurationHandler : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private ParticleSystem effect;

    private void OnEnable()
    {
        if(effect)
            effect.Play();
        
        StartCoroutine(DurationCooldownCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator DurationCooldownCoroutine()
    {
        yield return new WaitForSeconds(duration);

        gameObject.SetActive(false);
    }
}
