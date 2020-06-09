using UnityEngine;

public class HitEnimies : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Matei o Inimigo");
            Player player = collision.gameObject.GetComponent<Player>();
            player.SendMessage("KillFeedback", SendMessageOptions.DontRequireReceiver);
            anim.SetBool("hit", true);
        }
    }
}