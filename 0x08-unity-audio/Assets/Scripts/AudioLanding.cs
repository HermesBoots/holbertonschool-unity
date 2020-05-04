using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLanding : StateMachineBehaviour
{
    /// <summary>The audio clips used when running.</summary>
    public AudioClip[] clips;

    /// <summary>Mixer used to filter sound.</summary>
    public AudioMixerGroup mixer;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GameObject player = GameObject.Find("Player");
        RaycastHit cast;
        MeshRenderer model;
        AudioClip clip = null;

        Physics.Raycast(player.transform.position, new Vector3(0, -1, 0), out cast);
        if (cast.collider) {
            model = cast.collider.gameObject.GetComponentInChildren<MeshRenderer>();
            if (model) {
                if (Enumerable.Contains(model.materials.Select((mat) => { return mat.name; }), "Green_leafs (Instance)"))
                    clip = this.clips[0];
                else
                    clip = this.clips[1];
            }
        }
        if (clip) {
            AudioSource audio = player.GetComponent<AudioSource>();
            audio.clip = clip;
            audio.outputAudioMixerGroup = this.mixer;
            audio.Play();
        }
    }
}
