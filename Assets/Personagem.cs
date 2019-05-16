using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{

	public float distancia = 3f;

	Vector3 destino;
	public float velocidade = 0.015f;
	public float velocidadeRotacao = 2f;

	void Start()
	{
		// No começo do jogo, o personagem ficará parado
		destino = transform.position;
	}

	void Update()
	{
		AtualizarPosicaoDestino();

		Rotacionar();

		Movimentar();
	}

	private void Rotacionar()
	{
		var rotacaoDestino = Quaternion.LookRotation(destino);
		var rotacao = Quaternion.Slerp(
			transform.rotation,
			rotacaoDestino,
			velocidadeRotacao * Time.deltaTime
		);

		rotacao.eulerAngles = new Vector3(
			0f,
			rotacao.eulerAngles.y,
			0f
		);

		transform.rotation = rotacao;
	}

	private void Movimentar()
	{
		transform.Translate(
			Vector3.forward * velocidade * Time.deltaTime
		);
	}

	private void AtualizarPosicaoDestino()
	{
		if (Input.GetMouseButton(0))
		{
			var mousePosition = Input.mousePosition;

			Ray ray = Camera.main.ScreenPointToRay(mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, distancia))
			{
				// Ray tocou em algum ponto
				destino = hit.point;
				destino.y = transform.position.y;
			}
		}
	}
}