using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class ClickCommand : ICommand
// {
//     private GameObject target;

//     public ClickCommand(GameObject target)
//     {
//         this.target = target;
//     }

//     public void Execute()
//     {
//         // Lógica para realizar un clic en el objeto "target"
//         Debug.Log("Clickeando el objeto: " + target.name);
//     }
// }

// Comando concreto para agarrar/soltar
public class GrabReleaseCommand : MonoBehaviour, ICommand
{
    [SerializeField] private PlayerController invoker;

    public GrabReleaseCommand(PlayerController invoker)
    {
        this.invoker = invoker;
    }

    public void Execute()
    {
        if (!invoker.HasGrabbedSomething())
        {
            // Lógica para agarrar el objeto
            Debug.Log("Agarrando un objeto: ");
        }
        else
        {
            // Lógica para soltar el objeto
            Debug.Log("Soltando el objeto: ");
        }
    }
}

// Comando concreto para empujar
// public class PushCommand : ICommand
// {
//     private GameObject target;
//     private Vector3 direction;

//     public PushCommand(GameObject target, Vector3 direction)
//     {
//         this.target = target;
//         this.direction = direction;
//     }

//     public void Execute()
//     {
//         // Lógica para empujar el objeto "target" en una dirección específica
//         Debug.Log("Empujando el objeto: " + target.name + " en dirección: " + direction);
//     }
// }

// // Comando concreto para girar
// public class RotateCommand : ICommand
// {
//     private GameObject target;
//     private Vector3 rotation;

//     public RotateCommand(GameObject target, Vector3 rotation)
//     {
//         this.target = target;
//         this.rotation = rotation;
//     }

//     public void Execute()
//     {
//         // Lógica para girar el objeto "target" con una rotación específica
//         Debug.Log("Rotando el objeto: " + target.name + " con rotación: " + rotation);
//     }
// }


