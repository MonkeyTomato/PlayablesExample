using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayAnimationUtilitiesSample : MonoBehaviour
{
    public Animator animator;
    public AnimationClip clip;
    PlayableGraph playableGraph;

    void Start()
    {
        if(animator==null)
            animator = GetComponent<Animator>();

        // playableGraph = PlayableGraph.Create("PlayAnimationUtilitesSample");
        // 这个方法会产生一个新的playableGraph，所以我们无需提前创建，创建后会导致存在两个Graph，可以通过Visualizer看出来
        AnimationPlayableUtilities.PlayClip(animator, clip, out playableGraph);
    }

    void OnDestroy()
    {
        playableGraph.Destroy();
    }
}
