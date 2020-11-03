using UnityEngine;

public class footstep : MonoBehaviour
{
    
        public AudioClip Feet_Dirt;
        public AudioClip Feet_Grass;
        public AudioClip jumpSound;
    public AudioClip pickupSound;
    private AudioClip stepSound;

    

        private AudioSource source;


        // Start is called before the first frame update
        void Start()
        {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        stepSound = Feet_Grass;
        }

        void OnTriggerEnter(Collider area)
        {

        if (area.gameObject.tag == "Dirt")
      {
            stepSound = Feet_Dirt;
        }

        if (area.gameObject.tag == "Grass")
        {
            stepSound = Feet_Grass;
        }

   }


        // Update is called once per frame
        void Update()
        {
     

        }
    public void Step()
    {
        if (source.isPlaying != true)
        {
            Debug.Log("play sound");
            source.PlayOneShot(stepSound);
        }
    }
    public void Jump()
    {
        if (source.isPlaying != true)
        {
            Debug.Log("play jump");
            source.PlayOneShot(jumpSound);
        }
    }
    public void Pickup()
    {
        if (source.isPlaying != true)
        {
            Debug.Log("play pickup");
            source.PlayOneShot(pickupSound);
        }
    }

}