using System.Collections;
using System.Collections.Generic;
using TouchDevUltimate.Gameplay.Characters;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shootSpeed = 1f;

    private bool shooting;

    private Character _character;
    private CharacterWeaponUser _user;

    public void SetUser(Character userCharacter)
    {
        _character = userCharacter;
        _user = _character.GetComponent<CharacterWeaponUser>();
    }

    private void Update()
    {
        //_character.model.SetFloat("ShootSpeed", shootSpeed);
    }

    public void ShootRequest()
    {
        //_character.model.SetTrigger("ShootRequest");
    }

    #region Animation functions
    public void ShootStart()
    {
        shooting = true;
        //_character.model.SetBool("Shooting", shooting);

        _user.weaponModel.ScaleTween(new Vector3(1, 0.8f, 1), 0.2f, LeanTweenType.easeInSine);
    }

    public void Shoot()
    {
        StartCoroutine(ShotDelay(1.5f / (shootSpeed * 10f)));
        
        // Tweening
        StartCoroutine(ShotAnimation(0.1f));
    }

    private IEnumerator ShotAnimation(float delay)
    {
        _user.weaponModel.ScaleTween(new Vector3(1, 1.6f, 1), delay, LeanTweenType.easeOutCirc);
        yield return new WaitForSeconds(delay);
        _user.weaponModel.ScaleTween(new Vector3(1.1f, 0.9f, 1), delay, LeanTweenType.easeOutCirc);
        yield return new WaitForSeconds(delay);
        _user.weaponModel.ScaleTween(new Vector3(1, 1.2f, 1), delay, LeanTweenType.easeOutCirc);
    }

    public IEnumerator ShotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //_character.model.SetTrigger("Shot");
    }

    public void ShootEnd()
    {
        shooting = false;
        //_character.model.SetBool("Shooting", shooting);
        //_character.model.SetTrigger("ShootEnd");
        //_character.model.ResetTrigger("Shot");

        _user.weaponModel.ScaleTween(Vector3.one, 0.1f, LeanTweenType.easeOutCirc);
    }

    
    #endregion
}
