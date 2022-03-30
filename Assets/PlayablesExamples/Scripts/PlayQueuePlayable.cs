using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class PlayQueuePlayable : PlayableBehaviour
{
    int currentClipIndex = -1;
    float timeToNextClip;
    Playable mixer;

    public void Initialize(AnimationClip[] clipsToPlay, Playable owner, PlayableGraph graph)
    {
        //用动画混合AnimationMixerPlayable作为输入，所以只有1个输入
        owner.SetInputCount(1);
        int clipCount = clipsToPlay.Length;
        mixer = AnimationMixerPlayable.Create(graph, clipCount);
        graph.Connect(mixer, 0, owner, 0);
        owner.SetInputWeight(0, 1);

        // 将所有的动画Clip创建对应的Playable
        for (int clipIndex = 0; clipIndex < clipCount; clipIndex++)
        {
            var clipPlayable = AnimationClipPlayable.Create(graph, clipsToPlay[clipIndex]);
            graph.Connect(clipPlayable, 0, mixer, clipIndex);
            mixer.SetInputWeight(clipIndex, 1.0f);
        }
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        int clipCount = mixer.GetInputCount();
        if (clipCount == 0)
            return;

        timeToNextClip -= info.deltaTime;
        if (timeToNextClip <= 0.0f)
        {
            currentClipIndex++;
            if (currentClipIndex >= clipCount)
            {
                currentClipIndex = 0;
            }
            var currentClip = (AnimationClipPlayable)mixer.GetInput(currentClipIndex);
            currentClip.SetTime(0);
            timeToNextClip = currentClip.GetAnimationClip().length;
        }

        for (int clipIndex = 0; clipIndex < clipCount; clipIndex++)
        {
            // 让当前正在播的动画的权重为1，其余全部为0，以此达到只播一个的效果
            if (clipIndex == currentClipIndex)
                mixer.SetInputWeight(clipIndex, 1.0f);  
            else
                mixer.SetInputWeight(clipIndex, 0.0f);
        }
    }
}
