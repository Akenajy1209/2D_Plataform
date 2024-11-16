using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using TMPro;

public class TestSuites
{
    [UnityTest]
    public IEnumerator Test_StartButtonClick()
    {
        SceneManager.LoadScene("Menu");
        yield return null;
        // Verifica que la escena actual sea la del menú
        Assert.AreEqual("Menu", SceneManager.GetActiveScene().name, "La escena no es la del menú.");
        // Inicia la simulación del clic en el botón Start
        yield return new WaitForSeconds(1f);

        // Busca el botón Start en la escena (asegúrate de que el nombre sea correcto)
        GameObject startButton = GameObject.Find("Play");

        // Verifica si el botón está presente en la escena
        if (startButton != null)
        {
            Button button = startButton.GetComponent<Button>();
            if (button != null)
            {
                // Simula el clic en el botón Start
                button.onClick.Invoke();
                Debug.Log("Se ha simulado un clic en el botón Start.");
            }
            else
            {
                Debug.LogError("El objeto StartButton no tiene un componente Button.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el botón Start.");
        }

        // Espera un momento para que la escena cambie
        yield return new WaitForSeconds(1f);

        // Verifica que la escena haya cambiado a la escena del juego
        Assert.AreEqual("Level", SceneManager.GetActiveScene().name, "La escena no se cambió a 'Level'.");

    }
    [UnityTest]
    public IEnumerator TestPlayerDeathDeactivation()
    {
        // Cargar la escena "Level"
        SceneManager.LoadScene("Level");

        yield return new WaitForSeconds(3f);
        GameObject player = GameObject.Find("Player");
        // Posicionamos al jugador debajo del mapa (fuera de la vista)
        player.transform.position = new Vector3(-50f, -50f, -50f);

        // Simulamos que el jugador muere llamando al método Death() de GameManager
        yield return new WaitForSeconds(1f);
        GameManager.instance.Death();
        // Esperar un momento para que se complete el proceso de muerte

        // Verificar si el objeto jugador se ha desactivado (GameObject.SetActive(false))
        Assert.IsFalse(player.activeSelf, "El jugador no se desactivó después de la muerte.");
    }
    [UnityTest]
    public IEnumerator TestElementText()
    {
        // Cargar la escena "Level"
        SceneManager.LoadScene("Level");

        // Esperamos 3 segundos para que la escena cargue completamente
        yield return new WaitForSeconds(3f);

        // Buscar el objeto de texto en la jerarquía por su ruta
        GameObject scorePanelObject = GameObject.Find("Canvas/GameUI/CoinPickupText");

        // Verificar que el objeto fue encontrado y que es un TMP_Text
        TMP_Text scoreText = scorePanelObject.GetComponent<TMP_Text>();
        Assert.IsNotNull(scoreText, "El texto no se encontró en el objeto.");

        // Verificar que el texto del objeto es "0"
        Assert.AreEqual("0", scoreText.text, "El texto del elemento no es correcto.");
    }
}