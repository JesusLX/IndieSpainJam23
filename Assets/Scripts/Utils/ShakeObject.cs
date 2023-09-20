using DG.Tweening;
using UnityEngine;

public class ShakeObject : MonoBehaviour {
    public Transform recoilTransform;
    public Vector3 recoilDirection = new Vector3(0f, 1f, -1f);
    private Vector3 startPos;
    public float recoilDistance = 0.2f;
    public float recoilDuration = 0.05f;
    private void Start() {
        startPos = recoilTransform.localPosition;
    }
    public void Fire() {
        recoilTransform.DOComplete(); // Cancela cualquier tweener en curso
        recoilTransform.DOLocalMove(recoilTransform.localPosition + recoilDirection * recoilDistance, recoilDuration).SetEase(Ease.OutQuad).OnComplete(ResetRecoilPosition);
    }

    private void ResetRecoilPosition() {
        //recoilTransform.localPosition = startPos;
        recoilTransform.DOLocalMove(startPos, recoilDuration).SetEase(Ease.InQuad);
    }
}
