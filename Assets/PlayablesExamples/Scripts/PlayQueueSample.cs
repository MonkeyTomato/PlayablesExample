using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class PlayQueueSample : MonoBehaviour
{
    public Animator animator;
    public AnimationClip[] clipsToPlay;
    PlayableGraph playableGraph;

    void Start()
    {
        playableGraph = PlayableGraph.Create("PlayQueue");
        var playQueuePlayable = ScriptPlayable<PlayQueuePlayable>.Create(playableGraph);
        var playQueueBehaviour = playQueuePlayable.GetBehaviour();
        playQueueBehaviour.Initialize(clipsToPlay, playQueuePlayable, playableGraph);

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Output", animator);

        playableOutput.SetSourcePlayable(playQueuePlayable);
        playableOutput.SetSourceOutputPort(0);

        playableGraph.Play();
    }

    void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
