using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painthit : MonoBehaviour
{

    public Transform reticle, spine;
    RaycastHit rayhit;
    public GameObject paintDecal,Gun;
    CinemachineImpulseSource impSource;

    IEnumerator ShootPaint()
    {
        while (true)
        {

            if (GameManager.instance.LevelFail && !GameManager.instance.LevelComplete)
            {
                GameManager.instance.GameFailed();
                reticle.gameObject.SetActive(false);
                Gun.SetActive(false);
                transform.DORotate(new Vector3(0,-165.95f,0),0.2f);
                GetComponentInChildren<Animator>().Play("Crying");
                yield break;
            }

            if (GameManager.instance.LevelComplete && !GameManager.instance.LevelFail)
            {
                GameManager.instance.GameComplete();
                reticle.gameObject.SetActive(false);
                Gun.SetActive(false);
                GetComponentInChildren<Animator>().Play("Dancing");
                yield break;
            }
            else
            {
                GameManager.instance.onGameStart -= Shoot;
                spine.LookAt(reticle.position);
                if (Physics.Raycast(reticle.position, reticle.TransformDirection(Vector3.forward), out rayhit, 200f))
                {
                    Debug.DrawLine(reticle.position, rayhit.point, Color.red);

                    if (rayhit.collider.CompareTag("Mask"))
                    {
                        UIManager.instance.scoreNo+=5;
                        UIManager.instance.SetScore();
                        GetComponentInChildren<Animator>().Play("Shoot");
                        AudioManager.instance.Play("PaintSplash");
                        impSource.GenerateImpulse();
                        GameObject paintSplatter = Instantiate(paintDecal, rayhit.point, Quaternion.identity);

                        yield return new WaitForSeconds(0.1f);
                        rayhit.collider.GetComponent<SpriteMask>().enabled = true;
                        rayhit.collider.GetComponent<BoxCollider>().enabled = false;

                        paintSplatter.GetComponent<SpriteRenderer>().DOFade(0, 1f).OnComplete
                        (
                            () =>
                            {
                                Destroy(paintSplatter);
                            }
                        );

                    }

                    if (rayhit.collider.CompareTag("Enemy") && !rayhit.collider.GetComponent<EnemyMovement>().dead)
                    {
                        UIManager.instance.scoreNo+=5;
                        UIManager.instance.SetScore();
                        GetComponentInChildren<Animator>().Play("Shoot");
                        AudioManager.instance.Play("HitEnemy");
                        GameManager.instance.numberOfKilled++;
                        impSource.GenerateImpulse();
                        rayhit.collider.GetComponent<EnemyMovement>().dead = true;
                    }
                    GetComponentInChildren<Animator>().Play("ShootPose");
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        

    }

    public void Shoot()
    {
        transform.LookAt(impSource.transform.forward);
        reticle.gameObject.SetActive(true);
        StartCoroutine(ShootPaint());
    }

    void Start()
    {
        impSource = reticle.GetComponent<CinemachineImpulseSource>();
        GameManager.instance.onGameStart += Shoot;
       
    }
}
