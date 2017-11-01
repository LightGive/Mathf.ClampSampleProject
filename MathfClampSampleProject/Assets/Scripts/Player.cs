using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	//HPの最大値
	private const float MAX_HP = 10.0f;

	//自機の速度
	[SerializeField]
	private float speed;
	//横の幅
	[SerializeField]
	private float limitWidth;
	//縦の幅
	[SerializeField]
	private float limitHeight;
	//HPバーのGameObject
	[SerializeField]
	private GameObject hpBar;
	//残りHPを表示するテキスト
	[SerializeField]
	private Text textHpValue;

	private float hp = MAX_HP;
	
	void Update ()
	{
		Move();
	}

	void Move()
	{
		//入力を取得
		var hor = Input.GetAxis("Horizontal");
		var ver = Input.GetAxis("Vertical");

		//Mathf.Clampを使った方法
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x + (hor * speed * Time.deltaTime), -limitWidth, limitWidth),
			Mathf.Clamp(transform.position.y + (ver * speed * Time.deltaTime), -limitHeight, limitHeight));


		////Mathf.Clampを使わない場合１
		//transform.position += new Vector3(hor, ver, 0.0f) * speed * Time.deltaTime;
		////画面端から出たら画面内に戻す
		//if (transform.position.x > limitWidth)
		//	transform.position = new Vector3(limitWidth, transform.position.y);
		//else if (transform.position.x < -limitWidth)
		//	transform.position = new Vector3(-limitWidth, transform.position.y);
		//if (transform.position.y > limitHeight)
		//	transform.position = new Vector3(transform.position.x, limitHeight);
		//else if (transform.position.y < -limitHeight)
		//	transform.position = new Vector3(transform.position.x, -limitHeight);



		////Mathf.Clampを使わない場合２
		//var addvec = new Vector3(hor, ver, 0.0f) * speed * Time.deltaTime;
		//var nextPos = transform.position + addvec;
		//if (nextPos.x > limitWidth || nextPos.x < -limitWidth)
		//	addvec.x = 0.0f;
		//if (nextPos.y > limitHeight || nextPos.y < -limitHeight)
		//	addvec.y = 0.0f;
		//transform.position += addvec;
	}

	void OnMouseDown()
	{
		var damage = Random.Range(1.0f, 4.0f);
		Damage(damage);

		Debug.Log("ダメージ！");
	}

	void Damage(float _val)
	{
		hp = Mathf.Clamp(hp - _val, 0.0f, MAX_HP);
		hpBar.transform.localScale = new Vector3(hp / MAX_HP, 1.0f);
		textHpValue.text = hp.ToString("F2");
	}
}
