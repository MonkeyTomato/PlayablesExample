using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Audio;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class MultiOutputSample : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AnimationClip animationClip;
    public AudioClip audioClip;

    PlayableGraph playableGraph;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        playableGraph = PlayableGraph.Create("MultiOutputGraph");
        AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(playableGraph, "AnimationPlayableOutput", animator);
        AudioPlayableOutput audioPlayableOutput = AudioPlayableOutput.Create(playableGraph, "AudioPlayableOutput", audioSource);

        AnimationClipPlayable animationClipPlayable = AnimationClipPlayable.Create(playableGraph, animationClip);
        AudioClipPlayable audioClipPlayable = AudioClipPlayable.Create(playableGraph, audioClip, true);

        animationPlayableOutput.SetSourcePlayable(animationClipPlayable);
        audioPlayableOutput.SetSourcePlayable(audioClipPlayable);      

        playableGraph.Play();  
    }

    void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
