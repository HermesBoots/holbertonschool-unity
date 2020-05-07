using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioRunning : StateMachineBehaviour
{
    /// <summary>The audio clips used when running.</summary>
    public AudioClip[] clips;

    /// <summary>Mixer used to filter sound.</summary>
    public AudioMixerGroup mixer;

    // how many clips played this animation loop
    private float lastTime = -1;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        this.lastTime = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = GameObject.Find("Player");
        RaycastHit cast;
        MeshRenderer model;
        AudioClip clip = null;

        if (stateInfo.normalizedTime - this.lastTime < 0.5f)
            return;

        Physics.Raycast(player.transform.position, new Vector3(0, -1, 0), out cast);
        if (cast.collider) {
            model = cast.collider.gameObject.GetComponentInChildren<MeshRenderer>();
            if (model) {
            }
        }
        if (!clip) return;

        if (stateInfo.normalizedTime % 1 > 5 / 20) {
            AudioSource audio = player.GetComponent<AudioSource>();
            audio.outputAudioMixerGroup = this.mixer;
            audio.clip = clip;
            audio.Play();
            this.lastTime = stateInfo.normalizedTime;
        }
    }
}
