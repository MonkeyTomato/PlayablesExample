using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class PlayWithTimeControlSample : MonoBehaviour
{
    public Animator animator;
    public AnimationClip clip;
    public float time;
    PlayableGraph playableGraph;
    AnimationClipPlayable clipPlayable;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        playableGraph = PlayableGraph.Create("PlayWithTimeControl");

        AnimationPlayableOutput output = AnimationPlayableOutput.Create(playableGraph, "Output", animator);
        clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);

        output.SetSourcePlayable(clipPlayable);

        playableGraph.Play();

        clipPlayable.Pause();
    }

    void Update()
    {
        clipPlayable.SetTime(time);
    }

    void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
