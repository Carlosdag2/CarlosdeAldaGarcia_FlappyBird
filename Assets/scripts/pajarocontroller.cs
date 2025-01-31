using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Usamos TextMeshPro para el texto

public class pajarocontroller : MonoBehaviour
{
    public Rigidbody rb;
    public float gravity_force;
    public float jump_force;
    public TextMeshProUGUI scoreText; // Texto para mostrar la puntuación
    public TextMeshProUGUI highScoreText; // Texto para mostrar la puntuación más alta
    public GameObject gameOverPanel; // Panel que se muestra cuando el juego termina
    public Button restartButton; // Botón para reiniciar
    public Button exitButton; // Botón para salir
    public Button newLevelButton; // Botón para nuevo nivel
    public AudioManager audioManager;

    private int score = 0;
    private int highScore = 0;
    public bool is_dead;

    // Start is called antes del primer frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        is_dead = false;
        restartButton.onClick.AddListener(RestartGame); // Al presionar Restart, reinicia el juego
        exitButton.onClick.AddListener(ExitGame); // Al presionar Exit, sale del juego
        newLevelButton.onClick.AddListener(NewLevel); // Al presionar New Level, carga un nuevo nivel
        gameOverPanel.SetActive(false); // Desactiva el panel de Game Over al inicio
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Cargar el record
        UpdateScore();
    }

    // Update se llama una vez por frame
    void Update()
    {
        if (!is_dead)
        {
            rb.AddForce(Vector3.down * gravity_force, ForceMode.Force);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jump_force, ForceMode.VelocityChange);
            }
        }
    }

    // Detectar colisiones
    void OnCollisionEnter(Collision collision)
    {
        // Mostrar el menú de pausa al colisionar con un obstáculo
        if (collision.gameObject.CompareTag("obstaculo"))
        {
            is_dead = true;
            ShowGameOver(); // Mostrar el menú de pausa
        }
    }

    // Detectar colisiones con triggers
    void OnTriggerEnter(Collider trigger)
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // Guardar el record
        }
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Puntuación: " + score;
        highScoreText.text = "Puntuación más alta: " + highScore;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true); // Mostrar el panel de game over
    }

    public void RestartGame()
    {
        score = 0; // Reiniciar la puntuación
        UpdateScore();
        gameOverPanel.SetActive(false); // Ocultar el panel de game over
        Time.timeScale = 1; // Reanudar el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar la escena
        audioManager.LoadSettings(); // Cargar ajustes de audio
    }

    public void NewLevel()
    {
        score = 0; // Reiniciar la puntuación
        highScore = 0;
        PlayerPrefs.SetInt("HighScore", highScore); // Guardar el record
        UpdateScore();
        gameOverPanel.SetActive(false); // Ocultar el panel de game over
        Time.timeScale = 1; // Reanudar el juego

        audioManager.ResetToDefault(); // Restablecer ajustes de audio

        // Cargar un nuevo nivel (asumiendo que los niveles están numerados secuencialmente)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit(); // Salir del juego
    }
}
