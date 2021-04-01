using UnityEngine;

public class footstep : MonoBehaviour
{
    
        public AudioClip Feet_Dirt;
        public AudioClip Feet_Grass;
        public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip pickupSound;
    private AudioClip stepSound;

    

        private AudioSource source;


        // Start is called before the first frame update
        void Start()
        {
        
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
        if (Input.GetKeyDown("space"))
        {
            source.PlayOneShot(jumpSound);
        }
    }
    public void Step()
    {
        
            Debug.Log("play sound");
            source.PlayOneShot(stepSound);
       
    }
    public void Jump()
    {
      
        
    }
    public void Land()
    {

        source.PlayOneShot(landSound);
    }
    public void Pickup()
    {
        
            Debug.Log("play pickup");
            source.PlayOneShot(pickupSound);
        
    }
   

}