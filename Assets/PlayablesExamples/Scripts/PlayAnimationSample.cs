using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class PlayAnimationSample : MonoBehaviour
{
    public Animator animator;
    public AnimationClip clip;
    PlayableGraph playableGraph;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        // 创建PlayableGraph
        playableGraph = PlayableGraph.Create("PlayAnimationSample");
        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        // 创建Playable
        AnimationClipPlayable playable = AnimationClipPlayable.Create(playableGraph, clip);
        // 创建PlayableOutput
        AnimationPlayableOutput playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", animator);
        // 链接PlayableOutput和Playable
        playableOutput.SetSourcePlayable(playable);
        playableGraph.Play();
    }

    void OnDestroy()
    {
        // 要记得销毁
        playableGraph.Destroy();
    }
}
