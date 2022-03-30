using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

/// <summary>
/// 动画混合
/// </summary>
[RequireComponent(typeof(Animator))]
public class MixAnimationSample : MonoBehaviour
{
    public Animator animator;
    public AnimationClip clip0, clip1;
    public float weight;

    PlayableGraph graph;
    AnimationMixerPlayable mixerPlayable;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        graph = PlayableGraph.Create("MixAnimation");
        var playableOutput = AnimationPlayableOutput.Create(graph, "Animation", animator);
        mixerPlayable = AnimationMixerPlayable.Create(graph, 2);
        playableOutput.SetSourcePlayable(mixerPlayable);

        AnimationClipPlayable clipPlayable0 = AnimationClipPlayable.Create(graph, clip0);
        AnimationClipPlayable clipPlayable1 = AnimationClipPlayable.Create(graph, clip1);
        // 用Graph连接
        graph.Connect(clipPlayable0, 0, mixerPlayable, 0);
        graph.Connect(clipPlayable1, 0, mixerPlayable, 1);
        // 用Playable连接都可以
        // mixerPlayable.ConnectInput(0, clipPlayable0, 0);
        // mixerPlayable.ConnectInput(1, clipPlayable1, 0);

        graph.Play();
    }

    void Update()
    {
        weight = Mathf.Clamp01(weight);
        // 通过设置权重的方式达到混合效果
        mixerPlayable.SetInputWeight(0, 1f - weight);
        mixerPlayable.SetInputWeight(1, weight);
    }

    void OnDestroy()
    {
        graph.Destroy();
    }
}
