using System;
using System.Collections.Generic;

class Carta
{
    public string Nombre { get; set; }
    public string Pinta { get; set; }
    public int Valor { get; set; }

    public Carta(string nombre, string pinta, int valor)
    {
        Nombre = nombre;
        Pinta = pinta;
        Valor = valor;
    }

    public void Print()
    {
        Console.WriteLine($"Carta: {Nombre} de {Pinta}, Valor: {Valor}");
    }
}

class Mazo
{
    public List<Carta> Cartas { get; set; }

    public Mazo()
    {
        Cartas = new List<Carta>();
        // Inicializar el mazo con 52 cartas
        string[] pintas = { "Tréboles", "Picas", "Corazones", "Diamantes" };
        foreach (string pinta in pintas)
        {
            for (int valor = 1; valor <= 13; valor++)
            {
                Carta nuevaCarta = new Carta(GetNombreCarta(valor), pinta, valor);
                Cartas.Add(nuevaCarta);
            }
        }
    }

    private string GetNombreCarta(int valor)
    {
        switch (valor)
        {
            case 1:
                return "As";
            case 11:
                return "J";
            case 12:
                return "Reina";
            case 13:
                return "Rey";
            default:
                return valor.ToString();
        }
    }

    public void Barajar()
    {
        Random random = new Random();
        int n = Cartas.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Carta temp = Cartas[k];
            Cartas[k] = Cartas[n];
            Cartas[n] = temp;
        }
    }

    public Carta Repartir()
    {
        if (Cartas.Count > 0)
        {
            Carta cartaRepartida = Cartas[Cartas.Count - 1];
            Cartas.RemoveAt(Cartas.Count - 1);
            return cartaRepartida;
        }
        else
        {
            Console.WriteLine("No quedan cartas en el mazo.");
            return null;
        }
    }

    public void Reiniciar()
    {
        Cartas.Clear();
        // Volver a crear todas las cartas en el mazo
        string[] pintas = { "Tréboles", "Picas", "Corazones", "Diamantes" };
        foreach (string pinta in pintas)
        {
            for (int valor = 1; valor <= 13; valor++)
            {
                Carta nuevaCarta = new Carta(GetNombreCarta(valor), pinta, valor);
                Cartas.Add(nuevaCarta);
            }
        }
    }
}

class Jugador
{
    public string Nombre { get; set; }
    public List<Carta> Mano { get; set; }

    public Jugador(string nombre)
    {
        Nombre = nombre;
        Mano = new List<Carta>();
    }

    public Carta Robar(Mazo mazo)
    {
        Carta cartaRobada = mazo.Repartir();
        if (cartaRobada != null)
        {
            Mano.Add(cartaRobada);
        }
        return cartaRobada;
    }

    public Carta? Descartar(int indice)
    {
        if (indice >= 0 && indice < Mano.Count)
        {
            Carta cartaDescartada = Mano[indice];
            Mano.RemoveAt(indice);
            return cartaDescartada;
        }
        else
        {
            Console.WriteLine("Índice de descarte inválido.");
            return null;
        }
    }

    public void MostrarMano()
    {
        Console.WriteLine($"{Nombre}'s Mano:");
        foreach (Carta carta in Mano)
        {
            carta.Print();
        }
    }
}

class Program
{
    static void Main()
    {
        Carta cartaEjemplo = new Carta("As", "Tréboles", 1);
        cartaEjemplo.Print();

        Mazo mazo = new Mazo();
        mazo.Barajar();
        mazo.Repartir();
        mazo.Repartir();

        Jugador jugador = new Jugador("Jugador 1");
        jugador.Robar(mazo);
        jugador.Robar(mazo);
        jugador.Robar(mazo);

        jugador.MostrarMano();

        jugador.Descartar(1);
        jugador.MostrarMano();
    }
}
