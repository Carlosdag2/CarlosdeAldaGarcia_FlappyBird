using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipecontroller : MonoBehaviour
{
    //Algo interesante que nos permite hacer Unity es en el momento de la declaracion de variables publicas
    //Al ser publica, automaticamente es visible y editable desde el editor

    public float speed;
    pajarocontroller pajaro;
    //esto nos permite prototipar rapidisimo


    //Como el Start, se hace una vez pero antes que todos los Start (en el momento de instanciacion)
    //Lo solemos usar para inicializar cosas
    private void Awake()
    {

    }

    //Se llama una vez justo antes del primer frame
    void Start()
    {
        pajaro = GameObject.Find("Pajaro").GetComponent<pajarocontroller>();
    }

    //Se llama todos y cada uno de los frames
    //Lo que vamos a hacer es desplazar el objeto a lo largo del eje necesario y cuando alcance cierta
    //coordenada lo volvemos a reposicionar en su punto inicial
    void Update()
    {

        /*
         Si os fijais, el tubo sale de pantalla desde el 11 al -11
        y para el desplazamiento el componente que estamos modificando desde editor es el Transform
        por lo que debemos hacer lo mismo por codigo
         */

        //Para mover/rotar/escalar un objeto accedemos a su transform

        //por defecto podemos acceder al trabsform de un objeto con la palabra "transform"
        //con el operador de acceso '.' accedemos a su contenido, una funcion que nos permite desplazar seria Translate()
        //La cual tiene multiples implementaciones, podemos usar la que mas os guste, el minimo necesario es darle un eje sobre el que desplazarse
        //Si os fijais yo quiero mover mi tuberia en el eje X
        //                  X   ,   Y,      Z
        // Una unidad en unity es 1m -> le estamos diciendo al GameObject que contenga este scritp que se mueva
        //1m en x, 0m en y , 0m en z


        //transform.Translate(1.0f, 0.0f, 0.0f);


        //Un problema grave de esto es el numero de llamadas de Update() -> 1 vez por frame
        //si me fijo en la pestañita de Stats en el panel de Game voy a una media de 170 fps
        //por lo que estoy desplazando el objeto 170m por segundo

        //Para solucionar esto debemos usar una propiedad de Time, llamada deltaTime

        //si poneis el raton encima, os la describe como el intervalo en segundos entre frame y frame
        //Time.deltaTime
        //Si un pc va a 1 fps -> ¿en un segundo cuantos metros se ha desplazado un objeto con este script?
        //1 metro

        //Cuanto es ¿el intervalo en segundos entre frame y frame? de este ordenador
        // Time.deltaTime = 1 segundo

        //Si un pc va a 20 fps -> ¿en un segundo cuantos metros se ha desplazado un objeto con este script?
        //20 metros

        //Cuanto es ¿el intervalo en segundos entre frame y frame? de este ordenador
        //Time.deltaTime = 1/20 -> 0.05 segundos

        //la idea es ralentizar el desplazamiento de los ordenadores mas potentes para que la ejecucion en ambos sistemas sea la misma pasado el mismo tiempo

        //por lo que si multiplicamos esto mismo por Time.deltaTime,
        //Un pc que sea una patata como el de 1 fps, es una multiplicacion por 1
        //en cambio el que va a 20, es una multiplicacion por 0.05
        //en el mismo tiempo ambos pc tendran el objeto en el mismo sitio
        if (!pajaro.is_dead)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);


            //ahora que ya sabemos desplazar un objeto y normalizar su desplazamiento en funcion de la arquitectura
            //vamos a comprobar su posicion

            //preguntadle al transform su posicion en el eje adecuado
            if (transform.position.x < -6.0f)
            {
                //podemos darle una nueva posicion a mano
                //como queremos que la altura vaya variando cada vez que reposicionemos el tubo, podemos usar
                //un clasico objeto Random
                //En Unity lo tenemos estatico, por lo que no hace falta ni crear el objeto
                //Simplemente llamaremos a la funcion
                //Range(minInclusivo, maxInclusivo)
                transform.position = new Vector3(55.0f, Random.Range(-17.1f, -1.3f), 3.18f);
                //TODO:
                //Algo que podríais hacer es cambiar los valores hardcoded 11, -5, 5, etc... por variables
                //parecido a lo que hemos hecho con "speed" en el Translate()
            }
        }

        //todos y cada uno de los frames desplazará el objeto y preguntara por su posicion en x
        //Como quiero que vaya a negativos tendre que decirle que se desplaza en -1



    }
}
