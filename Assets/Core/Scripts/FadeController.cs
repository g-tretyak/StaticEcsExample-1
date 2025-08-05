using UnityEngine;
using UnityEngine.UI;
using ZeroZeroGames.Ecs.Modules.Shared.Other.Interfaces;
using ZeroZeroGames.Ecs.Modules.Shared.Providers;
using ZeroZeroGames.Ecs.Modules.Shared.World;

public class FadeController : MonoBehaviour, ICanvasProvider
{
    private static readonly int MaskAmount = Shader.PropertyToID("_MaskAmount");
    [SerializeField] private CanvasProvider holder;
    public Image target;

    private void Start()
    {
        var materialInstance = new Material(target.material);
        target.material = materialInstance;
    }


    public GameObject GetObj()
    {
        return gameObject;
    }

    public CanvasProvider Holder => holder;
    public int SortingOrder => Cxt.S.fadeCanvasSortingOrder;

    public void Init()
    {
    }

    public bool CanDestroy => false;

    public void ChangeFade(float lerpVal)
    {
        target.material.SetFloat(MaskAmount, Mathf.Lerp(-1, 1, lerpVal));
    }
}