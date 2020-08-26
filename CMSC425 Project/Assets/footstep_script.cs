
using UnityEngine;

public class footstep_script : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] steps;
    [SerializeField]
    private AudioClip[] jumps;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip step = GetRandomStep();
        audioSource.PlayOneShot(step);
    }

    private void Jump()
    {
        AudioClip jump = GetRandomJump();
        audioSource.PlayOneShot(jump);
    }


    private AudioClip GetRandomStep()
    {
        return steps[UnityEngine.Random.Range(0, steps.Length)];
    }

    private AudioClip GetRandomJump()
    {
        return jumps[UnityEngine.Random.Range(0, jumps.Length)];
    }
}
