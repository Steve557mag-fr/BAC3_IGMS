using COL1.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] GameObject visualInteractGroup, pauseContainer;
    [SerializeField] Image reticle;
    [SerializeField] float minReticle, maxReticle;

    float reticleGrowFactor;

    private void Start()
    {
        UpdateInteract(false);
    }

    private void Update()
    {
        reticle.GetComponent<RectTransform>().localScale = new(reticleGrowFactor, reticleGrowFactor, reticleGrowFactor);
    }

    public void UpdateReticle(bool grow=false)
    {
        reticleGrowFactor = grow ? maxReticle : minReticle;

    }

    public void UpdateInteract(bool underInteraction = false)
    {
        UpdateReticle(underInteraction);
        visualInteractGroup.SetActive(underInteraction);
    }

    public void SetPause(bool pauseMode)
    {
        pauseContainer.SetActive(pauseMode);
    }

    public void backMenu()
    {
        Singleton.Get<Game>().Goto("Menu");
        Destroy(Singleton.Get<PlayerController>().gameObject);
    }

}
