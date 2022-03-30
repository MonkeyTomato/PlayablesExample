using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PauseSubGraphAnimationSample : MonoBehaviour
{
    public Animator animator;
    public AnimationClip clip0;
    public AnimationClip clip1;

    PlayableGraph playableGraph;
    AnimationMixerPlayable mixerPlayable;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        playableGraph = PlayableGraph.Create("PauseSubGraphAnimation");

        AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(playableGraph, "AnimationPlayableOutput", animator);
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        playableOutput.SetSourcePlayable(mixerPlayable);

        AnimationClipPlayable clipPlayable0 = AnimationClipPlayable.Create(playableGraph, clip0);
        AnimationClipPlayable clipPlayable1 = AnimationClipPlayable.Create(playableGraph, clip1);

        playableGraph.Connect(clipPlayable0, 0, mixerPlayable, 0);
        playableGraph.Connect(clipPlayable1, 0, mixerPlayable, 1);

        mixerPlayable.SetInputWeight(0, 1.0f);
        mixerPlayable.SetInputWeight(1, 1.0f);

        // clipPlayable1.SetPlayState(PlayState.Paused);
        clipPlayable1.Pause();  // SetPlayState方法已经被弃用了，可以直接使用Pause

        playableGraph.Play();
    }


    void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
