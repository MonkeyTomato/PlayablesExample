using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

/// <summary>
/// 混合动画片段和Controller
/// </summary>
[RequireComponent(typeof(Animator))]
public class RuntimeControllerSample : MonoBehaviour
{
    public Animator animator;
    public AnimationClip clip;
    public RuntimeAnimatorController controller;
    public float weight;
    PlayableGraph playableGraph;
    AnimationMixerPlayable mixerPlayable;

    void Start()
    {
        if(animator==null)
            animator = GetComponent<Animator>();
        
        playableGraph = PlayableGraph.Create("RuntimeControllerSample");
        AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(playableGraph, "AnimationOutput", animator);
        mixerPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        playableOutput.SetSourcePlayable(mixerPlayable);

        AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
        AnimatorControllerPlayable controllerPlayable = AnimatorControllerPlayable.Create(playableGraph, controller);
        playableGraph.Connect(clipPlayable, 0, mixerPlayable, 0);
        playableGraph.Connect(controllerPlayable, 0, mixerPlayable, 1);

        playableGraph.Play();
    }

    void Update()
    {
        weight = Mathf.Clamp01(weight);
        mixerPlayable.SetInputWeight(0, 1.0f - weight);
        mixerPlayable.SetInputWeight(1, weight);
    }

    void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
